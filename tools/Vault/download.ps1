$current = (Get-ChildItem -Path . -File -Filter "*.txt" | Select-Object -ExpandProperty BaseName) ?? "v0.0.0"
$latest = (Invoke-RestMethod -Method GET -Uri "https://api.github.com/repos/hashicorp/vault/releases/latest").tag_name

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

$version = $latest.TrimStart("v")
$platform = "windows_amd64"
$binaries = "https://releases.hashicorp.com/vault/$version/vault_${version}_$platform.zip"
$checksum = "https://releases.hashicorp.com/vault/$version/vault_${version}_SHA256SUMS"
$out = ".\.download"
$zip = "$out\vault-$latest.zip"
$sha = "$out\vault-$latest.zip.sha256"
$exe = "$out\vault.exe"

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

Expand-Archive -Path $zip -DestinationPath $out

Write-Host "Validating binaries"

$sha256 = (Get-Content $sha | ConvertFrom-StringData -Delimiter " " | Where-Object Values -eq "vault_${version}_$platform.zip").Keys
if ($(Get-FileHash -Algorithm SHA256 $zip).Hash -ne $sha256) {
    Write-Host "Failed checksum validation removing binaries."
    clean
    exit
}

Write-Host "Binaries succesfully validated"

Remove-Item "vault.exe" -ErrorAction Ignore
Copy-Item $exe -Destination .

Remove-Item "$current.txt" -ErrorAction Ignore
New-Item -Path "$latest.txt" -ItemType File > $null

clean
Write-Host "Vault " -NoNewline
Write-Host $latest -ForegroundColor Green -NoNewline
Write-Host " installed successfully!"