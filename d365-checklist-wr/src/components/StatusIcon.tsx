import * as React from "react";
import { CheckmarkCircle24Filled, DismissCircle24Filled } from "@fluentui/react-icons";

interface StatusIconProps {
  status: "approved" | "rejected" | "none";
}

export const StatusIcon: React.FC<StatusIconProps> = ({ status }) => {
  if (status === "approved") {
    return <CheckmarkCircle24Filled style={{ color: "#107c10" }} />;
  }
  if (status === "rejected") {
    return <DismissCircle24Filled style={{ color: "#d13438" }} />;
  }
  return null; // Không hiển thị gì nếu status là 'none'
};