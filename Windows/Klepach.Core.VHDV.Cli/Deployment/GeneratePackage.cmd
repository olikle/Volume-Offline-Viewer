@echo off
set pwd=%~dp0
set CPTempDir=%pwd%Temp

if exist %CPTempDir% rmdir /s /q %CPTempDir%
mkdir %CPTempDir%
echo created directory %CPTempDir%
cd %pwd%..
dotnet publish "Klepach.Core.VHDV.Cli.csproj" -o %CPTempDir%

echo ZIP the package
set ZipFile=%pwd%%%yyyyMMdd%%-vhdv-cli.zip
if exist %ZipFile% del %ZipFile%
powershell.exe -Command "& '%pwd%..\..\..\Projekte\Powershell\TB-Archive.ps1' 'Compress' -CompressedFile '%ZipFile%' -FilesToCompress '%CPTempDir%' "

echo remove Temp
rmdir /s /q %CPTempDir%

echo.
echo finish create solution File

pause