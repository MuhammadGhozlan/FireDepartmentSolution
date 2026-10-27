export interface LeaveRequestRequest {
  leaveRequestHours: number;
  leaveStartDate: string;
  leaveEndDate: string;
  leaveDetailId: number;
  requesterComment?: string | null;
}

export interface LeaveRequestResponse {
  leaveRequestId: number;
  leaveRequestSubmitDate: string;
  leaveRequestHours: number;
  leaveStartDate: string;
  leaveEndDate: string;
  requesterRoic: string;
  leaveDetailId: number;
  approverRoic?: string | null;
  approvalStatusId: number;
  requesterComment?: string | null;
  approverComment?: string | null;
}

export interface LeaveTransactionRequest {
  roic: string;
  leaveHours: number;
  leaveTransactionDate: string;
  leaveDetailId: number;
  leaveTransactionTypeId: number;
  leaveTransactionComments?: string | null;
}

export interface LeaveTransactionResponse {
  leaveTransactionId: number;
  roic: string;
  leaveHours: number;
  leaveTransactionDate: string;
  leaveDetailId: number;
  leaveTransactionTypeId: number;
  leaveTransactionComments?: string | null;
}