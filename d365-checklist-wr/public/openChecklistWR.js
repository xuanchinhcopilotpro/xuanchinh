// openChecklistWR.js - JavaScript Web Resource để mở Checklist Dialog từ D365
var YourNamespace = YourNamespace || {};

YourNamespace.Checklist = {
    /**
     * Mở Checklist Dialog từ button trên form
     * @param {any} primaryControl - Form context từ Dynamics 365
     */
    openChecklistDialog: function (primaryControl) {
        var formContext = primaryControl;
        var recordId = formContext.data.entity.getId().replace(/[{}]/g, "");
        var entityName = formContext.data.entity.getEntityName();

        // Thông tin để truyền vào Web Resource
        var pageInput = {
            pageType: "webresource",
            webresourceName: "new_checklistreactapp.html", // Tên web resource HTML
            data: {
                recordId: recordId,
                entityName: entityName,
                mode: "dialog"
            }
        };

        // Cấu hình dialog
        var navigationOptions = {
            target: 2, // 2: Mở dưới dạng dialog
            width: { value: 95, unit: "%" },
            height: { value: 90, unit: "%" },
            position: 1, // 1: Căn giữa
            title: "Claim Assessment Checklist"
        };

        // Mở dialog
        Xrm.Navigation.navigateTo(pageInput, navigationOptions).then(
            function success(result) {
                console.log("Checklist dialog opened successfully");
                // Có thể refresh form sau khi dialog đóng nếu cần
                // formContext.data.refresh(false);
            },
            function error(e) {
                console.error("Error opening checklist dialog:", e);
                Xrm.Navigation.openAlertDialog({
                    confirmButtonLabel: "OK",
                    text: "Unable to open checklist dialog. Please try again.",
                    title: "Error"
                });
            }
        );
    },

    /**
     * Kiểm tra quyền trước khi hiển thị button (optional)
     * @param {any} primaryControl - Form context
     * @returns {boolean} - True nếu được phép hiển thị button
     */
    checkButtonVisibility: function (primaryControl) {
        // Thêm logic kiểm tra quyền ở đây nếu cần
        // Ví dụ: chỉ cho phép role "Claims Officer" hoặc "Manager" thực hiện
        return true;
    },

    /**
     * Khởi tạo và cấu hình ribbon button
     */
    initializeRibbon: function () {
        // Code này sẽ được gọi khi form load
        console.log("Checklist ribbon initialized");
    }
};

// Export cho global scope
window.YourNamespace = YourNamespace;