Function Execute($pathToExe){
    Start-Process -FilePath $pathToExe
    Start-Sleep -s 1
}

# build the solutions
$continue = $true;

# Run each application
if($continue) {
    Write-Host "* Starting Services...."     
    
    Execute("$PSScriptRoot\Admin\Admin.ReadModels.Service\bin\Debug\Admin.ReadModels.Service.exe")
    
    Execute("$PSScriptRoot\Admin\Admin.Service\bin\Debug\Admin.Service.exe")    
    
    Execute("$PSScriptRoot\Cashier\Cashier.Service\bin\Debug\Cashier.Service.exe");
    
    Execute("$PSScriptRoot\Cashier\Cashier.ReadModels.Service\bin\Debug\Cashier.ReadModels.Service.exe");
    
#    Execute("$PSScriptRoot\Barista\Barista.ReadModels.Service\bin\Debug\Barista.ReadModels.Service.exe")
    
#    Execute("$PSScriptRoot\Barista\Barista.Service\bin\Debug\Barista.Service.exe")    
}
