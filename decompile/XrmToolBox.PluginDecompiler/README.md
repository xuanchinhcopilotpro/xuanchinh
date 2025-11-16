# XrmToolBox Plugin Decompiler

Plugin cho XrmToolBox để decompile các file DLL plugin của Dynamics 365 CRM/CE sang mã nguồn C#.

## Tính năng

- ✅ **Giao diện thân thiện** - Tích hợp ngay trong XrmToolBox
- ✅ **Decompile nhanh** - Chuyển DLL thành C# source code dễ đọc
- ✅ **Project structure** - Tạo project hoàn chỉnh có thể mở bằng Visual Studio
- ✅ **Combined file** - File tổng hợp toàn bộ code để xem nhanh
- ✅ **Auto open** - Tự động mở thư mục output sau khi decompile
- ✅ **Log chi tiết** - Theo dõi quá trình decompile real-time

## Cài đặt

### Yêu cầu

- XrmToolBox version 1.2023.12.71 hoặc cao hơn
- .NET Framework 4.6.2 hoặc cao hơn

### Cách 1: Cài từ XrmToolBox Tool Library (Khuyến nghị)

1. Mở XrmToolBox
2. Click **Tool Library**
3. Tìm kiếm "**Plugin Decompiler**"
4. Click **Install**

### Cách 2: Cài thủ công

1. Build project này
2. Copy các file sau vào thư mục `Plugins` của XrmToolBox:
   ```
   XrmToolBox\Plugins\
   ├── XrmToolBox.PluginDecompiler.dll
   ├── ICSharpCode.Decompiler.dll
   ├── System.Collections.Immutable.dll
   ├── System.Reflection.Metadata.dll
   └── PluginManifest.json
   ```
3. Khởi động lại XrmToolBox

## Build Plugin

### Bước 1: Build project

```powershell
cd "d:\CODE\github copilot\decompile\XrmToolBox.PluginDecompiler"
dotnet restore
dotnet build -c Release
```

### Bước 2: Copy dependencies

Sau khi build, copy tất cả DLL từ `bin\Release\net462\` vào thư mục Plugins của XrmToolBox.

**Vị trí mặc định của XrmToolBox Plugins:**
```
%AppData%\MscrmTools\XrmToolBox\Plugins\
```

## Sử dụng

### 1. Mở Plugin trong XrmToolBox

1. Khởi động XrmToolBox
2. Tìm và mở "**Plugin Decompiler**" trong danh sách tools

### 2. Decompile Plugin DLL

1. Click **Browse** bên cạnh "Plugin DLL File"
2. Chọn file DLL plugin cần decompile
3. (Tùy chọn) Chọn thư mục output
4. Click **Decompile Plugin**

### 3. Xem kết quả

Plugin sẽ tạo:
- **Project files** - Có thể mở bằng Visual Studio
- **Combined source file** - Toàn bộ code trong 1 file .cs
- **Organized structure** - Code được tổ chức theo namespace

## Cấu trúc Output

```
MyPlugin_Decompiled/
├── MyPlugin.csproj              # Project file
├── MyPlugin_Combined.cs         # Toàn bộ code trong 1 file
├── Plugins/
│   ├── PreOperationPlugin.cs
│   ├── PostOperationPlugin.cs
│   └── ValidationPlugin.cs
└── Services/
    └── HelperService.cs
```

## Options

- **Create combined source file** - Tạo file .cs chứa toàn bộ code
- **Open output folder after decompilation** - Tự động mở folder sau khi xong

## Troubleshooting

### Plugin không xuất hiện trong XrmToolBox

1. Kiểm tra version XrmToolBox (cần >= 1.2023.12.71)
2. Đảm bảo tất cả DLL dependencies đã được copy
3. Khởi động lại XrmToolBox

### Lỗi "Could not load file or assembly"

- Copy tất cả DLL từ `bin\Release\net462\` vào thư mục Plugins
- Đặc biệt là: ICSharpCode.Decompiler.dll và các dependencies

### Decompile không thành công

- Kiểm tra DLL có hợp lệ không
- Một số DLL bị obfuscated có thể decompile không hoàn toàn
- Xem log để biết chi tiết lỗi

## Build cho Production

```powershell
# Build release version
dotnet build -c Release

# Tạo package
cd bin\Release\net462
Compress-Archive -Path *.dll,PluginManifest.json -DestinationPath XrmToolBox.PluginDecompiler.zip
```

## Screenshots

### Giao diện chính
![Main Interface](screenshots/main-interface.png)

### Decompilation Log
![Decompilation Log](screenshots/decompilation-log.png)

## Công nghệ sử dụng

- **XrmToolBox SDK** - Framework cho XrmToolBox plugins
- **ICSharpCode.Decompiler** - Engine decompile mạnh mẽ
- **.NET Framework 4.6.2** - Platform
- **WinForms** - UI framework

## License

MIT License - Sử dụng cho mục đích học tập và phát triển hợp pháp.

## Changelog

### Version 1.0.0
- Initial release
- Decompile plugin DLL to C# source
- Create project structure
- Generate combined source file
- Integration with XrmToolBox

## Author

Created for Dynamics 365 developers to help with plugin development and debugging.

---

**Lưu ý:** Chỉ decompile các plugin mà bạn có quyền truy cập. Tôn trọng bản quyền và license.
