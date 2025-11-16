@echo off
echo ============================================
echo   Dynamics 365 Plugin DLL Decompiler
echo   Build Script
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

echo [1/3] Restoring NuGet packages...
dotnet restore
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to restore packages
    pause
    exit /b 1
)

echo.
echo [2/3] Building project...
dotnet build -c Release
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Build failed
    pause
    exit /b 1
)

echo.
echo [3/3] Publishing self-contained executable...
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Publish failed
    pause
    exit /b 1
)

echo.
echo ============================================
echo   BUILD SUCCESSFUL!
echo ============================================
echo.
echo Executable location:
echo   bin\Release\net8.0\win-x64\publish\DllDecompiler.exe
echo.
echo You can now use the tool:
echo   DllDecompiler.exe "path\to\your\plugin.dll"
echo.
pause
