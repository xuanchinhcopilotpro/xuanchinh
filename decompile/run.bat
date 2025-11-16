@echo off
setlocal enabledelayedexpansion

echo ============================================
echo   Dynamics 365 Plugin DLL Decompiler
echo ============================================
echo.

REM Check if dotnet is installed
where dotnet >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: .NET SDK not found!
    echo.
    echo Please install .NET 8.0 SDK from:
    echo https://dotnet.microsoft.com/download/dotnet/8.0
    echo.
    pause
    exit /b 1
)

REM Check if DLL path is provided
if "%~1"=="" (
    echo Usage: run.bat ^<path-to-dll^> [output-folder]
    echo.
    echo Examples:
    echo   run.bat MyPlugin.dll
    echo   run.bat "C:\Plugins\MyPlugin.dll"
    echo   run.bat "C:\Plugins\MyPlugin.dll" "C:\Output"
    echo.
    pause
    exit /b 1
)

REM Run the decompiler
dotnet run -- %*

pause
