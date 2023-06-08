[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [bool] $upgrade = $true
)

if ($upgrade) {
    reg add "HKCU\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32" /f /ve
}
else {
    reg delete "HKCU\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}" /f
}