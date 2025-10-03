Chào bạn, dựa trên yêu cầu và hình ảnh bạn cung cấp, tôi hiểu rằng bạn muốn xây dựng một màn hình checklist phức tạp dưới dạng một Web Resource cho Dynamics 365, sử dụng React (với Vite) và thư viện component Fluent UI.

Đây là một plan chi tiết và bài bản để bạn có thể bắt đầu. Tôi sẽ chia thành các bước từ khâu chuẩn bị, cấu trúc project, cho đến cách tích hợp vào Dynamics 365.

-----

### **Plant Build: Checklist Web Resource (React + Fluent UI)**

#### **Bước 1: Chuẩn bị Môi trường và Khởi tạo Project**

1.  **Cài đặt môi trường:**

      * Node.js và npm (hoặc yarn).
      * Visual Studio Code.
      * XrmToolBox (với plugin Web Resource Manager) để deploy dễ dàng hơn.

2.  **Khởi tạo React Project với Vite và TypeScript:**
    TypeScript là lựa chọn hàng đầu khi làm việc với Dynamics 365 để đảm bảo type-safety.

    ```bash
    npm create vite@latest d365-checklist-wr -- --template react-ts
    cd d365-checklist-wr
    npm install
    ```

3.  **Cài đặt các thư viện cần thiết:**

      * **Fluent UI:** Thư viện component chính.
        ```bash
        npm install @fluentui/react-components
        ```
      * **Dataverse Web API utility:** Giúp gọi API Dynamics 365 dễ dàng hơn (khuyến khích).
        ```bash
        npm install dataverse-webapi
        ```

#### **Bước 2: Phân tích và Cấu trúc Component**

Nhìn vào giao diện, chúng ta có thể chia nhỏ thành các component tái sử dụng được.

```
src/
|-- components/
|   |-- ChecklistDialog.tsx       # Component cha chứa toàn bộ layout
|   |-- Section.tsx               # Component layout chung cho mỗi box (có title)
|   |-- DocumentSection/
|   |   |-- index.tsx             # Logic cho khu vực số 2 (Documents)
|   |   |-- DocumentItem.tsx      # Hiển thị 1 file document
|   |-- InfoSection.tsx           # Component cho khu vực 3, 6, 7 (dạng key-value)
|   |-- ChecklistSection.tsx      # Component cho khu vực 4, 5, 8 (dạng list các item)
|   |-- AuditLogGrid.tsx          # Component cho khu vực số 9 (lưới audit log)
|   |-- StatusIcon.tsx            # Component nhỏ hiển thị icon check (xanh) hoặc cross (đỏ)
|-- services/
|   |-- crm-api.ts                # Nơi chứa các hàm gọi API tới D365 (lấy data, save audit)
|-- App.tsx                       # Component gốc, khởi tạo dialog
|-- main.tsx                      # Entry point của React app
```

#### **Bước 3: Xây dựng Giao diện với Fluent UI**

Dưới đây là code mẫu cho một vài component chính để bạn có hình dung.

**1. `ChecklistDialog.tsx` (Component Cha)**

Component này sẽ dùng `Dialog` của Fluent UI để tạo popup.

```tsx
// src/components/ChecklistDialog.tsx
import * as React from "react";
import {
  Dialog,
  DialogTrigger,
  DialogSurface,
  DialogTitle,
  DialogBody,
  DialogActions,
  DialogContent,
  Button,
} from "@fluentui/react-components";
import { Dismiss24Regular } from "@fluentui/react-icons";
// Import các section components
import { DocumentSection } from "./DocumentSection";
import { AuditLogGrid } from "./AuditLogGrid";
// ... import các section khác

export const ChecklistDialog = () => {
  // State để quản lý dữ liệu checklist
  const [checklistData, setChecklistData] = React.useState<any>({}); // Khởi tạo state rỗng

  React.useEffect(() => {
    // TODO: Gọi API để load tất cả dữ liệu cần thiết cho checklist
    // Ví dụ: loadDocuments(), loadCustomerInfo(), ...
    // Sau đó setChecklistData(result);
  }, []);

  const handleSave = () => {
    // TODO:
    // 1. Thu thập tất cả thay đổi từ state.
    // 2. Gọi API để lưu record Audit Log.
    // 3. Hiển thị thông báo thành công.
    console.log("Saving changes...", checklistData);
  };

  return (
    <Dialog modalType="modal">
      <DialogSurface style={{ width: "90vw", maxWidth: "1200px" }}>
        <DialogBody>
          <DialogTitle
            action={
              <DialogTrigger action="close">
                <Button
                  appearance="subtle"
                  aria-label="close"
                  icon={<Dismiss24Regular />}
                />
              </DialogTrigger>
            }
          >
            Claim Assessment Checklist
          </DialogTitle>
          <DialogContent>
            {/* Bố cục grid cho các section */}
            <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr 1fr', gap: '16px' }}>
                <DocumentSection />  {/* Khu vực 2 */}
                <InfoSection title="Customer Information" /> {/* Khu vực 3 */}
                <ChecklistSection title="Inforce Duration" /> {/* Khu vực 4 */}
                <ChecklistSection title="Diagnosis / ICD Code" /> {/* Khu vực 5 */}
                <InfoSection title="Policy Information" /> {/* Khu vực 6 */}
                <InfoSection title="Coverage Information" /> {/* Khu vực 7 */}
                <ChecklistSection title="Watch List" /> {/* Khu vực 8 */}
            </div>
            <AuditLogGrid /> {/* Khu vực 9 */}
          </DialogContent>
          <DialogActions>
            <Button appearance="primary">Confirm Checklist</Button>
            <Button appearance="secondary" onClick={handleSave}>Save</Button>
          </DialogActions>
        </DialogBody>
      </DialogSurface>
    </Dialog>
  );
};
```

**2. `DocumentSection/index.tsx` (Khu vực số 2)**

Đây là ví dụ cho khu vực hiển thị document.

```tsx
// src/components/DocumentSection/index.tsx
import * as React from "react";
import { Checkbox, Text } from "@fluentui/react-components";
import { Section } from "../Section"; // Component section chung
import { StatusIcon } from "../StatusIcon";

// Dữ liệu giả
const documents = {
  "CCCD": [{ name: "CCCD mặt trước.jpg", status: "approved" }],
  "Discharge Note": [{ name: "Giấy ra viện.pdf", status: "approved" }],
  "Health Insurance Card": [{ name: "Thẻ BHYT.png", status: "approved" }],
  "Invoice": [{ name: "Hóa đơn viện phí.pdf", status: "rejected" }],
  "Report of Accident": [{ name: "Biên bản tai nạn.docx", status: "none" }],
};

export const DocumentSection = () => {
  return (
    <Section title="Documents" selectAll>
      {Object.entries(documents).map(([docType, files]) => (
        <div key={docType}>
          <Text weight="semibold">{docType}</Text>
          {files.map((file) => (
            <div key={file.name} style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', paddingLeft: '16px' }}>
              <Checkbox label={file.name} />
              <StatusIcon status={file.status as "approved" | "rejected" | "none"} />
            </div>
          ))}
        </div>
      ))}
    </Section>
  );
};
```

**3. `StatusIcon.tsx` (Icon Check/Cross)**

Component nhỏ nhưng rất quan trọng, dùng ở nhiều nơi.

```tsx
// src/components/StatusIcon.tsx
import * as React from "react";
import { CheckmarkCircle24Filled, DismissCircle24Filled } from "@fluentui/react-icons";

interface StatusIconProps {
  status: "approved" | "rejected" | "none";
}

export const StatusIcon: React.FC<StatusIconProps> = ({ status }) => {
  if (status === "approved") {
    return <CheckmarkCircle24Filled style={{ color: "green" }} />;
  }
  if (status === "rejected") {
    return <DismissCircle24Filled style={{ color: "red" }} />;
  }
  return null; // Không hiển thị gì nếu status là 'none'
};
```

#### **Bước 4: Tích hợp với Dynamics 365**

1.  **Lấy Context của Record:**
    Web resource khi được mở từ form sẽ nhận được ID của record hiện tại qua query string. Ta cần đọc nó.

    ```tsx
    // Trong App.tsx hoặc ChecklistDialog.tsx
    React.useEffect(() => {
        const params = new URLSearchParams(window.location.search);
        const recordId = params.get("id"); // Lấy ID từ URL

        if (recordId) {
            // Gọi hàm fetch dữ liệu với recordId này
            // fetchDataForChecklist(recordId);
        }
    }, []);
    ```

2.  **Gọi Web API:**
    Dùng `Xrm.WebApi` (cách native) hoặc thư viện `dataverse-webapi`.

    ```typescript
    // src/services/crm-api.ts
    import { WebApi } from "dataverse-webapi";

    // Khởi tạo 1 lần
    const api = new WebApi({
      apiVersion: "9.2",
      url: Xrm.Utility.getGlobalContext().getClientUrl(),
    });

    export const getContactDetails = async (contactId: string) => {
      // retrieveRecord<EntityType>(entityName, entityId, options)
      const contact = await api.retrieveRecord<{ fullname: string, statecode: number }>(
        "contact",
        contactId,
        "?$select=fullname,statecode"
      );
      return contact;
    };

    export const createAuditLog = async (logData: any) => {
      // createRecord(entityName, data)
      const result = await api.createRecord("new_checklistauditlog", logData);
      return result.entityId;
    };
    ```

3.  **Mở Web Resource từ Button trên Form:**
    Bạn cần tạo một JavaScript Web Resource để gọi từ command của button "Checklist".

    ```javascript
    // openChecklistWR.js
    var YourNamespace = YourNamespace || {};

    YourNamespace.Checklist = {
        openChecklistDialog: function (primaryControl) {
            var formContext = primaryControl;
            var recordId = formContext.data.entity.getId().replace(/[{}]/g, "");
            var entityName = formContext.data.entity.getEntityName();

            var pageInput = {
                pageType: "webresource",
                webresourceName: "new_checklistreactapp.html", // Tên web resource HTML của bạn
                data: recordId // Truyền ID qua data parameter
            };

            var navigationOptions = {
                target: 2, // 2: Mở dưới dạng dialog
                width: { value: 90, unit: "%" },
                height: { value: 90, unit: "%" },
                position: 1, // 1: Căn giữa
                title: "Claim Assessment Checklist"
            };

            Xrm.Navigation.navigateTo(pageInput, navigationOptions).then(
                function success() {
                    // Có thể refresh form sau khi dialog đóng
                    formContext.data.refresh(false);
                },
                function error(e) {
                    console.error(e);
                }
            );
        }
    };
    ```

    *Lưu ý:* Vite sẽ tự động thêm hash vào tên file JS/CSS khi build. Bạn cần có một bước build script để cập nhật tên file này vào file `index.html` của mình hoặc deploy chúng với tên cố định.

#### **Bước 5: Build và Deploy**

1.  **Build Project:**

    ```bash
    npm run build
    ```

    Lệnh này sẽ tạo ra folder `dist` chứa file `index.html`, `assets/*.js`, và `assets/*.css`.

2.  **Deploy lên Dynamics 365:**

      * Mở **XrmToolBox** -\> **Web Resource Manager**.
      * Tạo mới các web resource:
          * `new_checklistreactapp.html` (Type: HTML) - nội dung là file `dist/index.html`.
          * `new_checklistmain.js` (Type: Script JScript) - nội dung là file JS trong `dist/assets`.
          * `new_checkliststyles.css` (Type: CSS) - nội dung là file CSS trong `dist/assets`.
      * **Quan trọng:** Mở file `new_checklistreactapp.html` và chỉnh lại đường dẫn tới file JS và CSS cho đúng với tên bạn đã đặt trên D365.
        ```html
        <script type="module" crossorigin src="/%7B637...%7D/new_checklistmain.js"></script>
        <link rel="stylesheet" href="/%7B637...%7D/new_checkliststyles.css">
        ```
      * Tạo JavaScript web resource `new_openChecklistWR.js` với nội dung ở Bước 4.
      * Dùng Ribbon Workbench để thêm button "Checklist" vào form, và cấu hình Command của nó để gọi tới hàm `YourNamespace.Checklist.openChecklistDialog`.

Kế hoạch này cung cấp cho bạn một lộ trình hoàn chỉnh. Bạn hãy bắt đầu từ việc dựng các component giao diện tĩnh với dữ liệu giả, sau đó mới kết nối với API của Dynamics 365. Chúc bạn thành công\!