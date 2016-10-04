[CmdletBinding()]
param ()

Import-Module -Name "$PSScriptRoot\Invoke-MsBuild.psm1"
Import-Module -Name "$PSScriptRoot\Invoke-NuGetPackageRestore.psm1"

$solutions = @(
    @{ Solution = "Microservices Common";   Path = "$PSScriptRoot\MicroService-Common\MicroServices.Common.sln" },
    @{ Solution = "Admin";                  Path = "$PSScriptRoot\Admin\Admin.sln" },
    @{ Solution = "Cashier";                Path = "$PSScriptRoot\Cashier\Cashier.sln" },
    @{ Solution = "Barista";                Path = "$PSScriptRoot\Barista\Barista.sln" }
)

Function ReportProgress($currentSolution, $currentStatus, $currentPercentage) {
    Write-Progress -Id 1 -Activity "Microservices Build" -Status "Building $currentSolution" -CurrentOperation $currentStatus -PercentComplete $currentPercentage
}

Function BuildSolution($pathToSolution, $solutionName, $alreadyComplete, $totalSteps) {
    $currentProgress = $alreadyComplete / $totalSteps * 100
    $progressStep = 1 / $totalSteps * 100 / 2

    ReportProgress $solutionName "Restoring NuGet packages" $currentProgress
    $nugetSucceeded = Invoke-NuGetPackageRestore -Path $pathToSolution
    
    if (!$nugetSucceeded)
    {
        return $nugetSucceeded
    }

    ReportProgress $solutionName "Running MSBuild" ($currentProgress + $progressStep)
    $buildSucceeded = Invoke-MsBuild -Path $pathToSolution

    return $buildSucceeded;
}

[System.Collections.ArrayList]$results = @()

Function AddResult($solution, $result) {
    $object = New-Object -TypeName PSObject

    $object | Add-Member -MemberType NoteProperty -Name "Result" -Value $result    
    $object | Add-Member -MemberType NoteProperty -Name "Solution" -Value $solution.Solution
    $object | Add-Member -MemberType NoteProperty -Name "Location" -Value $solution.Path
    
    $results.Add($object) | Out-Null
}

foreach ($solution in $solutions) {
    if (($results | Where-Object { $_.Result -eq "Failed" }) -ne $null) {
        AddResult $solution "Skipped"
        continue
    }

    $success = BuildSolution $solution.Path $solution.Solution $solutions.IndexOf($solution) $solutions.Length
    
    if ($success) { 
        AddResult $solution "Succeeded" 
    }
    else { 
        AddResult $solution "Failed" 
    }
}

return $results