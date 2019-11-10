#Requires -RunAsAdministrator
#Requires -Modules IISAdministration, Microsoft.PowerShell.Archive, SqlServer

add-type @"
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    public class TrustAllCertsPolicy : ICertificatePolicy {
        public bool CheckValidationResult(
            ServicePoint srvPoint, X509Certificate certificate,
            WebRequest request, int certificateProblem) {
            return true;
        }
    }
"@
$AllProtocols = [System.Net.SecurityProtocolType]'Ssl3,Tls,Tls11,Tls12'
[System.Net.ServicePointManager]::SecurityProtocol = $AllProtocols
[System.Net.ServicePointManager]::CertificatePolicy = New-Object TrustAllCertsPolicy

$version = '9.3.2'
$releaseversion = '24'
$dnnInstallUrl = "https://github.com/dnnsoftware/Dnn.Platform/releases/download/v$version/DNN_Platform_$version." + $releaseversion + "_Install.zip"

$installZip = "$PSScriptRoot\install_$version.zip"
if (-not (Test-Path $installZip)) {
    Invoke-WebRequest $dnnInstallUrl -OutFile $installZip
}

$siteDir = "$PSScriptRoot\wwwroot\"
if (Test-Path $siteDir) {
    Remove-Item $siteDir -Recurse -Force
}

Expand-Archive $installZip $siteDir


docker build -t dnn-platform-install:$version $PSScriptRoot
