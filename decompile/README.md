# Dynamics 365 CRM/CE Plugin DLL Decompiler

Công cụ dịch ngược (decompile) các file DLL plugin của Dynamics 365 CRM/CE sang mã nguồn C#.

## Tính năng

- ✅ Dịch ngược DLL thành mã nguồn C# dễ đọc
- ✅ Hỗ trợ plugin Dynamics 365 CRM/CE
- ✅ Tạo project structure hoàn chỉnh
- ✅ Tạo file combined để xem nhanh toàn bộ code
- ✅ Hiển thị XML documentation nếu có
- ✅ Loại bỏ dead code và tối ưu output
- ✅ Hỗ trợ cả .NET Framework và .NET Core

## Yêu cầu

- .NET 8.0 SDK hoặc cao hơn
- Windows, Linux, hoặc macOS

## Cài đặt và Build

### 1. Restore dependencies và build project

```powershell
dotnet restore
dotnet build
```

### 2. Build project thành file executable

```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

File executable sẽ được tạo trong thư mục: `bin\Release\net8.0\win-x64\publish\`

## Sử dụng

### Cách 1: Chạy trực tiếp với dotnet

```powershell
dotnet run -- <đường-dẫn-dll> [thư-mục-output]
```

**Ví dụ:**

```powershell
# Decompile plugin DLL (output sẽ ở thư mục Decompiled cùng cấp với DLL)
dotnet run -- "C:\Plugins\MyCustomPlugin.dll"

# Chỉ định thư mục output
dotnet run -- "C:\Plugins\MyCustomPlugin.dll" "C:\Output\Decompiled"

# Decompile DLL trong thư mục hiện tại
dotnet run -- MyPlugin.dll
```

### Cách 2: Chạy file executable đã build

```powershell
# Sau khi build, chạy file .exe
.\bin\Release\net8.0\win-x64\publish\DllDecompiler.exe "C:\Plugins\MyCustomPlugin.dll"
```

## Kết quả Output

Sau khi decompile, bạn sẽ có:

1. **Project structure** - Các file .cs được tổ chức theo namespace
2. **File .csproj** - Project file để có thể mở bằng Visual Studio
3. **File Combined** - Một file .cs chứa toàn bộ source code để xem nhanh

### Ví dụ cấu trúc output:

```
Decompiled/
├── MyCustomPlugin.csproj
├── MyCustomPlugin_Combined.cs
├── Plugins/
│   ├── PreOperationPlugin.cs
│   ├── PostOperationPlugin.cs
│   └── ValidationPlugin.cs
├── Services/
│   └── HelperService.cs
└── Models/
    └── CustomEntity.cs
```

## Ứng dụng cho Dynamics 365

### Các trường hợp sử dụng phổ biến:

1. **Học hỏi từ plugin có sẵn** - Xem cách các plugin khác được implement
2. **Debug và troubleshooting** - Hiểu logic bên trong plugin khi gặp lỗi
3. **Reverse engineering** - Khôi phục source code khi mất file gốc
4. **Code review** - Kiểm tra các plugin từ bên thứ 3
5. **Migration** - Hiểu rõ plugin cũ để migrate sang version mới

### Lưu ý về Dynamics 365 Plugins:

- Plugin DLL thường chứa các class implement `IPlugin` interface
- Có thể chứa dependencies như Microsoft.Xrm.Sdk
- Tool sẽ cố gắng resolve các dependencies tự động

## Troubleshooting

### Lỗi: "Could not load file or assembly"

Nếu DLL có dependencies, đảm bảo các DLL dependency cùng thư mục với DLL cần decompile.

### Lỗi: "File not found"

Kiểm tra đường dẫn file DLL, nếu có khoảng trắng hãy đặt trong dấu ngoặc kép: `"path with spaces.dll"`

### Output không đầy đủ

Một số phần của code có thể bị obfuscated hoặc protected. Tool sẽ cố gắng decompile tốt nhất có thể.

## Best Practices

1. **Backup** - Luôn giữ bản backup của DLL gốc
2. **Legal** - Chỉ decompile các DLL bạn có quyền truy cập
3. **Dependencies** - Đặt tất cả DLL liên quan trong cùng thư mục
4. **Output folder** - Sử dụng thư mục output riêng để dễ quản lý

## Công nghệ sử dụng

- **ICSharpCode.Decompiler** - Library decompile mạnh mẽ (ILSpy engine)
- **.NET 8.0** - Framework hiện đại, cross-platform
- **C#** - Ngôn ngữ chính

## License

Tool này sử dụng ICSharpCode.Decompiler (MIT License) và chỉ dành cho mục đích học tập và phát triển hợp pháp.

## Liên hệ và đóng góp

Nếu bạn gặp vấn đề hoặc muốn cải thiện tool, hãy tạo issue hoặc pull request.

---

**Lưu ý quan trọng:** Công cụ này chỉ nên được sử dụng cho các mục đích hợp pháp như học tập, debugging, và khôi phục source code của chính bạn. Hãy tôn trọng bản quyền và license của các phần mềm.
