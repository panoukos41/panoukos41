[CmdletBinding()]
param (
    [String] $out = "",
    [String] $pass = ""
)

$out = $out.Equals("") ? "id_rsa" : $out
$pass = $pass.Equals("") ? """" : "$pass"

ssh-keygen -b 4096 -t rsa -f $out -q -N $pass
