import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import {
  AttendanceFilterRequest,
  AttendanceRequest,
  AttendanceResponse,
  AttendanceStatusResponse,
  DailyRosterResponse,
  UpdateAttendanceRequest,
} from '../interfaces';
import { AppServices } from '../services/app-services';

interface AttendanceRow {
  key: string;
  attendanceId: number | null;
  attendanceDate: string;
  roic: string;
  employeeAssignmentId: number;
  workPeriodNbr: number | null;
  attendanceStatusId: number | null;
  attendanceComments: string;
  originalStatusId: number | null;
  originalComments: string;
}

@Component({
  selector: 'app-attendance-page',
  standalone: false,
  templateUrl: './attendance-page.html',
  styleUrl: './attendance-page.css',
})
export class AttendancePage {
  statuses: AttendanceStatusResponse[] = [];
  rows: AttendanceRow[] = [];
  filterRequest: AttendanceFilterRequest = { attendanceStatusId: null };
  nameSearch = '';
  employeeNames: Record<string, string> = {};
  employeeNamesLoading = true;
  employeeNamesLoaded = false;
  workPeriods: number[] = [];
  selectedWorkPeriod: number | null = null;
  loading = false;
  saving = false;
  workPeriodsLoading = true;
  errorMessage = '';
  notice = '';

  constructor(private service: AppServices, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.getAttendanceStatuses();
    this.getWorkPeriods();
    this.getEmployees();
    void this.loadResults();
  }

  getEmployees(): void {
    this.service.GetEmployees().subscribe({
      next: employees => {
        const names: Record<string, string> = {};
        for (const employee of employees) {
          let name = employee.firstNme.trim();
          if (employee.middleInitialNme) name += ' ' + employee.middleInitialNme.trim();
          name += ' ' + employee.lastNme.trim();
          if (employee.suffix) name += ' ' + employee.suffix.trim();
          names[employee.roic] = name.trim();
        }
        this.employeeNames = names;
        this.employeeNamesLoaded = true;
        this.employeeNamesLoading = false;
        this.cdr.detectChanges();
      },
      error: error => {
        this.employeeNamesLoading = false;
        this.errorMessage = 'Employee names could not be loaded: ' + this.errorText(error);
        this.cdr.detectChanges();
      },
    });
  }

  getAttendanceStatuses(): void {
    this.service.GetAttendanceStatuses().subscribe({
      next: statuses => {
        this.statuses = statuses;
        this.cdr.detectChanges();
      },
      error: error => {
        this.errorMessage = this.errorText(error);
        this.cdr.detectChanges();
      },
    });
  }

  getWorkPeriods(): void {
    this.service.GetWorkPeriods().subscribe({
      next: periods => {
        const numbers: number[] = [];
        for (const period of periods) {
          if (!numbers.includes(period.workPeriodNbr)) {
            numbers.push(period.workPeriodNbr);
          }
        }
        this.workPeriods = numbers.sort((a, b) => a - b);
        this.workPeriodsLoading = false;
        this.cdr.detectChanges();
      },
      error: error => {
        this.workPeriodsLoading = false;
        this.errorMessage = this.errorText(error);
        this.cdr.detectChanges();
      },
    });
  }

  async loadResults(): Promise<void> {
    this.loading = true;
    this.rows = [];
    this.cdr.detectChanges();

    try {
      if (this.nameSearch.trim() && !this.employeeNamesLoaded) {
        throw new Error('Employee names are unavailable. Search by ROIC or retry later.');
      }
      const date = this.filterRequest.attendanceDate;
      if (date) {
        const periods = this.selectedWorkPeriod === null
          ? this.workPeriods
          : [this.selectedWorkPeriod];

        if (periods.length === 0) {
          throw new Error('Work periods are unavailable. Retry after the lookup loads.');
        }

        const roster: DailyRosterResponse[] = [];
        // The roster API requires a period, so request each one for "All Work Periods".
        for (const period of periods) {
          const result = await firstValueFrom(this.service.GetDailyRoster(date, period));
          roster.push(...result);
        }

        const roic = (this.filterRequest.roic ?? '').trim().toLowerCase();
        const statusId = this.filterRequest.attendanceStatusId;
        const matchingRows: AttendanceRow[] = [];

        for (const item of roster) {
          const row = this.rosterRow(item, date);
          if (roic && row.roic.toLowerCase() !== roic) continue;
          if (!this.nameMatches(row.roic)) continue;
          if (statusId != null && row.attendanceStatusId !== statusId) continue;
          matchingRows.push(row);
        }
        this.rows = matchingRows;
      } else {
        if (this.selectedWorkPeriod !== null) {
          throw new Error('Choose a date before filtering by work period.');
        }

        let attendance: AttendanceResponse[];
        if (this.filterRequest.roic?.trim() || this.filterRequest.attendanceStatusId != null) {
          const request: AttendanceFilterRequest = {
            roic: this.filterRequest.roic?.trim() || null,
            attendanceStatusId: this.filterRequest.attendanceStatusId ?? null,
          };
          attendance = await firstValueFrom(this.service.FilterAttendance(request));
        } else {
          attendance = await firstValueFrom(this.service.GetAttendance());
        }
        for (const item of attendance) {
          if (this.nameMatches(item.roic)) this.rows.push(this.savedRow(item));
        }
      }
    } catch (error) {
      this.errorMessage = this.errorText(error);
    } finally {
      this.loading = false;
      this.cdr.detectChanges();
    }
  }

  onSearch(): Promise<void> {
    this.errorMessage = '';
    this.notice = '';
    return this.loadResults();
  }

  resetFilters(): Promise<void> {
    this.filterRequest = { attendanceStatusId: null };
    this.nameSearch = '';
    this.selectedWorkPeriod = null;
    return this.onSearch();
  }

  cancelEdits(): Promise<void> {
    this.errorMessage = '';
    this.notice = '';
    return this.loadResults();
  }

  async saveAll(): Promise<void> {
    if (this.saving) return;

    const newRecords: AttendanceRequest[] = [];
    const editedRows: AttendanceRow[] = [];

    for (const row of this.rows) {
      if (row.attendanceId === null) {
        if (row.attendanceStatusId !== null) {
          newRecords.push(this.createRequest(row));
        } else if (row.attendanceComments.trim()) {
          this.errorMessage = 'Choose a status for every new row with comments before saving.';
          this.cdr.detectChanges();
          return;
        }
      } else if (this.isDirty(row)) {
        editedRows.push(row);
      }
    }

    if (newRecords.length === 0 && editedRows.length === 0) {
      this.notice = 'No attendance changes to save.';
      this.cdr.detectChanges();
      return;
    }

    this.saving = true;
    this.errorMessage = '';
    this.notice = '';

    try {
      if (newRecords.length > 0) {
        await firstValueFrom(this.service.TakeAttendance(newRecords));
      }
      // Existing records use the single-row update endpoint.
      for (const row of editedRows) {
        await firstValueFrom(this.service.UpdateAttendance(row.attendanceId!, this.updateRequest(row)));
      }
      this.notice = 'Attendance changes saved.';
    } catch (error) {
      this.errorMessage = this.errorText(error);
    } finally {
      this.saving = false;
      await this.loadResults();
    }
  }

  async saveRow(row: AttendanceRow): Promise<void> {
    if (this.saving) return;
    if (row.attendanceId === null && row.attendanceStatusId === null) {
      this.errorMessage = 'Choose an attendance status before saving this row.';
      this.cdr.detectChanges();
      return;
    }
    if (row.attendanceId !== null && !this.isDirty(row)) return;

    this.saving = true;
    this.errorMessage = '';
    this.notice = '';

    try {
      if (row.attendanceId === null) {
        await firstValueFrom(this.service.TakeAttendance([this.createRequest(row)]));
      } else {
        await firstValueFrom(this.service.UpdateAttendance(row.attendanceId, this.updateRequest(row)));
      }
      this.notice = 'Attendance changes saved.';
    } catch (error) {
      this.errorMessage = this.errorText(error);
    } finally {
      this.saving = false;
      await this.loadResults();
    }
  }

  async deleteRow(row: AttendanceRow): Promise<void> {
    if (this.saving || row.attendanceId === null) return;
    if (!window.confirm('Delete attendance for ' + row.roic + ' on ' + row.attendanceDate + '?')) return;

    this.saving = true;
    this.errorMessage = '';
    this.notice = '';

    try {
      await firstValueFrom(this.service.DeleteAttendance(row.attendanceId));
      this.notice = 'Attendance deleted.';
    } catch (error) {
      this.errorMessage = this.errorText(error);
    } finally {
      this.saving = false;
      await this.loadResults();
    }
  }

  isDirty(row: AttendanceRow): boolean {
    return row.attendanceId !== null &&
      (row.attendanceStatusId !== row.originalStatusId ||
        row.attendanceComments !== row.originalComments);
  }

  hasPendingChanges(): boolean {
    for (const row of this.rows) {
      if (row.attendanceId === null && row.attendanceStatusId !== null) return true;
      if (row.attendanceId === null && row.attendanceComments.trim()) return true;
      if (this.isDirty(row)) return true;
    }
    return false;
  }

  private createRequest(row: AttendanceRow): AttendanceRequest {
    return {
      attendanceDate: row.attendanceDate,
      roic: row.roic,
      employeeAssignmentId: row.employeeAssignmentId,
      attendanceStatusId: row.attendanceStatusId!,
      attendanceComments: row.attendanceComments,
    };
  }

  private updateRequest(row: AttendanceRow): UpdateAttendanceRequest {
    return {
      attendanceStatusId: row.attendanceStatusId,
      attendanceComments: row.attendanceComments !== row.originalComments
        ? row.attendanceComments
        : null,
    };
  }

  private savedRow(attendance: AttendanceResponse, workPeriodNbr: number | null = null): AttendanceRow {
    const comments = attendance.attendanceComments ?? '';
    return {
      key: 'attendance-' + attendance.attendanceId,
      attendanceId: attendance.attendanceId,
      attendanceDate: attendance.attendanceDate,
      roic: attendance.roic,
      employeeAssignmentId: attendance.employeeAssignmentId,
      workPeriodNbr,
      attendanceStatusId: attendance.attendanceStatusId,
      attendanceComments: comments,
      originalStatusId: attendance.attendanceStatusId,
      originalComments: comments,
    };
  }

  private rosterRow(roster: DailyRosterResponse, date: string): AttendanceRow {
    if (roster.attendance) return this.savedRow(roster.attendance, roster.workPeriodNbr);
    return {
      key: 'assignment-' + roster.employeeAssignmentId + '-' + date,
      attendanceId: null,
      attendanceDate: date,
      roic: roster.roic,
      employeeAssignmentId: roster.employeeAssignmentId,
      workPeriodNbr: roster.workPeriodNbr,
      attendanceStatusId: null,
      attendanceComments: '',
      originalStatusId: null,
      originalComments: '',
    };
  }

  private nameMatches(roic: string): boolean {
    const search = this.nameSearch.trim().toLowerCase();
    if (!search) return true;
    const name = this.employeeNames[roic] ?? '';
    return name.toLowerCase().includes(search);
  }

  private errorText(error: unknown): string {
    const response = error as HttpErrorResponse;
    if (typeof response.error === 'string') return response.error;
    if (response.error?.title) return response.error.title;
    if (error instanceof Error) return error.message;
    return 'The attendance request failed.';
  }
}
