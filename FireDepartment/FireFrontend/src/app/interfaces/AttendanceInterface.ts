export interface AttendanceRequest {
  attendanceDate: string;
  roic: string;
  attendanceStatusId: number;
  attendanceComments?: string | null;
  employeeAssignmentId?: number | null;
}

export interface AttendanceFilterRequest {
  attendanceDate?: string | null;
  roic?: string | null;
  employeeAssignmentId?: number | null;
  attendanceStatusId?: number | null;
  attendanceComments?: string | null;
  startDate?: string | null;
  endDate?: string | null;
}

export interface UpdateAttendanceRequest {
  attendanceDate?: string | null;
  attendanceStatusId?: number | null;
  attendanceComments?: string | null;
}

export interface AttendanceResponse {
  attendanceId: number;
  attendanceDate: string;
  roic: string;
  employeeAssignmentId: number;
  attendanceStatusId: number;
  attendanceComments?: string | null;
  deletedInd?: boolean | null;
  isOpen?: boolean;
}

export interface DailyRosterResponse {
  employeeAssignmentId: number;
  roic: string;
  workPeriodNbr: number;
  attendance?: AttendanceResponse | null;
}