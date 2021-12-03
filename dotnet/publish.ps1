[CmdletBinding()]
param (
    [Parameter(Position = 0, Mandatory)]
    [ValidateSet("win-x64", "win-x86", "linux-x64", "linux-arm", "linux-arm64", "osx-x64")]
    $runtime,

    [Parameter(Position = 1)]
    [ValidateSet("true", "false")]
    [switch] $self_contained = $false,
    
    [Parameter(Position = 2)]
    [ValidateSet("true", "false")]
    [switch] $trimmed = $false,

    [Parameter(Position = 3)]
    [string] $out = "./publish"
)

$out = "$out/$runtime"

Remove-Item $out -r *>$null

dotnet publish `
    --nologo `
    -c "Release" `
    -r "$runtime" `
    -o "$out" `
    -p:PublishSingleFile=$true `
    -p:DebugType=embedded `
    -p:IsTransformWebConfigDisabled=true `
    --self-contained $self_contained `
    -p:PublishTrimmed=$trimmed