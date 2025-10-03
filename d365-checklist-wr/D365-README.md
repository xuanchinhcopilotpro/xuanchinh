# 🚀 Dynamics 365 Checklist Web Resource

Giao diện Checklist cho Dynamics 365 được xây dựng với React + TypeScript + Fluent UI.

## ✨ Tính năng

- **Giao diện hiện đại**: Sử dụng Microsoft Fluent UI components
- **Responsive Design**: Tương thích với nhiều kích thước màn hình  
- **Type Safety**: TypeScript cho code quality cao
- **Performance**: Vite build tool cho tốc độ tối ưu
- **Integration**: Tích hợp mượt mà với Dynamics 365

## 🏗️ Kiến trúc

### Components
```
src/components/
├── ChecklistDialog.tsx      # Component chính chứa toàn bộ dialog
├── Section.tsx             # Layout wrapper cho các section
├── DocumentSection/        # Quản lý documents
├── InfoSection.tsx         # Hiển thị thông tin key-value
├── ChecklistSection.tsx    # Danh sách items với status
├── AuditLogGrid.tsx        # Grid audit log có thể expand
└── StatusIcon.tsx          # Icon trạng thái (approved/rejected)
```

### Services
```
src/services/
└── crm-api.ts             # Service layer cho Dynamics 365 API calls
```

## 🛠️ Công nghệ sử dụng

- **React 19.2** - Frontend framework
- **TypeScript** - Type safety
- **Vite** - Build tool & dev server  
- **Fluent UI** - Microsoft design system
- **Terser** - Code minification

## ⚡ Quick Start

### Development
```bash
# Install dependencies
npm install

# Start development server
npm run dev
```

### Build for Production
```bash
# Build optimized bundle
npm run build

# Preview production build
npm run preview
```

## 📦 Deployment

Xem chi tiết trong [DEPLOY.md](./DEPLOY.md)

### Web Resources cần tạo:
1. `new_checklistreactapp.html` - Main HTML file
2. `new_checklistmain.js` - React application bundle
3. `new_checkliststyles.css` - Styles
4. `new_openChecklistWR.js` - Button click handler

### Files để deploy:
- `dist/index-d365.html` → Upload như `new_checklistreactapp.html`
- `dist/assets/checklist-main.js` → Upload như `new_checklistmain.js`  
- `dist/assets/checklist-index.css` → Upload như `new_checkliststyles.css`
- `public/openChecklistWR.js` → Upload như `new_openChecklistWR.js`

## 🎨 Customization

### Thay đổi Theme
```typescript
// src/components/ChecklistDialog.tsx
import { webDarkTheme } from "@fluentui/react-components";

<FluentProvider theme={webDarkTheme}>
```

## 📊 Data Flow

```
Dynamics 365 Form
    ↓ (Button Click)
openChecklistWR.js
    ↓ (Open Dialog)
ChecklistDialog Component
    ↓ (Load Data)
CrmApiService
    ↓ (Fetch from D365)
Display Checklist Data
```

---

**Developed with ❤️ for Dynamics 365 Community**