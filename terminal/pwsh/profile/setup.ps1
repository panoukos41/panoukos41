[CmdletBinding()]
param (
    # True to update modules/apps that are found.
    [Parameter(Position = 0)]
    [bool]
    $update = $false
)

function Packages ($packages) {
    foreach ($package in $packages) {
        if ((winget list --id $package).StartsWith("No installed") | Select-Object -Last 1) {
            Write-Host "Installing $package"
            winget install $package -s winget
        }
        elseif ($update) {
            Write-Host "Updating $package"
            winget install $package -s winget
        }
    }
}

function Modules ($modules) {
    foreach ($module in $modules) {
        if (-not(Get-Module -ListAvailable -Name $module)) {
            Write-Host "Installing $module"
            Install-Module $module
        }
        elseif ($update) {
            Write-Host "Updating $module"
            Update-Module $module
        }
    }
}

Modules(@(
    "PSReadLine"
    "Terminal-Icons"
))

Packages(@(
    "JanDeDobbeleer.OhMyPosh"
))
