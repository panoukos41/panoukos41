Import-Module PSReadLine
Import-Module Terminal-Icons

Set-PSReadLineOption -PredictionViewStyle ListView

[console]::InputEncoding = [console]::OutputEncoding = [System.Text.UTF8Encoding]::new()

oh-my-posh init pwsh --config ~/panos.omp.json | Invoke-Expression -ErrorAction Ignore

kubectl completion powershell | Out-String | Invoke-Expression -ErrorAction Ignore

helm completion powershell | Out-String | Invoke-Expression -ErrorAction Ignore

$projects = "D:/panou/Projects/"

function elevate () { Start-Process pwsh -Verb runAs }

function Path-Reload() {
    $env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")
}