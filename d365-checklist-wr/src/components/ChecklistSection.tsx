import * as React from "react";
import { Text } from "@fluentui/react-components";
import { Section } from "./Section";
import { StatusIcon } from "./StatusIcon";

interface ChecklistItem {
  label: string;
  status: "approved" | "rejected" | "none";
}

interface ChecklistSectionProps {
  title: string;
  items: ChecklistItem[];
}

export const ChecklistSection: React.FC<ChecklistSectionProps> = (props) => {
  const { title, items } = props;
  return (
    <Section title={title}>
      {items.map((item, index) => (
        <div key={index} style={{ 
          display: 'flex', 
          alignItems: 'center', 
          justifyContent: 'space-between',
          paddingLeft: '8px',
          minHeight: '24px'
        }}>
          <Text size={300}>{item.label}</Text>
          <StatusIcon status={item.status} />
        </div>
      ))}
    </Section>
  );
};