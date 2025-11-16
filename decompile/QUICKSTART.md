# Quick Start Guide

## Nếu bạn chưa cài .NET SDK

👉 Xem hướng dẫn chi tiết trong file `INSTALL_DOTNET.md`

Hoặc tải nhanh tại: https://dotnet.microsoft.com/download/dotnet/8.0

## Nếu đã có .NET SDK

### Bước 1: Build project

#### Cách 1 - Sử dụng batch file (Dễ nhất)
```cmd
build.bat
```

#### Cách 2 - Build thủ công
```powershell
dotnet restore
dotnet build -c Release
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

### Bước 2: Sử dụng tool

#### Cách 1 - Chạy với dotnet (Không cần build)
```powershell
# Sử dụng batch file
run.bat "path\to\plugin.dll"

# Hoặc chạy trực tiếp
dotnet run -- "path\to\plugin.dll"
dotnet run -- "path\to\plugin.dll" "output_folder"
```

#### Cách 2 - Chạy file .exe (Sau khi build)
```powershell
.\bin\Release\net8.0\win-x64\publish\DllDecompiler.exe "path\to\plugin.dll"
```

## Ví dụ cụ thể cho Dynamics 365

### Decompile một plugin Dynamics 365
```powershell
# Plugin từ Dynamics 365 thường ở dạng:
dotnet run -- "C:\CRM\Plugins\MyCustomPlugin.dll"

# Hoặc nếu plugin trong folder PluginPackages
dotnet run -- "C:\Users\YourName\Downloads\MyPlugin.dll" "C:\Decompiled"
```

### Kết quả sẽ có:
```
Decompiled/
├── MyCustomPlugin.csproj           # Project file
├── MyCustomPlugin_Combined.cs      # Toàn bộ code trong 1 file
├── Plugins/
│   └── AccountPlugin.cs            # Các plugin classes
└── Services/
    └── ValidationService.cs        # Helper services
```

## Commands hữu ích

```powershell
# Xem version .NET
dotnet --version

# Build nhanh (Debug mode)
dotnet build

# Chạy trực tiếp (không build file exe)
dotnet run -- "MyPlugin.dll"

# Build release version
dotnet build -c Release

# Tạo file exe độc lập (không cần .NET runtime)
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

## Vị trí file sau khi build

| Build type | Vị trí file |
|------------|-------------|
| Debug build | `bin\Debug\net8.0\DllDecompiler.dll` |
| Release build | `bin\Release\net8.0\DllDecompiler.dll` |
| Published exe | `bin\Release\net8.0\win-x64\publish\DllDecompiler.exe` |

## Tips & Tricks

### 1. Decompile nhiều DLL cùng lúc
Tạo batch script:
```batch
@echo off
for %%f in (*.dll) do (
    echo Decompiling %%f...
    DllDecompiler.exe "%%f" "Decompiled_%%~nf"
)
```

### 2. Xem nhanh code
Sau khi decompile, mở file `*_Combined.cs` để xem toàn bộ code trong 1 file.

### 3. Mở project trong Visual Studio
Mở file `.csproj` trong thư mục output để có syntax highlighting và IntelliSense.

## Giải quyết vấn đề thường gặp

### ❌ "dotnet is not recognized"
→ Cài .NET SDK (xem INSTALL_DOTNET.md)

### ❌ "Could not load file or assembly"
→ Copy tất cả DLL dependencies vào cùng thư mục với DLL cần decompile

### ❌ "Access denied"
→ Chạy terminal với quyền Administrator

### ⚠️ Output thiếu code
→ DLL có thể bị obfuscated hoặc protected

## Đọc thêm

- **README.md** - Tài liệu đầy đủ
- **INSTALL_DOTNET.md** - Hướng dẫn cài .NET SDK
- **Program.cs** - Source code của tool

---

**Cần trợ giúp?** Kiểm tra file README.md để biết thêm chi tiết!
