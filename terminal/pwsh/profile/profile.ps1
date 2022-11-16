Import-Module Terminal-Icons
Import-Module PSReadLine
[console]::InputEncoding = [console]::OutputEncoding = [System.Text.UTF8Encoding]::new()
oh-my-posh init pwsh --config ~/panos.omp.json | Invoke-Expression

function elevate () { Start-Process pwsh -Verb runAs }

function pathReload () { $env:Path = [System.Environment]::GetEnvironmentVariable("Path", "Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path", "User") }