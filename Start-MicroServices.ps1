#requires -version 3
[CmdletBinding()]
param ([bool]$monitorServices = $true)

[console]::TreatControlCAsInput = $true

function Invoke-MicroService {
    param (
        $SourcePath,
        $DeployPath,
        $ProcessFilePath
    )

    Copy-Item -Path $SourcePath -Destination $DeployPath -Recurse -ErrorAction Stop
	
    $Process = Start-Process -FilePath $ProcessFilePath -PassThru -ErrorAction Stop

    return $Process
}

function Stop-MicroService {
    param (
        $Process
    )

    if (-not $Process) { return }

    $Process.Refresh()
    if (-not $Process.HasExited) {
        $Process.CloseMainWindow() | Out-Null # todo consider PostMessage to window with Ctrl+C
        Start-Sleep -Seconds 1
        $Process.Refresh()
    }
    if (-not $Process.HasExited) {
        $Process.Kill() | Out-Null
        Start-Sleep -Seconds 2
        $Process.Refresh()
    }
    if (-not $Process.HasExited) {
        throw "could not stop process"
    }
}

function Find-MicroService {
    param (
        $ProcessFilePath
    )

    $ProcessBaseName = [System.IO.Path]::GetFileNameWithoutExtension($ProcessFilePath)
    $Process = Get-Process -Name $ProcessBaseName -ErrorAction SilentlyContinue | 
        Where-Object { $_.Path -eq $ProcessFilePath }
    
    if (@($Process).Length -gt 1) { throw "More than one instance of the same microservice running" }
    
    return $Process
}

function Test-MicroService {
    param (
        $Process
    )

    if ($Process) {
        $Process.Refresh()
        return (-not $Process.HasExited)
    }
    return $false

}
    

function Get-LastWriteTime {
    param (
        [Parameter(Mandatory)]
        $Path
    )
    Get-ChildItem -Path $Path -Recurse | 
        Measure-Object -Property LastWriteTime -Maximum |
        Select-Object -ExpandProperty Maximum
}

function New-MicroServiceObject {
    param (
        $ProjectName,
        $RootPath
    )

    $ServiceHostProcessFileName = $ProjectName + '.exe'

    $o = [pscustomobject]@{
        Name = $ProjectName
        SourcePath = Join-Path -Path $PSScriptRoot -ChildPath $RootPath\$ProjectName\bin\Debug
        ProcessFileName = $ServiceHostProcessFileName
        Process = [System.Diagnostics.Process]$null
        DeployedLastWriteTime = [DateTime]::MinValue
        DeployPath = ''
        ProcessFilePath = ''
    }

    $o.DeployPath = Join-Path -Path (Split-Path -Path $o.SourcePath) -ChildPath Deployed
    $o.ProcessFilePath = Join-Path -Path $o.DeployPath -ChildPath $o.ProcessFileName

    $o.Process = Find-MicroService -ProcessFilePath $o.ProcessFilePath

    return $o
}

function Poll {
    param (
        $MicroServiceData
    )

    $ShouldDeploy = $false
    $LastWriteTime = Get-LastWriteTime -Path $MicroServiceData.SourcePath
    if (-not (Test-MicroService -Process $MicroServiceData.Process)) {
        $ShouldDeploy = $true
    } else {
	    if ($LastWriteTime -gt $MicroServiceData.DeployedLastWriteTime) {
            $ShouldDeploy = $true
        }
    }		

    if (-not $ShouldDeploy) {
        return
	}

    "$(Get-Date): Redeploying '$($MicroServiceData.Name)'"
    
    Stop-MicroService -Process $MicroServiceData.Process

    if (Test-Path -Path $MicroServiceData.DeployPath) { 
        Remove-Item -Path $MicroServiceData.DeployPath -Recurse
        if (-not $?) {
            return
        }
    }

    $MicroServiceData.Process = Invoke-MicroService -SourcePath $MicroServiceData.SourcePath -DeployPath $MicroServiceData.DeployPath -ProcessFilePath $MicroServiceData.ProcessFilePath

    $MicroServiceData.DeployedLastWriteTime = $LastWriteTime
}

$MicroServices = @(
    New-MicroServiceObject -RootPath Admin -ProjectName Admin.Service
    New-MicroServiceObject -RootPath Admin -ProjectName Admin.ReadModels.Service
    New-MicroServiceObject -RootPath Cashier -ProjectName Cashier.ReadModels.Service
    New-MicroServiceObject -RootPath Cashier -ProjectName Cashier.Service
    New-MicroServiceObject -RootPath Barista -ProjectName Barista.Service
)

while ($true) {
    if ($Host.UI.RawUI.KeyAvailable -and (3 -eq [int]$Host.UI.RawUI.ReadKey("AllowCtrlC,IncludeKeyUp,NoEcho").Character)) {
        "$(Get-Date): Shutdown requested"

        break
    }

    foreach ($MicroService in $MicroServices) {
        Poll -MicroServiceData $MicroService
    }

    if (-not $monitorServices) {
        return
    }

    Start-Sleep -Seconds 1
}

foreach ($MicroService in $MicroServices) {
    "$(Get-Date): Stopping '$($MicroService.Name)'"
    Stop-MicroService -Process $MicroService.Process
}