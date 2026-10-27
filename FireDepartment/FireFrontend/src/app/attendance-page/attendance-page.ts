import { Component, OnInit } from '@angular/core';
import { FireDepartmentService } from '../services/fire-department-service';
import {
  AttendanceStatusResponse,
  ShiftResponse,
  StationResponse
} from '../interfaces';

@Component({
  selector: 'app-attendance-page',
  standalone: false,
  templateUrl: './attendance-page.html',
  styleUrl: './attendance-page.css',
})
export class AttendancePage implements OnInit {
  filterDate: string = new Date().toISOString().split('T')[0];
  selectedShift: any = '';
  selectedStation: any = '';
  searchTerm: string = '';

  statuses: AttendanceStatusResponse[] = [];
  shifts: ShiftResponse[] = [];
  stations: StationResponse[] = [];
  roster: any[] = []; // Changed to any[] to match backend properties flexibly

  constructor(private fireDepartmentService: FireDepartmentService) { }

  ngOnInit(): void {
    this.loadAttendanceStatuses();
    this.loadShifts();
    this.loadStations();
    this.onSearch();
  }

  loadAttendanceStatuses(): void {
    this.fireDepartmentService.getAttendanceStatuses().subscribe((data) => {
      this.statuses = data;
    });
  }

  loadShifts(): void {
    this.fireDepartmentService.getShifts().subscribe((data) => {
      this.shifts = data;
    });
  }

  loadStations(): void {
    this.fireDepartmentService.getStations().subscribe((data) => {
      this.stations = data;
    });
  }

  onSearch(): void {
    const formattedDate = new Date(this.filterDate).toISOString().split('T')[0];
    this.fireDepartmentService.filterAttendance({
      attendanceDate: formattedDate,
      roic: this.searchTerm || null,
      shiftId: this.selectedShift ? Number(this.selectedShift) : null,
      stationId: this.selectedStation ? Number(this.selectedStation) : null
    }).subscribe((data) => {
      this.roster = data;
    });
  }

  onCancel(): void {
    this.searchTerm = '';
    this.selectedShift = '';
    this.selectedStation = '';
    this.roster = [];
  }

  saveChanges(): void {
    // Implement save logic
  }

  onStatusSelected(row: any, st: AttendanceStatusResponse): void {
    row.attendanceStatusId = st.attendanceStatusId;
    row.statusDescription = st.attendanceStatusDesc;
  }
}