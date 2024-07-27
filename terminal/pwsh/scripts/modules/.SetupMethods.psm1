function Packages ($packages) {
    foreach ($package in $packages) {
        if ((winget list --id $package).StartsWith("No installed") | Select-Object -Last 1) {
            Write-Host "Installing $package"
            winget install $package -s winget
        }
    }
}

function Modules ($modules) {
    foreach ($module in $modules) {
        if (-not(Get-Module -ListAvailable -Name $module)) {
            Write-Host "Installing $module"
            Install-Module $module
        }
    }
}

function Symlinks([HashTable] $links) {
    foreach ($link in $links.GetEnumerator()) {
        if ((Test-Path $link.Name) -and !(Test-Path $link.Value)) {
            Write-Host "Creating Symlink" -ForegroundColor Yellow
            Write-Host "FROM: " -NoNewline -ForegroundColor Yellow
            Write-Host $link.Name -ForegroundColor Green
            Write-Host "  TO: " -NoNewline -ForegroundColor Red
            Write-Host $link.Value -ForegroundColor Green
            New-Item -ItemType SymbolicLink -Target $link.Name -Path $link.Value
        }
    }
}

function Junctions([HashTable] $junctions) {
    foreach ($junction in $junctions.GetEnumerator()) {
        if ((Test-Path $junction.Name) -and !(Test-Path $junction.Value)) {
            Write-Host "Creating Junction" -ForegroundColor Yellow
            Write-Host "FROM: " -NoNewline -ForegroundColor Yellow
            Write-Host $junction.Name -ForegroundColor Green
            Write-Host "  TO: " -NoNewline -ForegroundColor Red
            Write-Host $junction.Value -ForegroundColor Green
            New-Item -ItemType Junction -Target $junction.Name -Path $junction.Value
        }
    }
}