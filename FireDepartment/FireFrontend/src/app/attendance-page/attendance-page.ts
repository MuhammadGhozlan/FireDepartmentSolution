import { ChangeDetectorRef, Component } from '@angular/core';
import { AppServices } from '../services/app-services';
import { AttendanceFilterRequest, AttendanceRequest, AttendanceResponse, UpdateAttendanceRequest, WorkPeriodResponse } from '../interfaces';

@Component({
  selector: 'app-attendance-page',
  standalone: false,
  templateUrl: './attendance-page.html',
  styleUrl: './attendance-page.css',
})
export class AttendancePage {
  status: { [key: string | number]: string } = {}
  attendanceResponse: AttendanceResponse[] = [];
  object = Object.keys;
  filterRequest: AttendanceFilterRequest = {};
  workPeriods: WorkPeriodResponse[] = [];
  selectedWorkPeriod: number | null = null;;
  constructor(private service: AppServices, private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    this.getAttendanceStatuses();
    this.getWorkPeriods();
    this.getAttendance();
  }

  getAttendanceStatuses() {
    this.service.GetAttendanceStatuses().subscribe({
      next: res => {
        console.log('API Response', res);
        if (res) {
          res.forEach(s => this.status[s.attendanceStatusId] = s.attendanceStatusDesc)
          this.cdr.detectChanges();
        }
      },
      error: err => {
        console.log(err)

      },
      complete: () => console.log('request has been completed')
    })
  }

  getWorkPeriods() {
    this.service.GetWorkPeriods().subscribe({
      next: res => {
        this.workPeriods = res;
        this.cdr.detectChanges();
      },
      error: err => console.log(err)
    });
  }

  getAttendance() {
    this.service.GetAttendance().subscribe({
      next: res => {
        console.log('API Response', res);
        if (res) {
          this.attendanceResponse = res.map(item => ({ ...item, isOpen: false }));
          this.cdr.detectChanges();
        }
      },
      error: err => {
        console.log(err)

      },
      complete: () => console.log('request has been completed')
    })
  }
  TakeAttendance(attendanceRequests: AttendanceRequest[]) {
    this.service.TakeAttendance(attendanceRequests).subscribe({
      next: res => {
        this.attendanceResponse = res.map(item => ({ ...item, isOpen: false }));
        this.cdr.detectChanges();
      },
      error: err => console.log(err)
    });
  }

  FilterAttendance(attendanceFilterRequest: AttendanceFilterRequest) {
    this.service.FilterAttendance(attendanceFilterRequest).subscribe({
      next: res => {
        this.attendanceResponse = res.map(item => ({ ...item, isOpen: false }));
        this.cdr.detectChanges();
      },
      error: err => console.log(err)
    });
  }

  UpdateAttendance(id: number, updateAttendanceRequest: UpdateAttendanceRequest) {
    this.service.UpdateAttendance(id, updateAttendanceRequest).subscribe({
      next: () => {
        this.getAttendance();
        this.cdr.detectChanges();
      },
      error: err => console.log(err)
    });
  }

  DeleteAttendance(id: number) {
    this.service.DeleteAttendance(id).subscribe({
      next: () => {
        this.getAttendance();
        this.cdr.detectChanges();
      },
      error: err => console.log(err)
    });
  }

  GetDailyRoster(date: string, workPeriodNbr: number | null) {
    this.service.GetDailyRoster(date, workPeriodNbr).subscribe({
      next: res => {
        this.attendanceResponse = res.map(r => r.attendance ?? {
          attendanceId: 0,
          attendanceDate: date,
          roic: r.roic,
          employeeAssignmentId: r.employeeAssignmentId,
          attendanceStatusId: 1,
          attendanceComments: '',
          isOpen: false
        });
        this.cdr.detectChanges();
      },
      error: err => console.log(err)
    });
  }
  loadRoster() {
    this.GetDailyRoster(this.filterRequest.attendanceDate!, this.selectedWorkPeriod);
  }
  onSearch() {
    if (this.filterRequest.attendanceDate) {
      this.GetDailyRoster(this.filterRequest.attendanceDate, this.selectedWorkPeriod);
    } else if (this.filterRequest.attendanceStatusId != null || this.filterRequest.roic) {
      this.FilterAttendance(this.filterRequest);
    }
  }
  resetFilters() {
    this.selectedWorkPeriod = null;
    this.filterRequest = {};
    this.getAttendance();
  }
}
