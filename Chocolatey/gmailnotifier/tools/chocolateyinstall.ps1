$ErrorActionPreference = 'Stop';

$packageName= $env:ChocolateyPackageName
$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$url        = 'https://github.com/xps/Gmail-Notifier-Replacement/releases/download/1.2.1/GmailNotifierReplacement.msi'

$packageArgs = @{
  packageName   = $packageName
  unzipLocation = $toolsDir
  fileType      = 'MSI'
  url           = $url
  url64bit      = $url

  softwareName  = 'gmailnotifier*'

  checksum      = 'ACE99ACCC9C959D549911E8750712B3D8BF6AB29772526315CA85F29215A2993'
  checksumType  = 'sha256'
  checksum64    = 'ACE99ACCC9C959D549911E8750712B3D8BF6AB29772526315CA85F29215A2993'
  checksumType64= 'sha256'

  silentArgs    = "/qn /norestart /l*v `"$($env:TEMP)\$($packageName).$($env:chocolateyPackageVersion).MsiInstall.log`""
  validExitCodes= @(0, 3010, 1641)
}

Install-ChocolateyPackage @packageArgs








    








