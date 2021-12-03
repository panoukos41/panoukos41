[CmdletBinding()]
param (
    [Parameter(Position = 0, Mandatory)]
    [Alias("command")]
    [ValidateSet("get", "install", "uninstall", "start", "stop")]
    [string] $command = "get",

    [Parameter(Position = 1, ParameterSetName = "install")]
    [Alias("path")]
    [string] $path = "..",

    [Parameter(Position = 2, ParameterSetName = "install")]
    [Alias("type")]
    [ValidateSet("Automatic", "Manual", "Disabled")]
    [string] $type = "Automatic",

    [Parameter(Position = 3)]
    [Alias("service")]
    [string] $service = "proxy"
)

if ($command -eq "get") {
    Get-Service $service
    exit
}

$argument = switch ($command) {
    "start" { "Start-Service $service" }
    "stop" { "Stop-Service $service" }
    "uninstall" { "Remove-Service $service" }
    "install" {
        $path = Resolve-Path $path
        $params = @{
            Name           = $service
            StartupType    = $type
            BinaryPathName = "$path\$service.exe"
            DisplayName    = "Yarp Proxy"
            Description    = "A YARP proxy app to proxy localhost websites."
        }
        { { New-Service @params } }
    }
}

$options = @{
    FilePath     = "pwsh"
    Verb         = "RunAs"
    ArgumentList = "-Command", $argument
    Wait         = $true
}

Start-Process @options
