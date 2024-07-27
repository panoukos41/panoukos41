dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org

$tools = @(
    "dotnet-script"
    "dotnet-ef"
    "dotnet-vs"
    "nanoff"
)

foreach ($tool in $tools) {
    Write-Host "Installing: " $tool
    dotnet tool install -g $tool
}