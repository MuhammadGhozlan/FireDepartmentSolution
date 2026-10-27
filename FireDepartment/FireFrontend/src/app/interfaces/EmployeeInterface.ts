export interface CreateEmployeeRequest {
  roic: string;
  employeeNbr: number;
  firstNme: string;
  middleInitialNme?: string | null;
  lastNme: string;
  suffix?: string | null;
  paramedic: boolean;
  hazmat: boolean;
  employeeStatusCde: string;
  birthDate?: string | null;
  socialSecurityNbr?: string | null;
  maritalStatusCde?: string | null;
  createdDate: string;
  lastUpdateDate: string;
  createdBy?: string;
  lastUpdateBy?: string;
}

export interface UpdateEmployeeRequest {
  firstNme: string;
  middleInitialNme?: string | null;
  lastNme: string;
  suffix?: string | null;
  paramedic: boolean;
  hazmat: boolean;
  employeeStatusCde: string;
  birthDate?: string | null;
  maritalStatusCde?: string | null;
  lastUpdateDate: string;
  lastUpdateBy?: string;
}

export interface EmployeeSummaryResponse {
  roic: string;
  employeeNbr: number;
  firstNme: string;
  lastNme: string;
  employeeStatusCde: string;
}

export interface EmployeeDetailResponse {
  roic: string;
  employeeNbr: number;
  firstNme: string;
  middleInitialNme?: string | null;
  lastNme: string;
  suffix?: string | null;
  paramedic: boolean;
  hazmat: boolean;
  employeeStatusCde: string;
  birthDate?: string | null;
  maritalStatusCde?: string | null;
  createdDate: string;
  createdBy: string;
  lastUpdateDate: string;
  lastUpdateBy: string;
}

export interface EmployeeAssignmentRequest {
  assignmentStartDate: string;
  assignmentEndDate?: string | null;
  isTemp: boolean;
  roic: string;
  companyPositionId: number;
  workPeriodId: number;
}

export interface EmployeeAssignmentResponse {
  employeeAssignmentId: number;
  assignmentStartDate: string;
  assignmentEndDate?: string | null;
  isTemp: boolean;
  roic: string;
  companyPositionId: number;
  workPeriodId: number;
  companyName?: string | null;
  stationNbr?: string | null;
}