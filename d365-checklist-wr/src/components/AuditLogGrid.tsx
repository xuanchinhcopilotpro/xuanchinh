import * as React from "react";
import { Button } from "@fluentui/react-components";
import { ChevronRight24Regular } from "@fluentui/react-icons";

export const AuditLogGrid = () => {
  const [isExpanded, setIsExpanded] = React.useState(false);

  return (
    <div style={{ marginTop: "16px" }}>
      <Button
        appearance="transparent"
        icon={<ChevronRight24Regular style={{ 
          transform: isExpanded ? "rotate(90deg)" : "rotate(0deg)",
          transition: "transform 0.2s"
        }} />}
        onClick={() => setIsExpanded(!isExpanded)}
        style={{ 
          display: "flex", 
          alignItems: "center", 
          gap: "8px",
          padding: "8px"
        }}
      >
        Audit Log
      </Button>
      
      {isExpanded && (
        <div style={{ 
          marginTop: "8px", 
          padding: "16px", 
          border: "1px solid #e0e0e0", 
          borderRadius: "4px",
          backgroundColor: "#f9f9f9"
        }}>
          <p style={{ margin: 0, color: "#666", fontSize: "14px" }}>
            Audit log entries will be displayed here...
          </p>
        </div>
      )}
    </div>
  );
};