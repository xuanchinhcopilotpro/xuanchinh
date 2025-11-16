@echo off
echo ============================================
echo   XrmToolBox Plugin Decompiler
echo   Build and Install Script
echo ============================================
echo.

REM Check if dotnet is installed
where dotnet >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: .NET SDK not found!
    echo Please install .NET 8.0 SDK from:
    echo https://dotnet.microsoft.com/download/dotnet/8.0
    pause
    exit /b 1
)

echo [1/4] Restoring NuGet packages...
dotnet restore
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to restore packages
    pause
    exit /b 1
)

echo.
echo [2/4] Building plugin (Release)...
dotnet build -c Release
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Build failed
    pause
    exit /b 1
)

echo.
echo [3/4] Creating deployment package...
set OUTPUT_DIR=bin\Release\net462
set DEPLOY_DIR=Deploy

if not exist "%DEPLOY_DIR%" mkdir "%DEPLOY_DIR%"

copy "%OUTPUT_DIR%\XrmToolBox.PluginDecompiler.dll" "%DEPLOY_DIR%\" /Y
copy "%OUTPUT_DIR%\ICSharpCode.Decompiler.dll" "%DEPLOY_DIR%\" /Y
copy "%OUTPUT_DIR%\System.Collections.Immutable.dll" "%DEPLOY_DIR%\" /Y
copy "%OUTPUT_DIR%\System.Reflection.Metadata.dll" "%DEPLOY_DIR%\" /Y
copy "%OUTPUT_DIR%\System.Runtime.CompilerServices.Unsafe.dll" "%DEPLOY_DIR%\" /Y
copy "PluginManifest.json" "%DEPLOY_DIR%\" /Y

echo.
echo [4/4] Finding XrmToolBox installation...
set XRMTB_PATH=%AppData%\MscrmTools\XrmToolBox\Plugins

if exist "%XRMTB_PATH%" (
    echo Found XrmToolBox at: %XRMTB_PATH%
    echo.
    set /p INSTALL="Do you want to install the plugin to XrmToolBox? (Y/N): "
    if /i "%INSTALL%"=="Y" (
        echo Installing plugin...
        xcopy "%DEPLOY_DIR%\*.*" "%XRMTB_PATH%\" /Y /I
        echo.
        echo ============================================
        echo   INSTALLATION SUCCESSFUL!
        echo ============================================
        echo.
        echo Plugin installed to: %XRMTB_PATH%
        echo.
        echo Please restart XrmToolBox to see the plugin.
    ) else (
        echo.
        echo Plugin files are ready in the Deploy folder.
        echo Manually copy them to your XrmToolBox Plugins folder.
    )
) else (
    echo.
    echo XrmToolBox not found in the default location.
    echo Plugin files are ready in the Deploy folder.
    echo.
    echo Manually copy them to your XrmToolBox Plugins folder:
    echo   %DEPLOY_DIR%\*.*  -->  [XrmToolBox]\Plugins\
)

echo.
echo ============================================
echo   BUILD COMPLETE!
echo ============================================
echo.
echo Files ready in: %DEPLOY_DIR%
echo.
pause
