// Service để gọi API Dynamics 365
export interface ChecklistData {
  recordId: string;
  entityName: string;
  documents: DocumentItem[];
  customerInfo: CustomerInfo;
  policyInfo: PolicyInfo;
  coverageInfo: CoverageInfo;
  watchList: WatchListItem[];
}

export interface DocumentItem {
  name: string;
  status: "approved" | "rejected" | "none";
  fileId?: string;
}

export interface CustomerInfo {
  premiumLoading: boolean;
  counterOffer: boolean;
  claimHistoryCount: number;
  complaintCount: number;
}

export interface PolicyInfo {
  status: string;
  effectiveDate: string;
  issuedDate: string;
  sumAssured: string;
  lastLapsedDate?: string;
  lastReinstatementDate?: string;
}

export interface CoverageInfo {
  status: string;
  effectiveDate: string;
  issuedDate: string;
  paidToDate: string;
  inforceDuration: string;
  sumAssured: string;
  lastLapsedDate: string;
  lastReinstatementDate: string;
  age: number;
}

export interface WatchListItem {
  type: "customer" | "agent" | "hospital";
  status: "approved" | "rejected" | "none";
}

export class CrmApiService {
  constructor() {
    // Trong môi trường thực tế, sẽ lấy từ Xrm.Utility.getGlobalContext().getClientUrl()
    console.log("CRM API Service initialized");
  }

  // Lấy thông tin checklist từ record ID
  async getChecklistData(recordId: string, entityName: string): Promise<ChecklistData> {
    try {
      // Trong môi trường thực tế, sẽ dùng Xrm.WebApi hoặc fetch API
      // Hiện tại return dữ liệu mẫu
      return this.getMockData(recordId, entityName);
    } catch (error) {
      console.error("Error fetching checklist data:", error);
      throw error;
    }
  }

  // Lưu audit log
  async saveAuditLog(data: any): Promise<string> {
    try {
      // Trong môi trường thực tế, sẽ gọi API để tạo record audit log
      console.log("Saving audit log:", data);
      return "audit-log-id-" + Date.now();
    } catch (error) {
      console.error("Error saving audit log:", error);
      throw error;
    }
  }

  // Confirm checklist
  async confirmChecklist(recordId: string, checklistData: any): Promise<boolean> {
    try {
      // Trong môi trường thực tế, sẽ cập nhật status của record
      console.log("Confirming checklist for record:", recordId, checklistData);
      return true;
    } catch (error) {
      console.error("Error confirming checklist:", error);
      throw error;
    }
  }

  // Dữ liệu mẫu để test
  private getMockData(recordId: string, entityName: string): ChecklistData {
    return {
      recordId,
      entityName,
      documents: [
        { name: "CCCD", status: "approved" },
        { name: "Discharge Note", status: "approved" },
        { name: "Health Insurance Card", status: "approved" },
        { name: "Invoice", status: "rejected" },
        { name: "Report of Accident", status: "none" },
      ],
      customerInfo: {
        premiumLoading: true,
        counterOffer: true,
        claimHistoryCount: 5,
        complaintCount: 0,
      },
      policyInfo: {
        status: "20",
        effectiveDate: "31/12/2020",
        issuedDate: "31/12/2020",
        sumAssured: "5,000,000,000",
      },
      coverageInfo: {
        status: "42",
        effectiveDate: "31/12/2020",
        issuedDate: "31/12/2020",
        paidToDate: "31/12/2023",
        inforceDuration: "12M",
        sumAssured: "200,000,000",
        lastLapsedDate: "31/12/2021",
        lastReinstatementDate: "31/03/2022",
        age: 23,
      },
      watchList: [
        { type: "customer", status: "approved" },
        { type: "agent", status: "rejected" },
        { type: "hospital", status: "approved" },
      ],
    };
  }
}

// Singleton instance
export const crmApiService = new CrmApiService();