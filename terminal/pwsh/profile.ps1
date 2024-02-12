Import-Module PSReadLine -ErrorAction SilentlyContinue
Import-Module Terminal-Icons -ErrorAction SilentlyContinue

Set-Alias -Name "k" -Value "kubectl"

[console]::InputEncoding = [console]::OutputEncoding = [System.Text.UTF8Encoding]::new()

oh-my-posh init pwsh --config "$PSHOME\panos.omp.json" | Invoke-Expression -ErrorAction Ignore 2> $null

$projects = "D:/panou/Projects/"

if ($Host.UI.RawUI.WindowSize.Height -gt 14) {
    Set-PSReadLineOption -PredictionViewStyle ListView
}

function elevate () { Start-Process pwsh -Verb runAs }

function Path-Reload() {
    $env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")
}