<#
Run the published ApiTester exe. Use after running build-windows.ps1.
Usage:
  .\run-windows.ps1
#>

$publishDir = Join-Path -Path (Resolve-Path ..) -ChildPath 'bin/publish'
if (-not (Test-Path $publishDir)) {
    Write-Error "Publish directory not found: $publishDir. Run build-windows.ps1 first."
    exit 1
}

$exe = Get-ChildItem -Path $publishDir -Filter '*.exe' | Select-Object -First 1
if (-not $exe) {
    Write-Error "No .exe found in $publishDir"
    exit 1
}

Write-Host "Running: $($exe.FullName)"
Start-Process -FilePath $exe.FullName
