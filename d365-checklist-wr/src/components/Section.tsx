import * as React from "react";
import { Card, Text, Checkbox } from "@fluentui/react-components";

interface SectionProps {
  title: string;
  selectAll?: boolean;
  children: React.ReactNode;
  headerNumber?: string;
}

export const Section: React.FC<SectionProps> = ({ 
  title, 
  selectAll = false, 
  children, 
  headerNumber 
}) => {
  return (
    <Card style={{ 
      padding: "12px", 
      height: "fit-content", 
      minWidth: 0, 
      maxWidth: '100%',
      width: '100%',
      boxSizing: 'border-box',
      border: '1px solid #e0e0e0',
      borderRadius: '4px'
    }}>
      {/* Header section với title và number */}
      <div style={{ 
        display: "flex", 
        alignItems: "center", 
        justifyContent: "space-between", 
        width: "100%",
        marginBottom: "8px",
        borderBottom: "1px solid #e0e0e0",
        paddingBottom: "6px"
      }}>
        <div style={{ display: "flex", alignItems: "center", gap: "8px" }}>
          {headerNumber && (
            <div style={{
              backgroundColor: "#d13438",
              color: "white",
              borderRadius: "50%",
              width: "24px",
              height: "24px",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              fontSize: "12px",
              fontWeight: "bold",
              flexShrink: 0
            }}>
              {headerNumber}
            </div>
          )}
          <Text weight="semibold" size={400} style={{ color: "#323130", fontSize: '14px' }}>
            {title}
          </Text>
        </div>
        {selectAll && (
          <Checkbox label="(Select all)" />
        )}
      </div>
      
      {/* Content section */}
      <div style={{ display: "flex", flexDirection: "column", gap: "6px" }}>
        {children}
      </div>
    </Card>
  );
};