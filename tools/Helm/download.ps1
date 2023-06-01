$current = (Get-ChildItem -Path . -File -Filter "*.txt" | Select-Object -ExpandProperty BaseName) ?? "v0.0.0"
$latest = (Invoke-RestMethod -Method GET -Uri "https://api.github.com/repos/helm/helm/releases/latest").tag_name

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

$platform = "windows-amd64"
$binaries = "https://get.helm.sh/helm-$latest-$platform.zip"
$checksum = "$binaries.sha256"
$out = ".\.download"
$zip = "$out\helm-$latest.zip"
$sha = "$out\helm-$latest.zip.sha256"

function clean() {
    Remove-Item -Path $out -Recurse -ErrorAction Ignore
}

clean
New-Item -Path $out -ItemType Directory -ErrorAction Ignore > $null

Write-Host "Downloading binaries @ " -NoNewline
Write-Host $binaries -ForegroundColor Yellow
Invoke-WebRequest $binaries -OutFile $zip

Write-Host "Downloading checksum @ " -NoNewline
Write-Host $checksum -ForegroundColor Yellow
Invoke-WebRequest $checksum -OutFile $sha

Write-Host "Validating binaries"
if ($(Get-FileHash -Algorithm SHA256 $zip).Hash -ne $(Get-Content $sha)) {
    Write-Host "Failed checksum validation removing binaries."
    clean
    exit
}

Write-Host "Binaries succesfully validated"
Expand-Archive -Path $zip -DestinationPath $out

$items = Get-ChildItem -Path "$out\$platform\" | Select-Object -ExpandProperty Name
$items | Remove-Item -ErrorAction Ignore
$items | ForEach-Object { "$out\$platform\$_" } | Copy-Item -Destination .

Remove-Item "$current.txt" -ErrorAction Ignore
New-Item -Path "$latest.txt" -ItemType File > $null

clean
Write-Host "Helm " -NoNewline
Write-Host $latest -ForegroundColor Green -NoNewline
Write-Host " installed successfully!"