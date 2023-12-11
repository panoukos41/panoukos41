$dir = $PSScriptRoot;
$current = (Get-ChildItem -Path $dir -File -Filter "*.txt" | Select-Object -ExpandProperty BaseName) ?? "v0.0.0"
$latest = (Invoke-RestMethod -Method GET -Uri "https://api.github.com/repos/tailwindlabs/tailwindcss/releases/latest").tag_name

Write-Host "Current: " -NoNewline
Write-Host $current -ForegroundColor Green
Write-Host " Latest: " -NoNewline
Write-Host $latest -ForegroundColor Green

if ($current -eq $latest) {
    Write-Host "You already have the latest version!"
    exit
}

Write-Host "Downloading " -NoNewline
Write-Host $latest -ForegroundColor Green

$platform = "tailwindcss-windows-x64.exe"
$binaries = "https://github.com/tailwindlabs/tailwindcss/releases/download/$latest/$platform"
$out = "$dir"
$exe = "$out\tailwindcss.exe"

Remove-Item -Path $exe -Recurse -ErrorAction Ignore

Write-Host "Downloading binaries @ " -NoNewline
Write-Host $binaries -ForegroundColor Yellow
Invoke-WebRequest $binaries -OutFile $exe

Remove-Item "$dir\$current.txt" -ErrorAction Ignore
New-Item -Path "$dir\$latest.txt" -ItemType File > $null

Write-Host "Tailwind CSS " -NoNewline
Write-Host $latest -ForegroundColor Green -NoNewline
Write-Host " installed successfully!"