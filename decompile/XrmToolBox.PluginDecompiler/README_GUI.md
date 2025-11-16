# 🔧 Dynamics 365 Plugin Decompiler - Windows Forms Tool

Tool GUI đẹp mắt để decompile plugin DLL của Dynamics 365 CRM/CE sang mã nguồn C#.

## ✨ Tính năng

- ✅ **Giao diện đồ họa đẹp mắt** - Windows Forms với thiết kế hiện đại
- ✅ **Drag & Drop hỗ trợ** - Kéo thả DLL file vào ô input
- ✅ **Decompile nhanh** - Sử dụng ILSpy engine mạnh mẽ
- ✅ **Project structure** - Tạo project Visual Studio hoàn chỉnh
- ✅ **Combined file** - File tổng hợp toàn bộ code để xem nhanh
- ✅ **Real-time log** - Theo dõi quá trình decompile với màu sắc
- ✅ **Auto open** - Tự động mở folder output
- ✅ **Không cần XrmToolBox** - Standalone application

## 📦 Download & Sử dụng

### Cách 1: Chạy trực tiếp (Cần .NET 8.0 Runtime)

1. Tải .NET 8.0 Desktop Runtime: https://dotnet.microsoft.com/download/dotnet/8.0
2. Chạy file executable:
   ```
   bin\Release\net8.0-windows\win-x64\publish\PluginDecompiler.exe
   ```

### Cách 2: Build từ source

```powershell
cd "d:\CODE\github copilot\decompile\XrmToolBox.PluginDecompiler"
dotnet restore
dotnet build -c Release
dotnet run
```

## 🚀 Hướng dẫn sử dụng

### 1. Khởi động ứng dụng

Chạy `PluginDecompiler.exe`

### 2. Chọn file DLL

- Click nút **Browse...** hoặc
- **Kéo thả** file DLL vào ô "Plugin DLL File"

### 3. Chọn thư mục output (tùy chọn)

- Để trống để tự động tạo thư mục `[TênPlugin]_Decompiled`
- Hoặc chọn thư mục tùy chỉnh

### 4. Tùy chọn

- ☑ **Create combined source file** - Tạo file .cs chứa toàn bộ code
- ☑ **Open output folder after decompilation** - Tự động mở folder

### 5. Click **🔄 Decompile Plugin**

Theo dõi quá trình trong log và đợi hoàn tất!

## 📸 Screenshots

### Giao diện chính
```
┌─────────────────────────────────────────────────────────┐
│  🔧 Dynamics 365 Plugin Decompiler                      │
│  Decompile plugin DLL files to readable C# source code │
├─────────────────────────────────────────────────────────┤
│  Plugin DLL File:                                       │
│  [C:\Plugins\MyPlugin.dll              ] [Browse...]    │
│                                                         │
│  Output Folder:                                         │
│  [C:\Plugins\MyPlugin_Decompiled       ] [Browse...]    │
│                                                         │
│  ☑ Create combined source file                         │
│  ☑ Open output folder after decompilation              │
│                                                         │
│  [        🔄 Decompile Plugin                    ]      │
│                                                         │
│  Decompilation Log:                                     │
│  ┌───────────────────────────────────────────────┐     │
│  │ Starting decompilation...                     │     │
│  │ Input: C:\Plugins\MyPlugin.dll                │     │
│  │ Output: C:\Plugins\MyPlugin_Decompiled        │     │
│  │                                               │     │
│  │ 📦 Assembly: MyPlugin                         │     │
│  │ 🎯 Target framework: .NETFramework,Version=v4.6.2  │
│  │ 🔄 Decompiling to project structure...        │     │
│  │ ✓ Project files created                       │     │
│  │ 📝 Creating combined source file...           │     │
│  │ ✓ Combined file created                       │     │
│  │                                               │     │
│  │ ✅ Decompilation completed successfully!      │     │
│  └───────────────────────────────────────────────┘     │
└─────────────────────────────────────────────────────────┘
```

## 📁 Cấu trúc Output

```
MyPlugin_Decompiled/
├── MyPlugin.csproj              # Visual Studio project file
├── MyPlugin_Combined.cs         # Toàn bộ code trong 1 file
├── Plugins/
│   ├── PreOperationPlugin.cs    # Plugin classes
│   ├── PostOperationPlugin.cs
│   └── ValidationPlugin.cs
├── Services/
│   ├── HelperService.cs
│   └── LoggingService.cs
└── Models/
    └── CustomEntity.cs
```

## 🎯 Use Cases

### 1. Học hỏi từ plugin có sẵn
Decompile plugin mẫu để học cách implement

### 2. Khôi phục source code
Recover code khi mất file gốc

### 3. Debug và Troubleshooting
Hiểu logic bên trong plugin khi gặp lỗi

### 4. Code Review
Kiểm tra plugin từ bên thứ 3

### 5. Migration
Hiểu plugin cũ để migrate sang version mới

## 💡 Tips & Tricks

### Xem nhanh code
Sau khi decompile, mở file `*_Combined.cs` để xem toàn bộ code trong 1 file

### Mở trong Visual Studio
Double-click file `.csproj` để mở project với syntax highlighting

### Decompile nhiều DLL
Tool hỗ trợ decompile từng file một, có thể chạy nhiều instances

### Dependencies
Nếu DLL có dependencies, đặt tất cả trong cùng folder

## 🔧 Build Options

### Build Debug
```powershell
dotnet build
```

### Build Release
```powershell
dotnet build -c Release
```

### Publish Single File
```powershell
dotnet publish -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true
```

### Publish Self-Contained (Không cần .NET runtime)
```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

## 📋 Yêu cầu hệ thống

- **OS**: Windows 10/11
- **Runtime**: .NET 8.0 Desktop Runtime (nếu không build self-contained)
- **RAM**: 512MB trở lên
- **Disk**: 50MB cho application + space cho output

## 🐛 Troubleshooting

### Tool không khởi động
→ Cài .NET 8.0 Desktop Runtime

### Lỗi "Could not load file or assembly"
→ Đặt tất cả DLL dependencies vào cùng folder với plugin DLL

### Output thiếu code
→ DLL có thể bị obfuscated/protected

### Decompile chậm
→ DLL lớn sẽ mất thời gian, xem log để theo dõi tiến trình

## 📚 Công nghệ

- **.NET 8.0** - Modern cross-platform framework
- **Windows Forms** - Native Windows GUI
- **ICSharpCode.Decompiler** - ILSpy decompiler engine
- **C# 12** - Latest language features

## 🌟 So sánh với Console Version

| Tính năng | Console | WinForms GUI |
|-----------|---------|--------------|
| Giao diện đẹp | ❌ | ✅ |
| Drag & Drop | ❌ | ✅ |
| Real-time log | ❌ | ✅ |
| Progress bar | ❌ | ✅ |
| Dễ sử dụng | ⭐⭐ | ⭐⭐⭐⭐⭐ |
| Automation | ✅ | ❌ |
| CI/CD friendly | ✅ | ❌ |

## 📝 License

MIT License - Sử dụng cho mục đích học tập và phát triển hợp pháp.

## ⚠️ Lưu ý quan trọng

**Chỉ decompile các plugin mà bạn có quyền truy cập hợp pháp!**

Tool này dành cho:
- ✅ Học tập và nghiên cứu
- ✅ Khôi phục source code của chính bạn
- ✅ Debug và troubleshooting
- ✅ Code review hợp pháp

Không dùng cho:
- ❌ Vi phạm bản quyền
- ❌ Reverse engineer phần mềm thương mại
- ❌ Mục đích bất hợp pháp

---

**Tác giả**: Created for Dynamics 365 developers  
**Version**: 1.0.0  
**Ngày tạo**: 2025-10-23
