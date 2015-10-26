Write-Host "Cleaning the bin directories"
get-childitem "C:\Projects\Microservices\microservice-poc" -include *.* -recurse | where-object { $_.fullname -like "*\bin\*" } | foreach ($_) { remove-item $_.fullname -recurse -Confirm:$false }

Write-Host "Cleaning the obj directories"
get-childitem "C:\Projects\Microservices\microservice-poc" -include *.* -recurse | where-object { $_.fullname -like "*\obj\*" } | foreach ($_) { remove-item $_.fullname -recurse -Confirm:$false }

Write-Host "Cleaning the packages directories"
get-childitem "C:\Projects\Microservices\microservice-poc" -include *.* -recurse | where-object { $_.fullname -like "*\packages\*" } | foreach ($_) { remove-item $_.fullname -recurse -Confirm:$false }


Write-Host "Press any key to continue ..."
$x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")