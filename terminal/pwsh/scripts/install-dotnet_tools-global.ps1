dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org

$tools = @(
    "dotnet-script"
    "dotnet-ef"
    "Microsoft.Tye"
    "nanoff"
    "Redth.Net.Maui.Check"
    "coverlet.console"
    "dotnet-vs"
)

foreach ($tool in $tools) {
    Write-Host "Installing: " $tool
    dotnet tool install -g $tool
}