Param(
  $Clear = "all",
  $EventStoreServerLocation = "c:\eventstore\server",
  $RedisInstallLocation = "C:\Program Files\Redis"
)

If("all","eventstore","redis" -NotContains $Clear)
{
	Throw "$($Clear) is not a valid value. Please use eventstore, redis or all"
}

$eventStores = @("MicroCafe")

If (-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
{   
    $arguments = "-file """ + $myinvocation.mycommand.definition + """ -EventStoreServerLocation """ + $EventStoreServerLocation + """"
    Start-Process "$psHome\powershell.exe" -Verb runAs -ArgumentList $arguments
    break
}

If(!($Clear -eq "redis"))
{
	Write-Host "Cleaning event store data"
	$eventStoreRoot = (Get-Item $EventStoreServerLocation).Parent.FullName

	#Kill existing Event Store processes
	$eventStoreProcesses = get-process | ?{$_.name -eq "EventStore.ClusterNode"}
	$eventStoreProcesses | foreach { stop-process -id $_.Id }

	#Delete data folders (adjust for your own environment as needed)
	$eventStoreDataDirectories = $eventStores | foreach { Join-Path $eventStoreRoot $_ } | foreach { Join-Path $_ "Data" }
	$eventStoreDataDirectories | foreach { rmdir $_ -recurse }
}

If(!($Clear -eq "eventstore"))
{
	Write-Host "Cleaning redis data"
	$redisCli = Join-Path $RedisInstallLocation "redis-cli.exe"
	$redisArgs = "flushdb"
	Start-Process $redisCli -ArgumentList $redisArgs
}