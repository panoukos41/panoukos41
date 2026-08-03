[CmdletBinding()]
param (
    [Parameter()]
    [switch]
    $uninstall,

    [Parameter()]
    [System.Collections.Generic.HashSet[string]]
    $features
)

Import-Module $PSScriptRoot\pwsh\scripts\modules\.SetupMethods.psm1

function HashSet {
    [OutputType([System.Collections.Generic.HashSet[string]])]
    param (
        [string[]] $values
    )

    return , [System.Collections.Generic.HashSet[string]]::new($values, [System.StringComparer]::OrdinalIgnoreCase)
}

$features = HashSet($features.Count -eq 0 ? "m", "p", "s" : $features)
$setModules = HashSet "m", "modules"
$setPackages = HashSet "p", "packages"
$setSymlinks = HashSet "s", "symlinks"

if ($setModules.Overlaps($features) -and !$uninstall) {
    $modules = @(
        "PSReadLine"
        "Terminal-Icons"
    )
    Write-Host "Modules"
    Modules -modules $modules -uninstall:$uninstall
}

if ($setPackages.Overlaps($features) -and !$uninstall) {
    $packages = @(
        "JanDeDobbeleer.OhMyPosh"
    )
    Packages -packages $packages -uninstall:$uninstall
    Write-Host "Winget Packages"
}

if ($setSymlinks.Overlaps($features)) {
    $profileDir = $PROFILE.CurrentUserAllHosts | Split-Path -Parent
    $symlinks = @{
        "$PSScriptRoot\pwsh\profile.ps1" = "$profileDir\profile.ps1"
        "$PSScriptRoot\panos.omp.json"   = "$profileDir\panos.omp.json"
    }
    Write-Host "SymLinks"
    Symlinks -links $symlinks -uninstall:$uninstall
}
