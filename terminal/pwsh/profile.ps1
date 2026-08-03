Import-Module PSReadLine -ErrorAction SilentlyContinue -Force
Import-Module Terminal-Icons -ErrorAction SilentlyContinue -Force

# functions
function Elevate () { 
    Start-Process pwsh -Verb runAs
}

function Reload-Path() {
    $env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")
}

# oh my posh
[console]::InputEncoding = [console]::OutputEncoding = [System.Text.UTF8Encoding]::new()
oh-my-posh init pwsh --config "$PSScriptRoot\panos.omp.json" | Invoke-Expression -ErrorAction Ignore 2> $null

# ps readline
if ($Host.UI.RawUI.WindowSize.Height -gt 14) {
    Set-PSReadLineOption -PredictionViewStyle ListView
}

# customizations
if (Test-Path "$PSScriptRoot\profile.after.ps1") {
    Import-Module $PSScriptRoot\profile.after.ps1
}