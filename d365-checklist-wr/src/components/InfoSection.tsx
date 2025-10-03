import * as React from "react";
import { Text, Checkbox } from "@fluentui/react-components";
import { Section } from "./Section";
import { StatusIcon } from "./StatusIcon";

interface InfoItem {
  label: string;
  value?: string | number;
  status?: "approved" | "rejected" | "none";
  isLink?: boolean;
  hasCheckbox?: boolean;
}

interface InfoSectionProps {
  title: string;
  items: InfoItem[];
  selectAll?: boolean;
}

export const InfoSection: React.FC<InfoSectionProps> = (props) => {
  const { title, items, selectAll = false } = props;
  return (
    <Section title={title} selectAll={selectAll}>
      {items.map((item, index) => (
        <div key={index} style={{ 
          display: 'flex', 
          alignItems: 'center', 
          justifyContent: 'space-between',
          paddingLeft: '8px',
          minHeight: '32px',
          gap: '8px'
        }}>
          <div style={{ display: 'flex', alignItems: 'center', gap: '8px', flex: '0 0 auto', minWidth: '120px', maxWidth: '140px' }}>
            <Text size={300} style={{ fontSize: '13px', whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' }}>
              {item.isLink ? (
                <span style={{ color: "#0078d4", textDecoration: "underline", cursor: "pointer" }}>
                  {item.label}
                </span>
              ) : (
                item.label
              )}
            </Text>
            {item.value !== undefined && (
              <input
                type="text"
                value={String(item.value)}
                readOnly
                disabled
                style={{
                  flex: 1,
                  minWidth: 100,
                  maxWidth: '100%',
                  background: '#f3f2f1',
                  border: '1px solid #c8c6c4',
                  borderRadius: 4,
                  color: '#666',
                  padding: '4px 8px',
                  fontSize: 13,
                  boxSizing: 'border-box'
                }}
              />
            )}
          </div>
          <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
            {item.status && <StatusIcon status={item.status} />}
            {item.hasCheckbox && <Checkbox />}
          </div>
        </div>
      ))}
    </Section>
  );
};