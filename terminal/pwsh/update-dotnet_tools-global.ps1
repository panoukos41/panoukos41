foreach ($package in $(dotnet tool list --global | Select-Object -Skip 2)) {
    Write-Host "dotnet tool update --global $($package.Split(" ", 2)[0])"
    dotnet tool update --global $($package.Split(" ", 2)[0])
    Write-Host ""
}
Read-Host "Press any key to exit"