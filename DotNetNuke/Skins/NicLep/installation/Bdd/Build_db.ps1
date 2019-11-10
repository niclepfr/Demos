#Requires -RunAsAdministrator

$siteName = "dnn"
$appPoolName = "DefaultAppPool"
$dbLogin = "login"
$dbLoginPwd = "password"

$dbDir = "C:\db"
if (Test-Path $dbDir) {
    Remove-Item $dbDir -Recurse -Force
}

mkdir $dbDir

Invoke-Sqlcmd -Query:"CREATE DATABASE [$siteName] ON (NAME=dnn, FILENAME='$dbDir\dnn.mdf') LOG ON (NAME=dnn_log, FILENAME='$dbDir\dnn_log.ldf');" -Database:master
if (-not (Test-Path "SQLSERVER:\SQL\(local)\DEFAULT\Logins\$(ConvertTo-EncodedSqlName "$dbLogin")")) {
    Invoke-Sqlcmd -Query:"CREATE LOGIN [$dbLogin] WITH PASSWORD=N'$dbLoginPwd', DEFAULT_DATABASE = [$siteName], CHECK_EXPIRATION=ON, CHECK_POLICY=ON;" -Database:master
}
Invoke-Sqlcmd -Query:"CREATE USER [$dbLogin] FOR LOGIN [$dbLogin];" -Database:$siteName
Invoke-Sqlcmd -Query:"EXEC sp_addrolemember N'db_owner', N'$dbLogin';" -Database:$siteName

Invoke-Sqlcmd -Query:"ALTER DATABASE [$siteName] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;" -Database:master
Invoke-Sqlcmd -Query:"ALTER DATABASE [$siteName] SET MULTI_USER WITH ROLLBACK IMMEDIATE;" -Database:master

#détacher la bdd avec le login créé pour pouvoir la ré-attacher
Invoke-Sqlcmd -Query:"EXEC sp_detach_db @dbname='$siteName', @skipchecks='true';" -Database:master -Username:$dbLogin -Password:$dbLoginPwd

