Param(
  $eventStoreServerLocation = "c:\eventstore\server"
)

$ErrorActionPreference = "inquire"

If (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
{   
    $arguments = "-file """ + $myinvocation.mycommand.definition + """ -eventStoreServerLocation """ + $eventStoreServerLocation + """"
    Start-Process "$psHome\powershell.exe" -Verb runAs -ArgumentList $arguments
    break
}

Function Get-ScriptDirectory {
    Split-Path -parent $PSCommandPath
}

Function StartEventStore($configFile)
{
    $srcFolder = Get-ScriptDirectory
	$configFullPath = Join-Path $srcFolder -childpath $configFile;
    $eventStoreExe = "$eventStoreServerLocation\EventStore.ClusterNode.exe";
    $arguments = "--config=`"$configFullPath`"";
	Start-Process -WorkingDirectory $eventStoreServerLocation -FilePath $eventStoreExe -ArgumentList $arguments
}

StartEventStore("config.yml");
