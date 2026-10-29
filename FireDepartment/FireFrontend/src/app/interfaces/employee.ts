import { EmployeeName } from './employeeName';

export interface EmployeeDetailResponse extends EmployeeName {
  employeeNbr: number;
  paramedic: boolean;
  hazmat: boolean;
  employeeStatusCde: string;
  birthDate: string | null;
  maritalStatusCde: string | null;
  createdDate: string;
  createdBy: string;
  lastUpdateDate: string;
  lastUpdateBy: string;
}

export interface CreateEmployeeRequest {
  roic: string;
  employeeNbr: number;
  firstNme: string;
  middleInitialNme: string | null;
  lastNme: string;
  suffix: string | null;
  paramedic: boolean;
  hazmat: boolean;
  employeeStatusCde: string;
  birthDate: string | null;
  socialSecurityNbr: string | null;
  maritalStatusCde: string | null;
  createdDate: string;
  lastUpdateDate: string;
  createdBy: string;
  lastUpdateBy: string;
}

export interface UpdateEmployeeRequest {
  firstNme: string;
  middleInitialNme: string | null;
  lastNme: string;
  suffix: string | null;
  paramedic: boolean;
  hazmat: boolean;
  employeeStatusCde: string;
  birthDate: string | null;
  maritalStatusCde: string | null;
  lastUpdateDate: string;
  lastUpdateBy: string;
}
