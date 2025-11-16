# Hướng dẫn Build XrmToolBox Plugin

## Phương án 1: Standalone Windows Forms Tool (Khuyến nghị - Dễ nhất)

Thay vì tích hợp vào XrmToolBox, tôi đã tạo một **Windows Forms application độc lập** mà bạn có thể chạy riêng.

### Build Standalone Tool

```powershell
cd "d:\CODE\github copilot\decompile\XrmToolBox.PluginDecompiler"
dotnet build -c Release
```

### Chạy tool

```powershell
cd bin\Release\net462
.\XrmToolBox.PluginDecompiler.exe
```

Tool này có:
- ✅ Giao diện WinForms đẹp mắt
- ✅ Dễ sử dụng với drag & drop
- ✅ Không cần XrmToolBox
- ✅ Chạy độc lập

---

## Phương án 2: Tích hợp vào XrmToolBox (Phức tạp hơn)

Để tích hợp vào XrmToolBox, bạn cần:

### Bước 1: Download XrmToolBox DLLs

1. Tải XrmToolBox: https://www.xrmtoolbox.com/
2. Cài đặt XrmToolBox
3. Copy các DLL sau từ thư mục XrmToolBox vào `d:\CODE\github copilot\decompile\libs\`:
   ```
   - XrmToolBox.PluginBase.dll
   - McTools.Xrm.Connection.dll
   - XrmToolBox.Extensibility.dll
   ```

### Bước 2: Build Plugin

```powershell
cd "d:\CODE\github copilot\decompile\XrmToolBox.PluginDecompiler"
dotnet restore
dotnet build -c Release
```

### Bước 3: Deploy to XrmToolBox

```powershell
# Chạy script tự động
.\build-plugin.bat
```

Hoặc copy thủ công:
```powershell
$XrmToolBoxPlugins = "$env:AppData\MscrmTools\XrmToolBox\Plugins"
Copy-Item "bin\Release\net462\*.dll" $XrmToolBoxPlugins -Force
Copy-Item "PluginManifest.json" $XrmToolBoxPlugins -Force
```

### Bước 4: Restart XrmToolBox

Khởi động lại XrmToolBox và tìm "Plugin Decompiler" trong danh sách tools.

---

## Troubleshooting

### Error: Cannot find XrmToolBox.PluginBase

➡️ Download XrmToolBox và copy các DLL cần thiết vào thư mục `libs`

### Build thành công nhưng plugin không xuất hiện

➡️ Kiểm tra:
1. XrmToolBox version >= 1.2023.12.71
2. Tất cả DLL đã được copy đúng
3. PluginManifest.json có trong thư mục Plugins

### Lỗi khi chạy plugin

➡️ Đảm bảo copy tất cả dependencies:
- ICSharpCode.Decompiler.dll
- System.Collections.Immutable.dll
- System.Reflection.Metadata.dll

---

## Khuyến nghị

**Sử dụng Phương án 1 (Standalone)** vì:
- ✅ Đơn giản, không cần XrmToolBox
- ✅ Chạy độc lập, dễ deploy
- ✅ Không phụ thuộc vào version XrmToolBox
- ✅ Có thể share cho người khác dễ dàng

**Chỉ dùng Phương án 2** nếu bạn:
- Đã có XrmToolBox và muốn tất cả tools trong 1 chỗ
- Cần tích hợp với CRM connection của XrmToolBox
- Quen với workflow của XrmToolBox
