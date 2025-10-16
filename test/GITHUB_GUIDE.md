# Hướng dẫn Push Code lên GitHub

## Bước 1: Cài đặt Git

1. Tải Git cho Windows từ: https://git-scm.com/download/win
2. Chạy file cài đặt và làm theo hướng dẫn (chọn mặc định)
3. Sau khi cài xong, mở lại PowerShell mới

## Bước 2: Cấu hình Git (chỉ làm 1 lần)

Mở PowerShell và chạy:

```powershell
git config --global user.name "Tên của bạn"
git config --global user.email "email@example.com"
```

## Bước 3: Tạo Repository trên GitHub

1. Đăng nhập vào GitHub: https://github.com
2. Nhấn nút **"New"** hoặc **"+"** ở góc trên bên phải
3. Chọn **"New repository"**
4. Điền thông tin:
   - Repository name: `api-tester` (hoặc tên bạn muốn)
   - Description: `API Testing Tool - Alternative to Postman`
   - Chọn **Public** hoặc **Private**
   - **KHÔNG** chọn "Initialize this repository with a README"
5. Nhấn **"Create repository"**

## Bước 4: Push Code lên GitHub

Mở PowerShell tại thư mục project và chạy các lệnh sau:

### 4.1. Di chuyển vào thư mục project

```powershell
cd "c:\Users\cnguyen2\Desktop\New folder\ApiTester"
```

### 4.2. Khởi tạo Git repository

```powershell
git init
```

### 4.3. Thêm tất cả files vào Git

```powershell
git add .
```

### 4.4. Tạo commit đầu tiên

```powershell
git commit -m "Initial commit: API Tester application with full features"
```

### 4.5. Đổi tên branch thành main

```powershell
git branch -M main
```

### 4.6. Kết nối với GitHub repository

**Thay `YOUR_USERNAME` và `YOUR_REPO_NAME` bằng thông tin thực tế của bạn:**

```powershell
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO_NAME.git
```

Ví dụ:
```powershell
git remote add origin https://github.com/johndoe/api-tester.git
```

### 4.7. Push code lên GitHub

```powershell
git push -u origin main
```

**Lưu ý:** Lần đầu push, GitHub sẽ yêu cầu đăng nhập:
- Có thể cần tạo **Personal Access Token** thay vì password
- Tạo token tại: https://github.com/settings/tokens
- Chọn scope: `repo` (full control of private repositories)
- Copy token và dùng làm password khi push

## Bước 5: Các lệnh Git hữu ích

### Kiểm tra trạng thái
```powershell
git status
```

### Xem lịch sử commit
```powershell
git log
```

### Thêm và commit thay đổi
```powershell
git add .
git commit -m "Mô tả thay đổi"
git push
```

### Tạo file .gitignore (đã có sẵn trong project)
File này giúp Git bỏ qua các file không cần thiết như `bin/`, `obj/`, `.vs/`

## Bước 6: Tạo README đẹp trên GitHub

File README.md đã có sẵn sẽ tự động hiển thị trên trang GitHub repository của bạn.

## Cấu trúc Repository sau khi push

```
api-tester/
├── .gitignore
├── README.md
├── ApiTester.csproj
├── App.xaml
├── App.xaml.cs
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── ImportCurlDialog.xaml
├── ImportCurlDialog.xaml.cs
├── Models/
│   ├── ApiRequest.cs
│   ├── ApiResponse.cs
│   ├── Environment.cs
│   └── RequestCollection.cs
├── Services/
│   ├── HttpService.cs
│   ├── CurlParser.cs
│   ├── EnvironmentService.cs
│   └── StorageService.cs
├── ViewModels/
│   ├── ViewModelBase.cs
│   ├── RelayCommand.cs
│   └── MainViewModel.cs
└── Converters/
    └── BoolToVisibilityConverter.cs
```

## Khắc phục sự cố

### Lỗi: "git is not recognized"
- Git chưa được cài đặt hoặc chưa thêm vào PATH
- Cài đặt Git và khởi động lại PowerShell

### Lỗi: "Permission denied (publickey)"
- Sử dụng HTTPS thay vì SSH
- Hoặc tạo SSH key: https://docs.github.com/en/authentication/connecting-to-github-with-ssh

### Lỗi: "Authentication failed"
- Không thể dùng password GitHub trực tiếp
- Phải tạo Personal Access Token
- Link tạo: https://github.com/settings/tokens

### Lỗi: "remote origin already exists"
```powershell
git remote remove origin
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO_NAME.git
```

## Script tự động (sau khi đã cài Git)

Tạo file `push.ps1` với nội dung:

```powershell
# Thêm tất cả thay đổi
git add .

# Commit với message
$message = Read-Host "Nhập commit message"
git commit -m $message

# Push lên GitHub
git push

Write-Host "Done! Code đã được push lên GitHub" -ForegroundColor Green
```

Chạy:
```powershell
.\push.ps1
```

## Chia sẻ Repository

Sau khi push thành công, bạn có thể chia sẻ link repository:
```
https://github.com/YOUR_USERNAME/api-tester
```

Người khác có thể clone về bằng:
```powershell
git clone https://github.com/YOUR_USERNAME/api-tester.git
```

---

**Chúc bạn thành công!** 🚀

Nếu gặp vấn đề, hãy kiểm tra:
1. Git đã được cài đặt chưa: `git --version`
2. Đã đăng nhập GitHub chưa
3. Repository đã được tạo trên GitHub chưa
