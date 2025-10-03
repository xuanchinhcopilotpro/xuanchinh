import * as React from "react";
import { Text } from "@fluentui/react-components";
import { Section } from "../Section";
import { StatusIcon } from "../StatusIcon";

interface DocumentItem {
  name: string;
  status: "approved" | "rejected" | "none";
  fileId?: string;
}

interface DocumentSectionProps {
  documents?: DocumentItem[];
}

export const DocumentSection: React.FC<DocumentSectionProps> = ({ documents }) => {
  // Dữ liệu mặc định nếu không có props
  const defaultDocuments = [
    { name: "CCCD", status: "approved" as const },
    { name: "Discharge Note", status: "approved" as const },
    { name: "Health Insurance Card", status: "approved" as const },
    { name: "Invoice", status: "rejected" as const },
    { name: "Report of Accident", status: "none" as const },
  ];

  const documentsToShow = documents || defaultDocuments;
  
  console.log("DocumentSection rendering with documents:", documentsToShow);

  return (
    <Section title="Documents:" selectAll>
      {documentsToShow.map((doc) => (
        <div key={doc.name} style={{ 
          display: 'flex', 
          alignItems: 'center', 
          justifyContent: 'space-between',
          paddingLeft: '8px'
        }}>
          <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
            <span style={{ fontSize: '14px' }}>▼</span>
            <Text size={300}>{doc.name}</Text>
          </div>
          <StatusIcon status={doc.status} />
        </div>
      ))}
    </Section>
  );
};