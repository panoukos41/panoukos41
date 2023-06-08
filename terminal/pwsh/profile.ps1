Import-Module PSReadLine
Import-Module Terminal-Icons

Set-PSReadLineOption -PredictionViewStyle ListView
[console]::InputEncoding = [console]::OutputEncoding = [System.Text.UTF8Encoding]::new()

oh-my-posh init pwsh --config "$PSHOME\panos.omp.json" | Invoke-Expression -ErrorAction Ignore 2> $null

$projects = "D:/panou/Projects/"

function elevate () { Start-Process pwsh -Verb runAs }

function Path-Reload() {
    $env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")
}