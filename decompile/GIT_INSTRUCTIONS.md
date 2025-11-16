# Hướng dẫn Push lên GitHub

## Bước 1: Cài đặt Git (nếu chưa có)

### Download Git
Tải Git for Windows tại: https://git-scm.com/download/win

### Cài đặt
1. Chạy installer
2. Chọn các options mặc định
3. Khởi động lại terminal sau khi cài

### Kiểm tra cài đặt
```powershell
git --version
```

## Bước 2: Cấu hình Git (lần đầu)

```powershell
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"
```

## Bước 3: Tạo Repository trên GitHub

1. Đăng nhập GitHub: https://github.com
2. Click nút **New** hoặc **Create repository**
3. Đặt tên repository: `dynamics365-plugin-decompiler`
4. Chọn **Public** hoặc **Private**
5. **KHÔNG** chọn "Initialize with README"
6. Click **Create repository**
7. Copy URL của repo (ví dụ: `https://github.com/username/dynamics365-plugin-decompiler.git`)

## Bước 4: Push code lên GitHub

### Cách 1: Sử dụng script tự động (Dễ nhất)

```powershell
cd "d:\CODE\github copilot\decompile"
.\git-push.bat
```

Script sẽ hỏi:
1. Commit message (có thể để trống)
2. GitHub repository URL

### Cách 2: Manual commands

```powershell
cd "d:\CODE\github copilot\decompile"

# Khởi tạo git repo
git init

# Add tất cả files
git add .

# Commit
git commit -m "Initial commit - Dynamics 365 Plugin Decompiler"

# Add remote (thay YOUR_REPO_URL bằng URL thực tế)
git remote add origin https://github.com/yourusername/dynamics365-plugin-decompiler.git

# Rename branch to main
git branch -M main

# Push
git push -u origin main
```

## Bước 5: Xác thực GitHub

### Option 1: Personal Access Token (Khuyến nghị)

1. Vào GitHub → Settings → Developer settings → Personal access tokens → Tokens (classic)
2. Click **Generate new token (classic)**
3. Chọn scopes: `repo` (full control)
4. Click **Generate token**
5. Copy token (chỉ hiện 1 lần!)
6. Khi git push hỏi password, paste token này

### Option 2: GitHub CLI

```powershell
# Cài GitHub CLI
winget install --id GitHub.cli

# Authenticate
gh auth login
```

### Option 3: SSH Key

```powershell
# Tạo SSH key
ssh-keygen -t ed25519 -C "your.email@example.com"

# Copy public key
Get-Content ~/.ssh/id_ed25519.pub | clip

# Thêm vào GitHub: Settings → SSH and GPG keys → New SSH key
# Paste key từ clipboard

# Đổi remote sang SSH
git remote set-url origin git@github.com:username/dynamics365-plugin-decompiler.git
```

## Troubleshooting

### Error: "git is not recognized"
→ Cài Git và khởi động lại terminal

### Error: "Permission denied"
→ Kiểm tra authentication (token/SSH)

### Error: "Repository not found"
→ Kiểm tra URL repository

### Error: "Updates were rejected"
→ Pull changes trước: `git pull origin main --rebase`

## Update code sau này

```powershell
cd "d:\CODE\github copilot\decompile"

# Add changes
git add .

# Commit
git commit -m "Update: [mô tả thay đổi]"

# Push
git push
```

## Useful Git Commands

```powershell
# Xem status
git status

# Xem history
git log --oneline

# Xem remote
git remote -v

# Pull latest changes
git pull

# Create new branch
git checkout -b feature-name

# Switch branch
git checkout main
```

## Tạo README.md cho GitHub

File `README_GITHUB.md` đã được tạo sẵn. Bạn có thể:

```powershell
# Rename để làm README chính
Copy-Item README_GITHUB.md README.md -Force
git add README.md
git commit -m "Add main README"
git push
```

## Repository Structure

Đảm bảo `.gitignore` đã được tạo để không push các file không cần:
- `bin/`
- `obj/`
- `*.dll`
- `*.exe`
- `*.pdb`
- `.vs/`

File `.gitignore` đã có sẵn trong project! ✅

---

**Chúc bạn push code thành công! 🚀**
