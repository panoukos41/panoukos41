$dir = $PSScriptRoot;
$current = (Get-ChildItem -Path $dir -File -Filter "*.txt" | Select-Object -ExpandProperty BaseName) ?? "v0.0.0"
$latest = Invoke-RestMethod -Method GET -Uri "https://storage.googleapis.com/kubernetes-release/release/stable.txt"

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

$platform = "windows/amd64"
$binaries = "https://dl.k8s.io/release/$latest/bin/$platform/kubectl.exe"
$checksum = "https://dl.k8s.io/$latest/bin/$platform/kubectl.exe.sha256"
$out = "$dir\.download"
$exe = "$out\kubectl.exe"
$sha = "$out\kubectl.exe.sha256"

function clean() {
    Remove-Item -Path $out -Recurse -ErrorAction Ignore
}

clean
New-Item -Path $out -ItemType Directory -ErrorAction Ignore > $null

Write-Host "Downloading binaries @ " -NoNewline
Write-Host $binaries -ForegroundColor Yellow
Invoke-WebRequest $binaries -OutFile $exe

Write-Host "Downloading checksum @ " -NoNewline
Write-Host $checksum -ForegroundColor Yellow
Invoke-WebRequest $checksum -OutFile $sha

Write-Host "Validating binaries"

if ($(Get-FileHash -Algorithm SHA256 $exe).Hash -ne $(Get-Content $sha)) {
    Write-Host "Failed checksum validation removing binaries."
    clean
    exit
}

Write-Host "Binaries succesfully validated"

Remove-Item "$dir\kubectl.exe" -ErrorAction Ignore
Copy-Item $exe -Destination $dir

Remove-Item "$dir\$current.txt" -ErrorAction Ignore
New-Item -Path "$dir\$latest.txt" -ItemType File > $null

clean
Write-Host "Kubectl " -NoNewline
Write-Host $latest -ForegroundColor Green -NoNewline
Write-Host " installed successfully!"
