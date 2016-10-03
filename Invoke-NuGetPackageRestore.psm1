#Requires -Version 2.0
function Invoke-NuGetPackageRestore
{
	[CmdletBinding(DefaultParameterSetName="Wait")]
	param
	(
		[parameter(Position=0,Mandatory=$true,ValueFromPipeline=$true,HelpMessage="The path to the file to restore NuGet packages for (e.g. a .sln or .csproj file).")]
		[ValidateScript({Test-Path $_})]
		[string] $Path,

        [parameter(Mandatory=$false)]
		[Alias("Show","S")]
		[switch] $ShowNugetWindow
	)

	BEGIN { }
	END { }
	PROCESS
	{
		Set-StrictMode -Version Latest

        # Default the ParameterSet variables that may not have been set depending on which parameter set is being used. This is required for PowerShell v2.0 compatibility.
        if (!(Test-Path Variable:Private:AutoLaunchBuildLogOnFailure)) { $AutoLaunchBuildLogOnFailure = $false }
        if (!(Test-Path Variable:Private:KeepBuildLogOnSuccessfulBuilds)) { $KeepBuildLogOnSuccessfulBuilds = $false }
        if (!(Test-Path Variable:Private:PassThru)) { $PassThru = $false }

		$buildCrashed = $false;
        $windowStyle = if ($ShowNugetWindow) { "Normal" } else { "Hidden" }

        $nugetExe = Get-NuGetExePath
        Write-Debug "NuGet.exe Path [$nugetExe]"

        if ($nugetExe -eq $null)
        {
            Write-Error "Could not find NuGet.exe"
            return $null
        }
        $argumentList = "restore " + $Path

		try
		{
			if ($PassThru)
			{
				$process = Start-Process $nugetExe.FullName -ArgumentList $argumentList -Wait -PassThru -WindowStyle $windowStyle
			}
			else
			{
				$process = Start-Process $nugetExe.FullName -ArgumentList $argumentList -Wait -PassThru -WindowStyle $windowStyle
				$processExitCode = $process.ExitCode
			}
		}
		catch
		{
			$errorMessage = $_
			Write-Error ("Unexpect error occured while building ""$Path"": $errorMessage" );
		}

        return $processExitCode -eq 0
	}
}

function Get-ChildItemToDepth {
  param(
    [String]$Path = $PWD,
    [String]$Filter = "*",
    [Byte]$ToDepth = 255,
    [Byte]$CurrentDepth = 0,
    [Switch]$DebugMode
  )
 
  $CurrentDepth++
  if ($DebugMode) { $DebugPreference = "Continue" }
 
  Get-ChildItem $Path | ForEach-Object {
    $_ | Where-Object { $_.Name -like $Filter }
 
    if ($_.PsIsContainer) {
      if ($CurrentDepth -le $ToDepth) {
        # Callback to this function
        Get-ChildItemToDepth -Path $_.FullName -Filter $Filter -ToDepth $ToDepth -CurrentDepth $CurrentDepth
      } else {
        Write-Debug $("Skipping GCI for Folder: $($_.FullName) " +
          "(Why: Current depth $CurrentDepth vs limit depth $ToDepth)")
      }
    }
  }
}

function Get-NuGetExePath
{
    #$nugetExes = Get-ChildItem $PSScriptRoot NuGet.exe -Recurse
    $nugetExes = Get-ChildItemToDepth -Path $PSScriptRoot -Filter NuGet.exe -ToDepth 2
    
    if ($nugetExes -eq $null) { return $null }
    
    return $nugetExes[0]
}

Export-ModuleMember -Function Invoke-NuGetPackageRestore