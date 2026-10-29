import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environments } from '../../environments/environments';
import { AttendanceRequest, 
  AttendanceFilterRequest, 
  UpdateAttendanceRequest, 
  AttendanceResponse, 
  AttendanceStatusResponse, 
  DailyRosterResponse, 
  StationResponse,
  CompanyResponse,
  CompanyPositionDetailResponse,
  ShiftResponse,
  JobTitleResponse,
  WorkPeriodResponse,
  EmployeeStatusCodeResponse,
  EmployeeDetailResponse,
  CreateEmployeeRequest,
  UpdateEmployeeRequest,
  EmployeeAssignmentRequest,
  EmployeeAssignmentResponse} from '../interfaces';

@Injectable({
  providedIn: 'root',
})
export class AppServices {
  readonly api_url = environments.API_Url;
  constructor(public http: HttpClient) { }

  //Attendance Methods
  GetAttendance(): Observable<AttendanceResponse[]> {
    return this.http.get<AttendanceResponse[]>(`${this.api_url}/api/attendance/getAttendance`)
  }

  GetAttendanceStatuses():Observable<AttendanceStatusResponse[]>{
    return this.http.get<AttendanceStatusResponse[]>(`${this.api_url}/api/attendance/status`);
  }

  TakeAttendance(attendanceRequests: AttendanceRequest[]): Observable<AttendanceResponse[]> {
    return this.http.post<AttendanceResponse[]>(`${this.api_url}/api/attendance/takeAttendance`, attendanceRequests);
  }

  FilterAttendance(attendanceFilterRequest: AttendanceFilterRequest): Observable<AttendanceResponse[]> {
    return this.http.post<AttendanceResponse[]>(`${this.api_url}/api/attendance/filterAttendance`, attendanceFilterRequest);
  }

  UpdateAttendance(id: number, updateAttendanceRequest: UpdateAttendanceRequest): Observable<AttendanceResponse> {
    return this.http.put<AttendanceResponse>(`${this.api_url}/api/attendance/updateAttendance/${id}`, updateAttendanceRequest);
  }

  DeleteAttendance(id: number): Observable<AttendanceResponse> {
    return this.http.delete<AttendanceResponse>(`${this.api_url}/api/attendance/deleteAttendance/${id}`);
  }

  GetDailyRoster(date: string, workPeriodNbr?: number | null): Observable<DailyRosterResponse[]> {
    let params = new HttpParams().set('date', date);
    if (workPeriodNbr != null) {
      params = params.set('workPeriodNbr', workPeriodNbr.toString());
    }
    return this.http.get<DailyRosterResponse[]>(`${this.api_url}/api/attendance/daily-roster`, { params });
  }

  GetEmployees(): Observable<EmployeeDetailResponse[]> {
    return this.http.get<EmployeeDetailResponse[]>(`${this.api_url}/api/employee/getEmployees`);
  }

  GetEmployeeByRoic(roic: string): Observable<EmployeeDetailResponse> {
    return this.http.get<EmployeeDetailResponse>(`${this.api_url}/api/employee/getEmployeeByRoic/${encodeURIComponent(roic)}`);
  }

  CreateEmployee(request: CreateEmployeeRequest): Observable<EmployeeDetailResponse> {
    return this.http.post<EmployeeDetailResponse>(`${this.api_url}/api/employee/createEmployee`, request);
  }

  UpdateEmployee(roic: string, request: UpdateEmployeeRequest): Observable<EmployeeDetailResponse> {
    return this.http.put<EmployeeDetailResponse>(`${this.api_url}/api/employee/updateEmployee/${encodeURIComponent(roic)}`, request);
  }

  GetEmployeeAssignments(): Observable<EmployeeAssignmentResponse[]> {
    return this.http.get<EmployeeAssignmentResponse[]>(`${this.api_url}/api/employeeAssignment/getEmployeeAssignments`);
  }

  GetEmployeeAssignmentsByRoic(roic: string): Observable<EmployeeAssignmentResponse[]> {
    return this.http.get<EmployeeAssignmentResponse[]>(`${this.api_url}/api/employeeAssignment/getEmployeeAssignmentsByEmployeeNbr/${encodeURIComponent(roic)}`);
  }

  GetEmployeeAssignmentById(roic: string, assignmentId: number): Observable<EmployeeAssignmentResponse> {
    return this.http.get<EmployeeAssignmentResponse>(`${this.api_url}/api/employeeAssignment/getEmployeeAssignmentById/${encodeURIComponent(roic)}/${assignmentId}`);
  }

  CreateEmployeeAssignment(request: EmployeeAssignmentRequest): Observable<EmployeeAssignmentResponse> {
    return this.http.post<EmployeeAssignmentResponse>(`${this.api_url}/api/employeeAssignment/createEmployeeAssignment`, request);
  }

  UpdateEmployeeAssignment(assignmentId: number, request: EmployeeAssignmentRequest): Observable<EmployeeAssignmentResponse> {
    return this.http.put<EmployeeAssignmentResponse>(`${this.api_url}/api/employeeAssignment/updateEmployeeAssignment/${assignmentId}`, request);
  }

  DeleteEmployeeAssignment(roic: string, assignmentId: number): Observable<void> {
    return this.http.delete<void>(`${this.api_url}/api/employeeAssignment/deleteEmployeeAssignment/${encodeURIComponent(roic)}/${assignmentId}`);
  }

  //Organizational Lookups
  GetAllStations(): Observable<StationResponse[]> {
    return this.http.get<StationResponse[]>(`${this.api_url}/api/lookups/stations`);
  }

  GetAllCompanies(): Observable<CompanyResponse[]> {
    return this.http.get<CompanyResponse[]>(`${this.api_url}/api/lookups/companies`);
  }

  GetAllCompanyPositions(): Observable<CompanyPositionDetailResponse[]> {
    return this.http.get<CompanyPositionDetailResponse[]>(`${this.api_url}/api/lookups/company-positions`);
  }

  GetAllShifts(): Observable<ShiftResponse[]> {
    return this.http.get<ShiftResponse[]>(`${this.api_url}/api/lookups/shifts`);
  }

  GetAllJobTitles(): Observable<JobTitleResponse[]> {
    return this.http.get<JobTitleResponse[]>(`${this.api_url}/api/lookups/job-titles`);
  }

  GetWorkPeriods(): Observable<WorkPeriodResponse[]> {
    return this.http.get<WorkPeriodResponse[]>(`${this.api_url}/api/lookups/work-periods`);
  }

  GetEmployeeStatusCodes(): Observable<EmployeeStatusCodeResponse[]> {
    return this.http.get<EmployeeStatusCodeResponse[]>(`${this.api_url}/api/lookups/employee-status-codes`);
  }
}
