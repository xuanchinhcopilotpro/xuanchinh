<#
Simple helper to restore and publish the WPF app as a self-contained single-file exe for win-x64.
Usage (PowerShell):
  cd path\to\repo\test\scripts
  .\build-windows.ps1
#>

param(
    [string] $Configuration = 'Release',
    [string] $Runtime = 'win-x64',
    [switch] $SelfContained = $true
)

$project = Join-Path -Path (Resolve-Path ..) -ChildPath 'ApiTester.csproj'
Write-Host "Project: $project"
Write-Host "Configuration: $Configuration Runtime: $Runtime SelfContained: $SelfContained"

dotnet restore $project

$selfContainedFlag = $SelfContained.IsPresent ? '--self-contained true' : ''

dotnet publish $project -c $Configuration -r $Runtime $selfContainedFlag -p:PublishSingleFile=true -p:EnableWindowsTargeting=true -o "$(Resolve-Path ../bin/publish)"

if ($LASTEXITCODE -ne 0) { throw "dotnet publish failed with exit code $LASTEXITCODE" }

Write-Host "Published to: $(Resolve-Path ../bin/publish)"
