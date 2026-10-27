export interface LeaveCategoryRequest {
  leaveCategoryCode: string;
  leaveCategoryDesc: string;
}

export interface LeaveCategoryResponse {
  leaveCategoryId: number;
  leaveCategoryCode: string;
  leaveCategoryDesc: string;
}

export interface LeaveDetailCodeRequest {
  leaveDetailCode1: string;
  leaveDetailDesc: string;
  leaveCategoryId: number;
}

export interface LeaveDetailCodeResponse {
  leaveDetailId: number;
  leaveDetailCode1: string;
  leaveDetailDesc: string;
  leaveCategoryId: number;
}

export interface LeaveTransactionTypeRequest {
  leaveTransactionDesc: string;
}

export interface LeaveTransactionTypeResponse {
  leaveTransactionTypeId: number;
  leaveTransactionDesc: string;
}

export interface LeaveDefaultHourRequest {
  defaultHours: number;
  leaveCategoryId: number;
  leaveTransactionTypeId: number;
  shiftId: number;
  leaveDetailId: number;
}

export interface LeaveDefaultHourResponse {
  leaveDefaultHoursId: number;
  defaultHours: number;
  leaveCategoryId: number;
  leaveTransactionTypeId: number;
  shiftId: number;
  leaveDetailId: number;
}

export interface SickSellbackCodeRequest {
  sickSellbackCode1: string;
  sickSellbackDesc: string;
  sickSellbackCodeComments?: string | null;
  shiftId: number;
}

export interface SickSellbackCodeResponse {
  sickSellbackCodeId: number;
  sickSellbackCode1: string;
  sickSellbackDesc: string;
  sickSellbackCodeComments?: string | null;
  shiftId: number;
}