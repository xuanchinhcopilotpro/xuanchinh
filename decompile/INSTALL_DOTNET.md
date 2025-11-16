# Cài đặt .NET SDK

Project này yêu cầu .NET 8.0 SDK để build và chạy.

## Kiểm tra .NET đã được cài đặt

Mở PowerShell hoặc Command Prompt và chạy:

```powershell
dotnet --version
```

Nếu bạn thấy version number (ví dụ: 8.0.x), .NET SDK đã được cài đặt.

## Cài đặt .NET 8.0 SDK

### Cách 1: Tải từ trang chính thức (Khuyến nghị)

1. Truy cập: https://dotnet.microsoft.com/download/dotnet/8.0
2. Tải **SDK** (không phải Runtime) cho Windows
3. Chọn phiên bản x64 installer
4. Chạy installer và làm theo hướng dẫn
5. Khởi động lại terminal sau khi cài đặt

### Cách 2: Sử dụng winget (Windows 11)

```powershell
winget install Microsoft.DotNet.SDK.8
```

### Cách 3: Sử dụng Chocolatey

```powershell
choco install dotnet-8.0-sdk
```

## Sau khi cài đặt

1. Mở terminal mới (PowerShell hoặc Command Prompt)
2. Kiểm tra lại:
   ```powershell
   dotnet --version
   ```
3. Quay lại thư mục project và chạy:
   ```powershell
   dotnet restore
   dotnet build
   ```

## Các version .NET được hỗ trợ

- **.NET 8.0** (Khuyến nghị) - LTS version
- **.NET 6.0** hoặc cao hơn cũng có thể hoạt động

Nếu bạn muốn sử dụng .NET 6.0, hãy sửa file `DllDecompiler.csproj`:
```xml
<TargetFramework>net6.0</TargetFramework>
```

## Troubleshooting

### Lỗi: "dotnet is not recognized"

- Đảm bảo bạn đã cài SDK (không phải Runtime)
- Khởi động lại terminal
- Kiểm tra biến môi trường PATH có chứa đường dẫn đến dotnet

### Lỗi phiên bản

Nếu bạn có phiên bản .NET cũ hơn, update lên version mới nhất:
- Tải installer mới từ trang chính thức
- Hoặc sử dụng: `dotnet tool update -g dotnet`

## Liên kết hữu ích

- [.NET Downloads](https://dotnet.microsoft.com/download)
- [.NET 8.0 Documentation](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8)
- [Install .NET on Windows](https://learn.microsoft.com/en-us/dotnet/core/install/windows)
