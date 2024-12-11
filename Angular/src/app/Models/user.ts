import { Address } from "./address";
import { UserRole } from "./userRole.enum";

export interface User {
  id: string;
  email: string;
  userRole: UserRole[];
  password: string;
  address: Address;
  token: string;
  timestamp: Date;
}
