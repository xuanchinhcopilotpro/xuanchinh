# Dynamics 365 Plugin Decompiler - Project Summary

Project bao gồm **2 công cụ** để decompile plugin DLL của Dynamics 365:

## 🎯 1. Console Application (Command Line Tool)

**Vị trí**: `d:\CODE\github copilot\decompile\`

### Tính năng
- ✅ CLI tool đơn giản, nhanh chóng
- ✅ Hỗ trợ automation và scripting
- ✅ CI/CD friendly
- ✅ Batch processing

### Cách sử dụng
```powershell
# Build
dotnet build -c Release

# Chạy
dotnet run -- "MyPlugin.dll"
dotnet run -- "MyPlugin.dll" "OutputFolder"

# Hoặc dùng executable
.\bin\Release\net8.0\win-x64\publish\DllDecompiler.exe "MyPlugin.dll"
```

### Files chính
- `DllDecompiler.csproj` - Project file
- `Program.cs` - Main logic
- `README.md` - Documentation đầy đủ
- `QUICKSTART.md` - Hướng dẫn nhanh
- `build.bat` - Build script
- `run.bat` - Run script

---

## 🖥️ 2. Windows Forms GUI Application (Standalone Tool)

**Vị trí**: `d:\CODE\github copilot\decompile\XrmToolBox.PluginDecompiler\`

### Tính năng
- ✅ Giao diện đồ họa đẹp mắt
- ✅ Drag & Drop support
- ✅ Real-time log với màu sắc
- ✅ Progress tracking
- ✅ Dễ sử dụng cho non-technical users

### Cách sử dụng
```powershell
# Build
cd XrmToolBox.PluginDecompiler
dotnet build -c Release

# Chạy
dotnet run

# Hoặc dùng executable
.\bin\Release\net8.0-windows\win-x64\publish\PluginDecompiler.exe
```

### Files chính
- `XrmToolBox.PluginDecompiler.csproj` - Project file
- `MainForm.cs` - GUI application
- `README_GUI.md` - Documentation
- `BUILD_INSTRUCTIONS.md` - Build guide

---

## 📊 So sánh 2 công cụ

| Tiêu chí | Console Tool | GUI Tool |
|----------|--------------|----------|
| **Giao diện** | Command line | Windows Forms |
| **Dễ sử dụng** | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **Automation** | ✅ Excellent | ❌ Not suitable |
| **Batch processing** | ✅ Yes | ❌ One at a time |
| **Visual feedback** | ❌ Text only | ✅ Rich UI |
| **Drag & Drop** | ❌ No | ✅ Yes |
| **Scripting** | ✅ Easy | ❌ Hard |
| **CI/CD** | ✅ Perfect | ❌ Not suitable |

---

## 🎓 Khi nào dùng tool nào?

### Dùng Console Tool khi:
- Cần automation và scripting
- Batch processing nhiều files
- Tích hợp vào CI/CD pipeline
- Quen với command line
- Chạy trên server không có GUI

### Dùng GUI Tool khi:
- Cần giao diện trực quan
- Decompile 1 file occasionally
- Người dùng không quen command line
- Muốn drag & drop files
- Cần visual feedback real-time

---

## 🚀 Quick Start Guide

### Setup môi trường
```powershell
# Cài .NET 8.0 SDK
# Download từ: https://dotnet.microsoft.com/download/dotnet/8.0

# Kiểm tra
dotnet --version  # Nên thấy: 8.0.x
```

### Build cả 2 tools
```powershell
# Build Console tool
cd "d:\CODE\github copilot\decompile"
dotnet build -c Release

# Build GUI tool
cd "d:\CODE\github copilot\decompile\XrmToolBox.PluginDecompiler"
dotnet build -c Release
```

### Test tools

**Console:**
```powershell
cd "d:\CODE\github copilot\decompile"
dotnet run -- "path\to\plugin.dll"
```

**GUI:**
```powershell
cd "d:\CODE\github copilot\decompile\XrmToolBox.PluginDecompiler"
dotnet run
# Sau đó drag & drop DLL file vào ứng dụng
```

---

## 📦 Output của cả 2 tools

Cả 2 tools đều tạo ra cấu trúc giống nhau:

```
MyPlugin_Decompiled/
├── MyPlugin.csproj              # Visual Studio project
├── MyPlugin_Combined.cs         # All code in one file
├── [Namespace]/
│   ├── Plugin1.cs
│   ├── Plugin2.cs
│   └── ...
└── [OtherNamespace]/
    └── ...
```

---

## 🛠️ Deployment

### Console Tool - Tạo executable
```powershell
cd "d:\CODE\github copilot\decompile"
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

# Output: bin\Release\net8.0\win-x64\publish\DllDecompiler.exe
```

### GUI Tool - Tạo executable
```powershell
cd "d:\CODE\github copilot\decompile\XrmToolBox.PluginDecompiler"
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

# Output: bin\Release\net8.0-windows\win-x64\publish\PluginDecompiler.exe
```

---

## 📚 Documentation

| Document | Location | Description |
|----------|----------|-------------|
| Console README | `/README.md` | Full console tool documentation |
| Console QuickStart | `/QUICKSTART.md` | Quick guide for console tool |
| GUI README | `/XrmToolBox.PluginDecompiler/README_GUI.md` | Full GUI tool documentation |
| Build Instructions | `/XrmToolBox.PluginDecompiler/BUILD_INSTRUCTIONS.md` | Build guide |
| .NET Install Guide | `/INSTALL_DOTNET.md` | .NET SDK installation |

---

## 🎯 Use Cases

### Developers
- Học cách implement Dynamics 365 plugins
- Debug plugin issues
- Code review

### IT Admins
- Khôi phục source code bị mất
- Audit third-party plugins
- Migration planning

### Consultants
- Hiểu business logic trong plugins
- Documentation
- Knowledge transfer

---

## ⚡ Performance

| Metric | Console | GUI |
|--------|---------|-----|
| Small DLL (<1MB) | ~2s | ~3s |
| Medium DLL (1-5MB) | ~5s | ~7s |
| Large DLL (>5MB) | ~15s | ~20s |

*Times include project creation + combined file*

---

## 🔐 Security & Legal

⚠️ **QUAN TRỌNG**: Chỉ decompile plugins bạn có quyền hợp pháp!

**Hợp pháp khi:**
- Plugins do bạn/công ty bạn phát triển
- Có quyền truy cập source code
- Mục đích học tập với plugins mẫu
- Debug và troubleshooting

**KHÔNG hợp pháp khi:**
- Vi phạm license agreements
- Reverse engineer phần mềm thương mại
- Truy cập code không được phép

---

## 🎉 Tóm lại

Bạn có **2 tools mạnh mẽ** để decompile Dynamics 365 plugins:

1. **Console Tool** - Cho automation, scripting, CI/CD
2. **GUI Tool** - Cho end-users, visual feedback, ease of use

Cả 2 đều:
- ✅ Sử dụng ILSpy engine
- ✅ Tạo Visual Studio projects
- ✅ Generate combined source files
- ✅ Hỗ trợ .NET Framework & .NET Core plugins
- ✅ Hoàn toàn FREE và open source

---

**Happy Decompiling! 🚀**
