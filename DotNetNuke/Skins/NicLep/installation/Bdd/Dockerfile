# this Dockerfile borrows ideas from https://github.com/Microsoft/mssql-docker/blob/1efc4cf9b78fa5fccea682f26067189660af85c8/windows/mssql-server-windows-express/dockerfile
FROM microsoft/mssql-server-windows-express

RUN ["powershell", "New-Item", "c:\\temp -ItemType 'directory'"]

ADD Build_db.ps1 /temp

RUN powershell.exe -executionpolicy bypass c:\temp\Build_db.ps1

RUN ["powershell", "Remove-Item", "c:\\temp -recurse"]