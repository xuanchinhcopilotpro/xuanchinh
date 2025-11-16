@echo off
echo ============================================
echo   Git Push Helper
echo ============================================
echo.

REM Check if git is installed
where git >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Git is not installed!
    echo.
    echo Please install Git from:
    echo https://git-scm.com/download/win
    echo.
    echo After installing Git, restart this script.
    pause
    exit /b 1
)

echo Step 1: Initializing Git repository (if needed)...
git init
echo.

echo Step 2: Adding all files...
git add .
echo.

echo Step 3: Checking status...
git status
echo.

set /p COMMIT_MSG="Enter commit message (or press Enter for default): "
if "%COMMIT_MSG%"=="" set COMMIT_MSG=Initial commit - Dynamics 365 Plugin Decompiler tools

echo.
echo Step 4: Committing with message: "%COMMIT_MSG%"
git commit -m "%COMMIT_MSG%"
echo.

echo Step 5: Repository setup
echo.
echo To push to GitHub, you need to:
echo 1. Create a repository on GitHub
echo 2. Add remote origin
echo.
set /p REPO_URL="Enter your GitHub repository URL (or press Enter to skip): "

if "%REPO_URL%"=="" (
    echo.
    echo Skipping remote setup. Your code is committed locally.
    echo.
    echo To add remote later, run:
    echo   git remote add origin [YOUR_REPO_URL]
    echo   git branch -M main
    echo   git push -u origin main
) else (
    echo.
    echo Adding remote origin...
    git remote add origin %REPO_URL%
    
    echo.
    echo Renaming branch to main...
    git branch -M main
    
    echo.
    echo Pushing to GitHub...
    git push -u origin main
    
    if %ERRORLEVEL% EQU 0 (
        echo.
        echo ============================================
        echo   SUCCESS! Code pushed to GitHub!
        echo ============================================
    ) else (
        echo.
        echo ============================================
        echo   Push failed. Please check:
        echo   - Repository URL is correct
        echo   - You have access to the repository
        echo   - GitHub authentication is set up
        echo ============================================
    )
)

echo.
pause
