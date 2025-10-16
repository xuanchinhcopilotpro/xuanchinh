# Hướng dẫn nhanh - Chạy API Tester trên Windows

## Bước 1: Cài .NET SDK 8.0
1. Truy cập: https://dotnet.microsoft.com/download/dotnet/8.0
2. Tải "SDK 8.0.x" (Windows x64)
3. Chạy file cài đặt và làm theo hướng dẫn
4. Khởi động lại PowerShell/Terminal sau khi cài

## Bước 2: Build ứng dụng
Mở PowerShell và chạy:

```powershell
cd path\to\repo\test\scripts
.\build-windows.ps1
```

**Lưu ý:** Thay `path\to\repo` bằng đường dẫn thật của bạn (ví dụ: `C:\Users\YourName\Documents\xuanchinh`)

## Bước 3: Chạy ứng dụng
```powershell
.\run-windows.ps1
```

Hoặc mở trực tiếp file exe tại: `test\bin\publish\ApiTester.exe`

---

## Cách khác (nhanh hơn - chỉ để test)
Nếu chỉ muốn chạy nhanh để test (không cần tạo file .exe):

```powershell
cd path\to\repo\test
dotnet run
```

---

## Nếu gặp lỗi
**Lỗi: "dotnet not recognized"**
- Bạn chưa cài .NET SDK hoặc chưa khởi động lại PowerShell
- Cài lại từ: https://dotnet.microsoft.com/download/dotnet/8.0

**Lỗi: "Execution of scripts is disabled"**
Chạy lệnh này trong PowerShell (as Administrator):
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

**Lỗi khác**
Xem file README.md để biết thêm chi tiết hoặc liên hệ hỗ trợ.

---

## Tóm tắt siêu nhanh
Nếu đã có .NET SDK 8.0:
```powershell
# Tại thư mục test/scripts
.\build-windows.ps1
.\run-windows.ps1
```

Xong! Ứng dụng sẽ mở.
