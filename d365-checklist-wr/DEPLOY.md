# Hướng dẫn Deploy Checklist Web Resource lên Dynamics 365

## Các file cần deploy:

### 1. HTML Web Resource
**Tên:** `new_checklistreactapp.html`  
**Loại:** HTML Web Page  
**Nguồn:** `/dist/index.html`

Trước khi upload, hãy chỉnh sửa các đường dẫn trong file `index.html`:

```html
<!doctype html>
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <link rel="icon" type="image/svg+xml" href="/vite.svg" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>D365 Checklist</title>
    <script type="module" crossorigin src="/WebResources/new_checklistmain.js"></script>
    <link rel="stylesheet" crossorigin href="/WebResources/new_checkliststyles.css">
  </head>
  <body>
    <div id="root"></div>
  </body>
</html>
```

### 2. JavaScript Web Resource
**Tên:** `new_checklistmain.js`  
**Loại:** Script (JScript)  
**Nguồn:** `/dist/assets/checklist-main.js`

### 3. CSS Web Resource
**Tên:** `new_checkliststyles.css`  
**Loại:** Style Sheet (CSS)  
**Nguồn:** `/dist/assets/checklist-index.css`

### 4. Button Action JavaScript
**Tên:** `new_openChecklistWR.js`  
**Loại:** Script (JScript)  
**Nguồn:** `/public/openChecklistWR.js`

## Cách Deploy:

### Option 1: Sử dụng XrmToolBox
1. Mở **XrmToolBox**
2. Kết nối với môi trường Dynamics 365
3. Mở plugin **Web Resource Manager**
4. Tạo mới hoặc cập nhật các Web Resources theo danh sách trên
5. Publish All

### Option 2: Sử dụng Power Platform CLI
```bash
# Kết nối với môi trường
pac auth create --url https://your-org.crm.dynamics.com

# Tạo solution
pac solution create --publisher-name "YourPublisher" --publisher-prefix "new"

# Thêm web resources
pac webresource push --file "dist/index.html" --name "new_checklistreactapp.html"
pac webresource push --file "dist/assets/checklist-main.js" --name "new_checklistmain.js"
pac webresource push --file "dist/assets/checklist-index.css" --name "new_checkliststyles.css"
pac webresource push --file "public/openChecklistWR.js" --name "new_openChecklistWR.js"
```

## Cấu hình Ribbon Button:

### 1. Sử dụng Ribbon Workbench
1. Mở **Ribbon Workbench** trong XrmToolBox
2. Chọn entity cần thêm button (ví dụ: Case/Incident)
3. Thêm button mới vào ribbon
4. Cấu hình Command cho button:
   - **Function Name:** `YourNamespace.Checklist.openChecklistDialog`
   - **Library:** `new_openChecklistWR.js`
   - **Parameters:** `PrimaryControl`

### 2. Command Definition
```xml
<RibbonDiffXml>
  <CustomActions>
    <CustomAction Id="YourOrg.SubArea.ChecklistButton.CustomAction" 
                  Location="Mscrm.Form.incident.MainTab.Actions" 
                  Sequence="30">
      <CommandUIDefinition>
        <Button Id="YourOrg.ChecklistButton" 
                Command="YourOrg.ChecklistCommand" 
                LabelText="Checklist" 
                Image16by16="/_imgs/ico_16_4210.gif" 
                Image32by32="/_imgs/ico_32_4210.gif" />
      </CommandUIDefinition>
    </CustomAction>
  </CustomActions>
  <RibbonTemplates Id="Mscrm.Templates">
    <CommandDefinition Id="YourOrg.ChecklistCommand">
      <EnableRules>
        <EnableRule Id="YourOrg.EnableRule.ChecklistVisible" />
      </EnableRules>
      <DisplayRules />
      <Actions>
        <JavaScriptFunction FunctionName="YourNamespace.Checklist.openChecklistDialog" 
                           Library="new_openChecklistWR.js">
          <CrmParameter Value="PrimaryControl" />
        </JavaScriptFunction>
      </Actions>
    </CommandDefinition>
  </RibbonTemplates>
  <RuleDefinitions>
    <TabDisplayRules />
    <DisplayRules />
    <EnableRules>
      <EnableRule Id="YourOrg.EnableRule.ChecklistVisible">
        <FormStateRule State="Existing" />
      </EnableRule>
    </EnableRules>
  </RuleDefinitions>
</RibbonDiffXml>
```

## Test và Debugging:

### 1. Test trên Development Environment
- Mở form có button Checklist
- Click button để mở dialog
- Kiểm tra console cho errors
- Test các chức năng Save và Confirm

### 2. Common Issues và Solutions:

**Issue:** "Resource not found"
- **Solution:** Kiểm tra tên Web Resource có đúng không
- Đảm bảo paths trong HTML đúng format `/WebResources/tên_file`

**Issue:** "Function not defined"
- **Solution:** Kiểm tra JavaScript Web Resource đã publish chưa
- Kiểm tra namespace trong openChecklistWR.js

**Issue:** "CORS errors"
- **Solution:** Đảm bảo API calls sử dụng relative paths hoặc same-origin

### 3. Browser Developer Tools
- Mở F12 Developer Tools
- Kiểm tra Network tab cho failed requests
- Kiểm tra Console tab cho JavaScript errors
- Kiểm tra Elements tab cho DOM issues

## Security Considerations:

1. **Web Resource Security:**
   - Chỉ users có quyền đọc Web Resource mới thấy được
   - Cấu hình security roles phù hợp

2. **API Calls:**
   - Sử dụng Xrm.WebApi cho data access
   - Tuân thủ Dynamics 365 security model
   - Không hard-code sensitive information

3. **User Permissions:**
   - Kiểm tra quyền trước khi hiển thị button
   - Validate permissions trong server-side code

## Performance Optimization:

1. **Minimize Bundle Size:**
   - Code splitting nếu cần
   - Remove unused dependencies
   - Optimize images và assets

2. **Lazy Loading:**
   - Load data only when needed
   - Implement pagination cho large datasets

3. **Caching:**
   - Cache static data appropriately
   - Use browser caching headers

## Maintenance:

1. **Version Control:**
   - Tag releases cho tracking
   - Maintain change logs
   - Backup before deployments

2. **Monitoring:**
   - Monitor usage và performance
   - Set up error logging
   - Regular health checks

3. **Updates:**
   - Test thoroughly before production deploy
   - Plan rollback strategy
   - Communicate changes to users