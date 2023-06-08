[CmdletBinding()]
param (
    [String] $key,

    [Parameter(Mandatory)]
    [String] $user,
    
    [Parameter(Mandatory)]
    [String] $server,

    [Parameter(Mandatory = $false)]
    [Bool] $clear = $false
)

$command = $clear `
    ? "rm .ssh/authorized_keys && cat >> .ssh/authorized_keys" `
    : "cat >> .ssh/authorized_keys"

Get-Content $key | ssh "$user@$server" $command
