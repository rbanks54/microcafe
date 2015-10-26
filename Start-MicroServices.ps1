Function Execute($pathToExe){
    Start-Process -FilePath $pathToExe
    Start-Sleep -s 1
}

# build the solutions
$continue = $true;

# Run each application
if($continue) {
    Write-Host "* Starting Services...."     
    
    Execute("$PSScriptRoot\Cashier\Cashier.Service\bin\Debug\Cashier.Service.exe");
    
    Execute("$PSScriptRoot\Cashier\Cashier.ReadModels.Service\bin\Debug\Cashier.ReadModels.Service.exe");
    
    Execute("$PSScriptRoot\Barista\Barista.ReadModels.Service\bin\Debug\Barista.ReadModels.Service.exe")
    
    Execute("$PSScriptRoot\Barista\Barista.Service\bin\Debug\Barista.Service.exe")    
    
}
