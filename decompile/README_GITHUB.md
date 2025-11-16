# Dynamics 365 Plugin Decompiler

Bộ công cụ decompile plugin DLL của Dynamics 365 CRM/CE sang mã nguồn C#.

## 🎯 2 Công cụ được cung cấp

### 1. 🖥️ GUI Tool (Windows Forms) - **Khuyến nghị**
- Giao diện đồ họa đẹp mắt, dễ sử dụng
- Drag & Drop support
- Real-time colored logging
- Perfect cho end-users

**Vị trí**: `XrmToolBox.PluginDecompiler/`

### 2. 💻 Console Tool (CLI)
- Command-line interface
- Automation & scripting friendly
- Batch processing support
- Perfect cho developers & CI/CD

**Vị trí**: Root folder

## 🚀 Quick Start

### GUI Tool (Dễ nhất)
```powershell
cd XrmToolBox.PluginDecompiler
dotnet run
```

### Console Tool
```powershell
dotnet run -- "MyPlugin.dll"
dotnet run -- "MyPlugin.dll" "OutputFolder"
```

## 📦 Build

### Build GUI Tool
```powershell
cd XrmToolBox.PluginDecompiler
dotnet build -c Release
```

### Build Console Tool
```powershell
dotnet build -c Release
```

## ✨ Tính năng

- ✅ Decompile DLL thành C# source code dễ đọc
- ✅ Tạo Visual Studio project structure
- ✅ Generate combined source file
- ✅ Hỗ trợ .NET Framework và .NET Core plugins
- ✅ Loại bỏ dead code và optimize output
- ✅ XML documentation preserved
- ✅ Drag & drop support (GUI)
- ✅ Real-time progress tracking (GUI)

## 📋 Yêu cầu

- .NET 8.0 SDK
- Windows 10/11

## 📚 Documentation

- [PROJECT_SUMMARY.md](PROJECT_SUMMARY.md) - Tổng quan project
- [QUICKSTART.md](QUICKSTART.md) - Hướng dẫn nhanh
- [XrmToolBox.PluginDecompiler/README_GUI.md](XrmToolBox.PluginDecompiler/README_GUI.md) - GUI tool docs
- [INSTALL_DOTNET.md](INSTALL_DOTNET.md) - Cài đặt .NET SDK

## 🎯 Use Cases

- 🎓 Học hỏi từ plugin có sẵn
- 🔧 Debug và troubleshooting
- 💾 Khôi phục source code bị mất
- 👀 Code review
- 🔄 Migration planning

## 📸 Screenshots

### GUI Tool
![GUI Tool Interface](screenshots/gui-interface.png)

### Output Structure
```
MyPlugin_Decompiled/
├── MyPlugin.csproj
├── MyPlugin_Combined.cs
├── Plugins/
│   ├── PreOperationPlugin.cs
│   └── PostOperationPlugin.cs
└── Services/
    └── HelperService.cs
```

## 🛠️ Công nghệ

- **.NET 8.0** - Modern framework
- **ICSharpCode.Decompiler** - ILSpy engine
- **Windows Forms** - Native Windows GUI
- **C# 12** - Latest features

## 📝 License

MIT License - Free for educational and legal use

## ⚠️ Disclaimer

**Chỉ decompile các plugin bạn có quyền truy cập hợp pháp!**

Tool này dành cho:
- ✅ Học tập và nghiên cứu
- ✅ Khôi phục source code của chính bạn
- ✅ Debug và troubleshooting hợp pháp

Không dùng cho:
- ❌ Vi phạm bản quyền
- ❌ Reverse engineer phần mềm thương mại trái phép

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📧 Contact

Created for Dynamics 365 developers community.

---

**Happy Decompiling! 🚀**
