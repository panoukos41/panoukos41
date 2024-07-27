function DownloadGithub {
    param (
        [string] $repo,
        [string] $filenamePattern,
        [string] $out,
        [bool] $preRelease = $false
    )

    $releasesUri = "https://api.github.com/repos/$repo/releases"

    if (!$preRelease) { $releasesUri += "/latest" }

    $release = ((Invoke-RestMethod -Method GET -Uri $releasesUri).assets | Where-Object name -like $filenamePattern)
    $out = Join-Path $out $release.name

    Write-Host "| release selected |:" $release.name
    Write-Host "|     download url |:" $release.browser_download_url
    Write-Host "|         out path |:" $out

    Invoke-WebRequest -Uri $release.browser_download_url -Out $out
}