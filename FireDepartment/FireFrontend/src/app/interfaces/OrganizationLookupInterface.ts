export interface EmployeeStatusCodeRequest {
  employeeStatusCde: string;
  employeeStatusDesc: string;
}

export interface EmployeeStatusCodeResponse {
  employeeStatusCde: string;
  employeeStatusDesc: string;
}

export interface AttendanceStatusRequest {
  attendanceStatusCde: string;
  attendanceStatusDesc: string;
}

export interface AttendanceStatusResponse {
  attendanceStatusId: number;
  attendanceStatusCde: string;
  attendanceStatusDesc: string;
}

export interface ApprovalStatusRequest {
  approvalStatusCde: string;
}

export interface ApprovalStatusResponse {
  approvalStatusId: number;
  approvalStatusCde: string;
}

export interface ContactTypeRequest {
  contactTypeDesc: string;
}

export interface ContactTypeResponse {
  contactTypeId: number;
  contactTypeDesc: string;
}

export interface DistrictRequest {
  districtNme: string;
  stationCount?: number | null;
  isActive: boolean;
  districtComment?: string | null;
}

export interface DistrictResponse {
  districtId: number;
  districtNme: string;
  stationCount?: number | null;
  isActive: boolean;
  districtComment?: string | null;
}

export interface StationRequest {
  stationNbr: string;
  isActive: boolean;
  stationComment?: string | null;
  districtId: number;
}

export interface StationResponse {
  stationId: number;
  stationNbr: string;
  isActive: boolean;
  stationComment?: string | null;
  districtId: number;
}

export interface CompanyRequest {
  companyNme: string;
  isActive: boolean;
  companyMemberCount: number;
  medicalClassification?: string | null;
  companyComment?: string | null;
  stationId?: number | null;
}

export interface CompanyResponse {
  companyId: number;
  companyNme: string;
  isActive: boolean;
  companyMemberCount: number;
  medicalClassification?: string | null;
  companyComment?: string | null;
  stationId?: number | null;
}

export interface CompanyPositionRequest {
  truckPosition: number;
  companyId: number;
  jobTitleId: number;
  shiftId: number;
}

export interface CompanyPositionResponse {
  companyPositionId: number;
  truckPosition: number;
  companyId: number;
  jobTitleId: number;
  shiftId: number;
}

export interface CompanyPositionDetailResponse {
  companyPositionId: number;
  stationNumber?: string | null;
  companyName?: string | null;
  shiftName?: string | null;
  jobTitleShortDesc?: string | null;
  truckPosition: number;
  companyId: number;
  stationId?: number | null;
  jobTitleId: number;
  shiftId: number;
}

export interface ShiftRequest {
  shiftCode: string;
  shiftNme?: string | null;
  sortOrder?: number | null;
  isActive: boolean;
}

export interface ShiftResponse {
  shiftId: number;
  shiftCode: string;
  shiftNme?: string | null;
  sortOrder?: number | null;
  isActive: boolean;
}

export interface JobTitleRequest {
  jobTitleDesc: string;
  jobTitleShortDesc: string;
  isActive: boolean;
  salaryRangeGradeCde: string;
}

export interface JobTitleResponse {
  jobTitleId: number;
  jobTitleDesc: string;
  jobTitleShortDesc: string;
  isActive: boolean;
  salaryRangeGradeCde: string;
}

export interface WorkPeriodRequest {
  weekDay: string;
  workPeriodNbr: number;
}

export interface WorkPeriodResponse {
  workPeriodId: number;
  weekDay: string;
  workPeriodNbr: number;
}

export interface StationDistanceRequest {
  distanceMiles: number;
  fromStationId: number;
  toStationId: number;
}

export interface StationDistanceResponse {
  stationDistanceId: number;
  distanceMiles: number;
  fromStationId: number;
  toStationId: number;
}