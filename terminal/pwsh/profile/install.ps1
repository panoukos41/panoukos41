[CmdletBinding()]
param (
    # True to update modules/apps that are found.
    [Parameter(Position = 0)]
    [bool]
    $update = $false
)

$module = "Terminal-Icons"

if (-not(Get-Module -ListAvailable -Name $module)) {
    Install-Module $module
}
elseif ($update) {
    Update-Module $module
}

winget install JanDeDobbeleer.OhMyPosh -s winget