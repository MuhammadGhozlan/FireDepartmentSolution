export interface EmployeeAssignmentRequest {
  assignmentStartDate: string;
  assignmentEndDate: string | null;
  isTemp: boolean;
  roic: string;
  companyPositionId: number;
  workPeriodId: number;
}

export interface EmployeeAssignmentResponse extends EmployeeAssignmentRequest {
  employeeAssignmentId: number;
  companyName: string | null;
  stationNbr: string | null;
}
