# API Tester - Giải pháp thay thế Postman

API Tester là một ứng dụng C# WPF giúp bạn test API một cách dễ dàng, tương tự như Postman nhưng chạy hoàn toàn offline và không cần đăng nhập.

> **⚡ Hướng dẫn nhanh cho Windows:** Xem file [WINDOWS_QUICKSTART.md](WINDOWS_QUICKSTART.md) để chạy ngay trong 3 bước đơn giản!

## Tính năng chính

### 🚀 Gửi HTTP Requests
- Hỗ trợ các phương thức: GET, POST, PUT, DELETE, PATCH, HEAD, OPTIONS
- Tùy chỉnh Headers
- Gửi Body với nhiều định dạng: JSON, XML, Raw text, Form Data
- Xem Response với formatting tự động (JSON)
- Hiển thị thời gian response và kích thước dữ liệu

### 📋 Import từ cURL
- Paste lệnh cURL và tự động chuyển đổi thành API request
- Tự động phân tích headers, body, method
- Hỗ trợ cả cURL từ Chrome DevTools, Firefox, và các công cụ khác

### 🌍 Quản lý Environment Variables
- Tạo nhiều môi trường (Development, Staging, Production, etc.)
- Định nghĩa biến với cú pháp `{{variable_name}}`
- Tự động thay thế biến trong URL, Headers, Body
- Lưu trữ API keys, base URLs một cách an toàn

**Ví dụ sử dụng biến:**
```
URL: {{base_url}}/api/users
Header: Authorization: Bearer {{api_key}}
```

### 💾 Collections & History
- Tổ chức requests thành collections
- Lưu trữ tất cả requests để sử dụng lại
- Xem lại lịch sử các request đã gửi
- Import/Export collections dưới dạng JSON

### 💡 Các tính năng khác
- Lưu trữ local, không cần internet
- Không yêu cầu đăng nhập
- Giao diện thân thiện, dễ sử dụng
- Hỗ trợ authentication: Bearer Token, Basic Auth, API Key

## Yêu cầu hệ thống

- Windows 10 hoặc cao hơn
- .NET 6.0 Runtime hoặc cao hơn

## Cài đặt

### Cách 1: Cài đặt .NET SDK và build từ source

1. **Tải và cài đặt .NET SDK:**
   - Truy cập: https://dotnet.microsoft.com/download
   - Tải bản .NET 6.0 SDK hoặc cao hơn
   - Chạy file cài đặt và làm theo hướng dẫn

2. **Build ứng dụng:**
   ```powershell
   cd "c:\Users\cnguyen2\Desktop\New folder\ApiTester"
   dotnet restore
   dotnet build
   ```

3. **Chạy ứng dụng:**
   ```powershell
   dotnet run
   ```

### Cách 2: Build file exe

```powershell
cd "c:\Users\cnguyen2\Desktop\New folder\ApiTester"
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

File exe sẽ nằm trong thư mục: `bin\Release\net6.0-windows\win-x64\publish\ApiTester.exe`

## Hướng dẫn sử dụng

### 1. Gửi một API request đơn giản

1. Chọn HTTP Method (GET, POST, etc.)
2. Nhập URL của API
3. Nhấn nút **Send**
4. Xem kết quả trong tab **Response**

### 2. Thêm Headers

1. Chuyển đến tab **Headers**
2. Nhập headers theo định dạng:
   ```
   Content-Type: application/json
   Authorization: Bearer your-token-here
   X-Custom-Header: value
   ```

### 3. Gửi Body với POST/PUT request

1. Chuyển đến tab **Body**
2. Nhập dữ liệu JSON:
   ```json
   {
     "name": "John Doe",
     "email": "john@example.com"
   }
   ```

### 4. Import từ cURL

1. Copy lệnh cURL từ Chrome DevTools hoặc nơi khác:
   ```bash
   curl 'https://api.example.com/users' \
     -H 'Authorization: Bearer token123' \
     -H 'Content-Type: application/json' \
     --data-raw '{"name":"John"}'
   ```

2. Chuyển đến tab **Import cURL**
3. Paste lệnh cURL vào
4. Nhấn **Import**
5. Request sẽ được tự động điền vào form

### 5. Tạo và sử dụng Environment Variables

1. Nhấn nút **New Environment**
2. Một environment mới sẽ được tạo với các biến mẫu
3. Chỉnh sửa file JSON trong thư mục:
   `%APPDATA%\ApiTester\environments\`
   
   Ví dụ:
   ```json
   {
     "Id": "abc-123",
     "Name": "Development",
     "Variables": {
       "base_url": "https://api-dev.example.com",
       "api_key": "dev-key-12345",
       "user_id": "123"
     },
     "IsActive": false
   }
   ```

4. Chọn environment từ dropdown ở menu trên
5. Sử dụng biến trong requests:
   ```
   URL: {{base_url}}/users/{{user_id}}
   Header: X-API-Key: {{api_key}}
   ```

### 6. Lưu Request vào Collection

1. Nhấn **New Collection** để tạo collection mới
2. Điền thông tin request
3. Nhấn nút **Save**
4. Request sẽ được lưu vào collection đã chọn

### 7. Sử dụng lại Request từ History

1. Xem danh sách requests trong phần **History** (bên trái)
2. Double-click vào request muốn sử dụng lại
3. Request sẽ được tự động load lên form

## Cấu trúc thư mục lưu trữ

Dữ liệu được lưu tại:
```
%APPDATA%\ApiTester\
├── collections\      # Các collections đã lưu
└── environments\     # Các environments đã tạo
```

## Ví dụ thực tế

### Test API với JSONPlaceholder

1. **GET Request:**
   - Method: GET
   - URL: `https://jsonplaceholder.typicode.com/posts/1`
   - Nhấn Send

2. **POST Request:**
   - Method: POST
   - URL: `https://jsonplaceholder.typicode.com/posts`
   - Headers:
     ```
     Content-Type: application/json
     ```
   - Body:
     ```json
     {
       "title": "Test Post",
       "body": "This is a test",
       "userId": 1
     }
     ```
   - Nhấn Send

### Test với Environment Variables

1. Tạo environment "JSONPlaceholder":
   ```json
   {
     "Name": "JSONPlaceholder",
     "Variables": {
       "base_url": "https://jsonplaceholder.typicode.com",
       "user_id": "1"
     }
   }
   ```

2. Sử dụng trong request:
   - URL: `{{base_url}}/users/{{user_id}}`

## Lưu ý

- Dữ liệu được lưu trữ local trên máy của bạn
- Không có giới hạn số lượng requests hoặc collections
- Có thể export/import collections để chia sẻ với team

## Khắc phục sự cố

### Lỗi "dotnet not recognized"
- Bạn chưa cài đặt .NET SDK
- Tải và cài đặt từ: https://dotnet.microsoft.com/download

### Không thể gửi HTTPS request
- Kiểm tra firewall và antivirus
- Đảm bảo máy tính có kết nối internet

### Biến environment không được thay thế
- Kiểm tra đã chọn đúng environment trong dropdown
- Đảm bảo tên biến trong `{{}}` khớp với tên trong environment

## Ghi chú về môi trường phát triển (Linux vs Windows)

- Đây là một ứng dụng WPF targeting Windows (`net8.0-windows` và `UseWPF=true`). WPF là công nghệ UI Windows và không thể chạy hoặc xuất sang một EXE Windows trực tiếp trên Linux.
- Nếu bạn cố build trên Linux, bạn sẽ gặp lỗi giống như:
   ```
   error NETSDK1100: To build a project targeting Windows on this operating system, set the EnableWindowsTargeting property to true.
   ```

Hai cách để có file exe hoặc thử ứng dụng:

1) Build & chạy trên Windows (local)

    - Cài .NET 8.0 SDK trên Windows.
    - Trong PowerShell:
       ```powershell
       cd path\to\repo\test
       dotnet restore
       dotnet run
       ```

    - Hoặc publish self-contained exe:
       ```powershell
       dotnet publish ApiTester.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
       ```

2) Dùng GitHub Actions (workflow này đã được thêm vào `.github/workflows/publish-windows.yml`) để build và tải artifact exe từ runner Windows.

    - Push code lên branch `main` hoặc trigger workflow từ trang Actions.
    - Khi workflow chạy xong, tải artifact "ApiTester-win-x64" từ trang run để lấy folder chứa exe.

   ### Scripts helper cho Windows

   Trong thư mục `test/scripts/` có 2 script PowerShell tiện lợi:

   - `build-windows.ps1` — restore và publish self-contained single-file exe cho `win-x64`.
   - `run-windows.ps1` — chạy file exe được publish (mở bằng Start-Process).

   Ví dụ chạy trên Windows PowerShell (mở PowerShell với quyền user bình thường):

   ```powershell
   cd path\to\repo\test\scripts
   .\build-windows.ps1
   .\run-windows.ps1
   ```

   Nếu muốn tuỳ chỉnh cấu hình hoặc runtime:

   ```powershell
   .\build-windows.ps1 -Configuration Release -Runtime win-x64
   ```



## Đóng góp

Ứng dụng này là mã nguồn mở. Bạn có thể tùy chỉnh và mở rộng theo nhu cầu của mình.

## License

MIT License - Sử dụng tự do cho mục đích cá nhân và thương mại.
