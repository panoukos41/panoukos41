Import-Module $PSScriptRoot\pwsh\modules\.SetupMethods.psm1

Modules(@(
    "PSReadLine"
    "Terminal-Icons"
))

Packages(@(
    "JanDeDobbeleer.OhMyPosh"
))

Symlinks(@{
    "$PSScriptRoot\pwsh\profile.ps1" = "$PSHOME\profile.ps1"
    "$PSScriptRoot\panos.omp.json" = "$PSHOME\panos.omp.json"
})
