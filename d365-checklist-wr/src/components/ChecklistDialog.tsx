import * as React from "react";
import {
  Dialog,
  DialogSurface,
  DialogTitle,
  DialogBody,
  DialogContent,
  Button,
  FluentProvider,
  webLightTheme,
  Spinner
} from "@fluentui/react-components";
import { Dismiss24Regular } from "@fluentui/react-icons";
import { DocumentSection } from "./DocumentSection";
import { InfoSection } from "./InfoSection";
import { ChecklistSection } from "./ChecklistSection";
import { AuditLogGrid } from "./AuditLogGrid";
import { crmApiService, type ChecklistData } from "../services/crm-api";

export const ChecklistDialog = () => {
  const [isOpen, setIsOpen] = React.useState(true);
  const [checklistData, setChecklistData] = React.useState<ChecklistData | null>(null);
  const [loading, setLoading] = React.useState(true);
  const [recordId, setRecordId] = React.useState<string>("");
  const [entityName, setEntityName] = React.useState<string>("");

  // Load data khi component mount
  React.useEffect(() => {
    const loadData = async () => {
      try {
        // Đọc parameters từ URL hoặc context
        const params = new URLSearchParams(window.location.search);
        const id = params.get("id") || params.get("recordId") || "sample-id";
        const entity = params.get("entity") || params.get("entityName") || "incident";
        
        setRecordId(id);
        setEntityName(entity);

        // Load checklist data
        const data = await crmApiService.getChecklistData(id, entity);
        console.log("Loaded checklist data:", data);
        setChecklistData(data);
      } catch (error) {
        console.error("Error loading checklist data:", error);
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, []);

  const handleSave = async () => {
    try {
      console.log("Saving changes...");
      await crmApiService.saveAuditLog({
        recordId,
        entityName,
        action: "save",
        timestamp: new Date().toISOString(),
        data: checklistData
      });
      // TODO: Show success message
    } catch (error) {
      console.error("Error saving:", error);
      // TODO: Show error message
    }
  };

  const handleConfirm = async () => {
    try {
      console.log("Confirming checklist...");
      await crmApiService.confirmChecklist(recordId, checklistData);
      // TODO: Show success message and close dialog
      setIsOpen(false);
    } catch (error) {
      console.error("Error confirming:", error);
      // TODO: Show error message
    }
  };

  if (loading) {
    return (
      <FluentProvider theme={webLightTheme}>
        <div style={{ 
          display: "flex", 
          justifyContent: "center", 
          alignItems: "center", 
          height: "100vh" 
        }}>
          <Spinner label="Loading checklist data..." />
        </div>
      </FluentProvider>
    );
  }

  if (!checklistData) {
    return (
      <FluentProvider theme={webLightTheme}>
        <div style={{ 
          display: "flex", 
          justifyContent: "center", 
          alignItems: "center", 
          height: "100vh" 
        }}>
          <div>Error loading checklist data</div>
        </div>
      </FluentProvider>
    );
  }

  // Dữ liệu từ API
  const customerInfoItems = [
    { 
      label: "Premium Loading", 
      isLink: true, 
      status: checklistData.customerInfo.premiumLoading ? "approved" as const : "rejected" as const 
    },
    { 
      label: "Counter Offer", 
      isLink: true, 
      status: checklistData.customerInfo.counterOffer ? "approved" as const : "rejected" as const 
    },
    { 
      label: "# of Claim History", 
      value: checklistData.customerInfo.claimHistoryCount.toString(), 
      hasCheckbox: true 
    },
    { 
      label: "# of Complaint", 
      value: checklistData.customerInfo.complaintCount.toString(), 
      hasCheckbox: true 
    },
  ];

  const policyInfoItems = [
    { label: "Policy Status", value: checklistData.policyInfo.status, hasCheckbox: true },
    { label: "Effective Date", value: checklistData.policyInfo.effectiveDate, hasCheckbox: true },
    { label: "Issued Date", value: checklistData.policyInfo.issuedDate, hasCheckbox: true },
    { label: "Sum Assured", value: checklistData.policyInfo.sumAssured, hasCheckbox: true },
    { label: "Last Lapsed Date", value: checklistData.policyInfo.lastLapsedDate, hasCheckbox: true },
    { label: "Last Reinstatement Date", value: checklistData.policyInfo.lastReinstatementDate, hasCheckbox: true },
  ];

  const coverageInfoItems = [
    { label: "Coverage Status", value: checklistData.coverageInfo.status, hasCheckbox: true },
    { label: "Effective Date", value: checklistData.coverageInfo.effectiveDate, hasCheckbox: true },
    { label: "Issued Date", value: checklistData.coverageInfo.issuedDate, hasCheckbox: true },
    { label: "Paid To Date", value: checklistData.coverageInfo.paidToDate, hasCheckbox: true },
    { label: "Inforce Duration", value: checklistData.coverageInfo.inforceDuration, hasCheckbox: true },
    { label: "Sum Assured", value: checklistData.coverageInfo.sumAssured, hasCheckbox: true },
    { label: "Last Lapsed Date", value: checklistData.coverageInfo.lastLapsedDate, hasCheckbox: true },
    { label: "Last Reinstatement Date", value: checklistData.coverageInfo.lastReinstatementDate, hasCheckbox: true },
    { label: "Age", value: checklistData.coverageInfo.age.toString(), hasCheckbox: true },
  ];

  const inforceDurationItems = [
    { label: "30 days", status: "approved" as const },
    { label: "90 days", status: "approved" as const },
    { label: "120 days", status: "rejected" as const },
  ];

  const diagnosisItems = [
    { label: "T&C Exclusion", status: "approved" as const },
    { label: "Customer Exclusion", status: "rejected" as const },
  ];

  const watchListItems = checklistData.watchList.map(item => ({
    label: `${item.type.charAt(0).toUpperCase() + item.type.slice(1)} Watch List`,
    status: item.status
  }));

  return (
    <FluentProvider theme={webLightTheme}>
      <Dialog open={isOpen} onOpenChange={(_, data) => setIsOpen(data.open)}>
        <DialogSurface style={{ width: "95vw", maxWidth: "95vw", height: "95vh", maxHeight: "95vh", overflow: 'hidden' }}>
          <DialogBody style={{ display: 'flex', flexDirection: 'column', height: '100%' }}>
            <DialogTitle
              action={
                <Button
                  appearance="subtle"
                  aria-label="close"
                  icon={<Dismiss24Regular />}
                  onClick={() => setIsOpen(false)}
                />
              }
            >
              <div style={{ display: "flex", alignItems: "center", gap: "8px" }}>
                Claim Assessment Checklist
              </div>
            </DialogTitle>
            <DialogContent style={{ padding: "0 24px", overflowY: "auto", flex: 1 }}>
              {/* Bố cục 4 cột đều nhau */}
              <div style={{ 
                display: 'grid', 
                gridTemplateColumns: 'repeat(4, minmax(250px, 1fr))', 
                gridTemplateRows: 'auto auto',
                gap: '16px',
                marginBottom: '16px',
                width: '100%'
              }}>
                {/* Hàng 1: Documents, Customer Information, Inforce Duration, Diagnosis */}
                <div style={{ gridColumn: 1, gridRow: 1, minWidth: 0, maxWidth: '100%' }}>
                  <DocumentSection documents={checklistData?.documents} />
                </div>
                <div style={{ gridColumn: 2, gridRow: 1, minWidth: 0, maxWidth: '100%' }}>
                  <InfoSection 
                    title="Customer Information:" 
                    items={customerInfoItems} 
                  />
                </div>
                <div style={{ gridColumn: 3, gridRow: 1, minWidth: 0, maxWidth: '100%' }}>
                  <ChecklistSection 
                    title="Inforce Duration:" 
                    items={inforceDurationItems} 
                  />
                </div>
                <div style={{ gridColumn: 4, gridRow: 1, minWidth: 0, maxWidth: '100%' }}>
                  <ChecklistSection 
                    title="Diagnosis / ICD Code:" 
                    items={diagnosisItems} 
                  />
                </div>
                
                {/* Hàng 2: Policy Information, Coverage Information, Watch List */}
                <div style={{ gridColumn: 2, gridRow: 2, minWidth: 0, maxWidth: '100%' }}>
                  <InfoSection 
                    title="Policy Information:" 
                    items={policyInfoItems} 
                    selectAll 
                  />
                </div>
                <div style={{ gridColumn: 3, gridRow: 2, minWidth: 0, maxWidth: '100%' }}>
                  <InfoSection 
                    title="Coverage Information:" 
                    items={coverageInfoItems} 
                    selectAll 
                  />
                </div>
                <div style={{ gridColumn: 4, gridRow: 2, minWidth: 0, maxWidth: '100%' }}>
                  <ChecklistSection 
                    title="Watch List:" 
                    items={watchListItems} 
                  />
                </div>
              </div>
              <AuditLogGrid />
              <div style={{ display: 'flex', justifyContent: 'flex-end', gap: 8, marginTop: 24 }}>
                <Button appearance="primary" onClick={handleConfirm}>
                  Confirm Checklist
                </Button>
                <Button appearance="secondary" onClick={handleSave}>
                  Save
                </Button>
              </div>
            </DialogContent>
          </DialogBody>
        </DialogSurface>
      </Dialog>
    </FluentProvider>
  );
};