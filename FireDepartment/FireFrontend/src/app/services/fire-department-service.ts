import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import {
  AttendanceRequest,
  AttendanceFilterRequest,
  UpdateAttendanceRequest,
  AttendanceStatusResponse,
  AttendanceResponse,
  DailyRosterResponse,
  AddressRequest,
  AddressResponse,
  PhoneNumberRequest,
  PhoneNumberResponse,
  EmailAddressRequest,
  EmailAddressResponse,
  EmergencyContactRequest,
  EmergencyContactResponse,
  CreateEmployeeRequest,
  UpdateEmployeeRequest,
  EmployeeSummaryResponse,
  EmployeeDetailResponse,
  LeaveRequestRequest,
  LeaveRequestResponse,
  LeaveTransactionRequest,
  LeaveTransactionResponse,
  StationResponse,
  DistrictResponse,
  CompanyResponse,
  ShiftResponse,
  JobTitleResponse
} from '../interfaces';

@Injectable({
  providedIn: 'root',
})
export class FireDepartmentService {
  readonly apiUrl = environment.API_URL;
  constructor(public http: HttpClient) { }

  getDailyRoster(roic: string, date: string) {
    return this.http.get<DailyRosterResponse[]>(`${this.apiUrl}/api/attendance/daily-roster`, { params: { roic, date } });
  }

  createAttendance(data: AttendanceRequest) {
    return this.http.post<AttendanceResponse>(`${this.apiUrl}/api/attendance`, data);
  }

  updateAttendance(id: number, data: UpdateAttendanceRequest) {
    return this.http.put<AttendanceResponse>(`${this.apiUrl}/api/attendance/${id}`, data);
  }

  filterAttendance(filter: any) {
    return this.http.post<any[]>(`${this.apiUrl}/api/attendance/filterAttendance`, filter);
  }

  // Employees
  getEmployees() {
    return this.http.get<EmployeeSummaryResponse[]>(`${this.apiUrl}/api/employees`);
  }

  getEmployeeDetail(roic: string) {
    return this.http.get<EmployeeDetailResponse>(`${this.apiUrl}/api/employees/${roic}`);
  }

  createEmployee(data: CreateEmployeeRequest) {
    return this.http.post<EmployeeDetailResponse>(`${this.apiUrl}/api/employees`, data);
  }

  updateEmployee(roic: string, data: UpdateEmployeeRequest) {
    return this.http.put<EmployeeDetailResponse>(`${this.apiUrl}/api/employees/${roic}`, data);
  }

  // Lookups
  getStations() {
    return this.http.get<StationResponse[]>(`${this.apiUrl}/api/lookups/stations`);
  }

  getDistricts() {
    return this.http.get<DistrictResponse[]>(`${this.apiUrl}/api/lookups/districts`);
  }

  getCompanies() {
    return this.http.get<CompanyResponse[]>(`${this.apiUrl}/api/lookups/companies`);
  }

  getShifts() {
    return this.http.get<ShiftResponse[]>(`${this.apiUrl}/api/lookups/shifts`);
  }

  getJobTitles() {
    return this.http.get<JobTitleResponse[]>(`${this.apiUrl}/api/lookups/job-titles`);
  }

  // Leave
  createLeaveRequest(data: LeaveRequestRequest) {
    return this.http.post<LeaveRequestResponse>(`${this.apiUrl}/api/leave/requests`, data);
  }

  createLeaveTransaction(data: LeaveTransactionRequest) {
    return this.http.post<LeaveTransactionResponse>(`${this.apiUrl}/api/leave/transactions`, data);
  }

  // Contacts
  createAddress(data: AddressRequest) {
    return this.http.post<AddressResponse>(`${this.apiUrl}/api/contacts/addresses`, data);
  }

  createPhoneNumber(data: PhoneNumberRequest) {
    return this.http.post<PhoneNumberResponse>(`${this.apiUrl}/api/contacts/phone-numbers`, data);
  }

  createEmailAddress(data: EmailAddressRequest) {
    return this.http.post<EmailAddressResponse>(`${this.apiUrl}/api/contacts/email-addresses`, data);
  }

  createEmergencyContact(data: EmergencyContactRequest) {
    return this.http.post<EmergencyContactResponse>(`${this.apiUrl}/api/contacts/emergency-contacts`, data);
  }

  getAttendanceStatuses() {
    return this.http.get<AttendanceStatusResponse[]>(`${this.apiUrl}/api/attendance/status`);
  }
}