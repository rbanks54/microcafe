Import-Module -Name "$PSScriptRoot\Invoke-MsBuild.psm1"

# global variables
Function BuildSolutions($pathToSolution) {
    $buildSucceeded = Invoke-MsBuild -Path $pathToSolution

    if ($buildSucceeded)
    { 
        Write-Host "  - Build completed successfully." 
    }
    else
    { 
        Write-Host "  - Build failed. Check the build log file for errors." 
    }

    return $buildSucceeded;
}

# build the solutions
$continue = $true;
if($continue) {
    Write-Host "1. Building Common Microservice Library ...." 
    $continue = BuildSolutions("$PSScriptRoot\MicroService-Common\MicroServices.Common.sln");
}

if($continue) {
    Write-Host "2. Building Cashier Services...." 
    $continue = BuildSolutions("$PSScriptRoot\Cashier\Cashier.sln");
}

if($continue) {
    Write-Host "3. Building Barista Services...." 
    $continue = BuildSolutions("$PSScriptRoot\Barista\Barista.sln");
}
