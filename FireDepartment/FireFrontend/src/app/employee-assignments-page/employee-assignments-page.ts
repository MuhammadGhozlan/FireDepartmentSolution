import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import {
  CompanyPositionDetailResponse,
  EmployeeAssignmentRequest,
  EmployeeAssignmentResponse,
  EmployeeDetailResponse,
  ShiftResponse,
  WorkPeriodResponse,
} from '../interfaces';
import { AppServices } from '../services/app-services';

interface AssignmentForm {
  roic: string;
  assignmentStartDate: string;
  assignmentEndDate: string;
  isTemp: boolean;
  companyPositionId: number | null;
  workPeriodId: number | null;
}

@Component({
  selector: 'app-employee-assignments-page',
  standalone: false,
  templateUrl: './employee-assignments-page.html',
  styleUrl: './employee-assignments-page.css',
})
export class EmployeeAssignmentsPage {
  assignments: EmployeeAssignmentResponse[] = [];
  companyPositions: CompanyPositionDetailResponse[] = [];
  workPeriods: WorkPeriodResponse[] = [];
  employeeNames: Record<string, string> = {};
  shiftCodes: Record<number, string> = {};
  roicQuery = '';
  searchedRoic = '';
  typeFilter = '';
  activeDate = '';
  view: 'list' | 'create' | 'edit' = 'list';
  form: AssignmentForm = this.emptyForm();
  selectedAssignmentId: number | null = null;
  loading = false;
  loadingDetail = false;
  loadingLookups = false;
  saving = false;
  deletingId: number | null = null;
  errorMessage = '';
  lookupError = '';
  notice = '';

  constructor(private service: AppServices, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    void this.loadAssignments();
    void this.loadLookups();
  }

  async loadAssignments(): Promise<void> {
    this.loading = true;
    this.errorMessage = '';
    this.cdr.detectChanges();
    try {
      const result = await firstValueFrom(this.service.GetEmployeeAssignments());
      this.assignments = result.sort((a, b) => a.roic.localeCompare(b.roic));
      this.searchedRoic = '';
      this.roicQuery = '';
    } catch (error) {
      this.errorMessage = this.errorText(error);
    } finally {
      this.loading = false;
      this.cdr.detectChanges();
    }
  }

  async findByRoic(): Promise<void> {
    const roic = this.roicQuery.trim();
    if (!roic) return;
    this.loading = true;
    this.errorMessage = '';
    this.cdr.detectChanges();
    try {
      this.assignments = await firstValueFrom(this.service.GetEmployeeAssignmentsByRoic(roic));
      this.searchedRoic = roic;
    } catch (error) {
      this.errorMessage = this.errorText(error);
    } finally {
      this.loading = false;
      this.cdr.detectChanges();
    }
  }

  async loadLookups(): Promise<void> {
    this.loadingLookups = true;
    this.lookupError = '';
    this.cdr.detectChanges();
    try {
      this.companyPositions = await firstValueFrom(this.service.GetAllCompanyPositions());
      this.workPeriods = await firstValueFrom(this.service.GetWorkPeriods());
      const shifts: ShiftResponse[] = await firstValueFrom(this.service.GetAllShifts());
      for (const shift of shifts) {
        this.shiftCodes[shift.shiftId] = shift.shiftCode;
      }
      const employees = await firstValueFrom(this.service.GetEmployees());
      this.employeeNames = {};
      for (const employee of employees) {
        this.employeeNames[employee.roic] = this.fullName(employee);
      }
    } catch (error) {
      this.lookupError = 'Assignment lookups or employee names could not be loaded: ' + this.errorText(error);
    } finally {
      this.loadingLookups = false;
      this.cdr.detectChanges();
    }
  }

  get visibleAssignments(): EmployeeAssignmentResponse[] {
    const result: EmployeeAssignmentResponse[] = [];
    for (const assignment of this.assignments) {
      if (this.typeFilter === 'temporary' && !assignment.isTemp) continue;
      if (this.typeFilter === 'regular' && assignment.isTemp) continue;
      if (this.activeDate && (assignment.assignmentStartDate > this.activeDate ||
        (assignment.assignmentEndDate && assignment.assignmentEndDate < this.activeDate))) continue;
      result.push(assignment);
    }
    return result;
  }

  get endDateBeforeStart(): boolean {
    return !!this.form.assignmentStartDate && !!this.form.assignmentEndDate &&
      this.form.assignmentEndDate < this.form.assignmentStartDate;
  }

  employeeName(roic: string): string {
    return this.employeeNames[roic] || '-';
  }

  positionName(id: number): string {
    const position = this.companyPositions.find(item => item.companyPositionId === id);
    if (!position) return `Position ${id}`;
    return [position.stationNumber ? `Station ${position.stationNumber}` : '',
      position.companyName, position.jobTitleShortDesc,
      `Shift ${this.shiftCodes[position.shiftId] || position.shiftName || position.shiftId}`,
      `Position ${position.truckPosition}`, `ID ${id}`].filter(Boolean).join(' / ');
  }

  positionSummary(id: number): string {
    const position = this.companyPositions.find(item => item.companyPositionId === id);
    if (!position) return `Position ${id}`;
    return `${position.jobTitleShortDesc || 'Position'} / ${this.shiftCodes[position.shiftId] || position.shiftName || '-'} / #${position.truckPosition}`;
  }

  workPeriodName(id: number): string {
    const period = this.workPeriods.find(item => item.workPeriodId === id);
    return period ? String(period.workPeriodNbr) : String(id);
  }

  startCreate(): void {
    this.form = this.emptyForm();
    this.selectedAssignmentId = null;
    this.view = 'create';
    this.errorMessage = '';
    this.notice = '';
  }

  async startEdit(assignment: EmployeeAssignmentResponse): Promise<void> {
    this.loadingDetail = true;
    this.errorMessage = '';
    this.notice = '';
    this.cdr.detectChanges();
    try {
      const detail = await firstValueFrom(this.service.GetEmployeeAssignmentById(
        assignment.roic, assignment.employeeAssignmentId));
      this.selectedAssignmentId = detail.employeeAssignmentId;
      this.form = {
        roic: detail.roic,
        assignmentStartDate: detail.assignmentStartDate,
        assignmentEndDate: detail.assignmentEndDate ?? '',
        isTemp: detail.isTemp,
        companyPositionId: detail.companyPositionId,
        workPeriodId: detail.workPeriodId,
      };
      this.view = 'edit';
    } catch (error) {
      this.errorMessage = this.errorText(error);
    } finally {
      this.loadingDetail = false;
      this.cdr.detectChanges();
    }
  }

  cancelEdit(): void {
    this.view = 'list';
    this.selectedAssignmentId = null;
    this.form = this.emptyForm();
    this.errorMessage = '';
  }

  async saveAssignment(): Promise<void> {
    if (this.saving) return;
    if (!this.form.roic.trim() || !this.form.assignmentStartDate ||
      !this.form.companyPositionId || !this.form.workPeriodId) {
      this.errorMessage = 'Fill in the required assignment fields before saving.';
      this.cdr.detectChanges();
      return;
    }
    if (this.endDateBeforeStart) {
      this.errorMessage = 'End date cannot be earlier than start date.';
      this.cdr.detectChanges();
      return;
    }

    const request: EmployeeAssignmentRequest = {
      roic: this.form.roic.trim(),
      assignmentStartDate: this.form.assignmentStartDate,
      assignmentEndDate: this.form.assignmentEndDate || null,
      isTemp: this.form.isTemp,
      companyPositionId: this.form.companyPositionId,
      workPeriodId: this.form.workPeriodId,
    };
    this.saving = true;
    this.errorMessage = '';
    this.notice = '';
    this.cdr.detectChanges();
    try {
      if (this.view === 'create') {
        await firstValueFrom(this.service.CreateEmployeeAssignment(request));
      } else if (this.view === 'edit' && this.selectedAssignmentId !== null) {
        await firstValueFrom(this.service.UpdateEmployeeAssignment(this.selectedAssignmentId, request));
      } else {
        return;
      }
      this.cancelEdit();
      await this.loadAssignments();
      this.notice = this.errorMessage
        ? 'Assignment saved, but the list could not be refreshed.' : 'Assignment saved.';
    } catch (error) {
      this.errorMessage = this.errorText(error);
    } finally {
      this.saving = false;
      this.cdr.detectChanges();
    }
  }

  async deleteAssignment(assignment: EmployeeAssignmentResponse): Promise<void> {
    if (this.deletingId !== null || !window.confirm(`Delete assignment ${assignment.employeeAssignmentId} for ROIC ${assignment.roic}?`)) return;
    this.deletingId = assignment.employeeAssignmentId;
    this.errorMessage = '';
    this.notice = '';
    this.cdr.detectChanges();
    try {
      await firstValueFrom(this.service.DeleteEmployeeAssignment(assignment.roic, assignment.employeeAssignmentId));
      await this.loadAssignments();
      this.notice = this.errorMessage
        ? 'Assignment deleted, but the list could not be refreshed.' : 'Assignment deleted.';
    } catch (error) {
      this.errorMessage = this.errorText(error);
    } finally {
      this.deletingId = null;
      this.cdr.detectChanges();
    }
  }

  private emptyForm(): AssignmentForm {
    return {
      roic: '', assignmentStartDate: '', assignmentEndDate: '', isTemp: false,
      companyPositionId: null, workPeriodId: null,
    };
  }

  private fullName(employee: EmployeeDetailResponse): string {
    return [employee.firstNme, employee.middleInitialNme, employee.lastNme, employee.suffix]
      .filter(Boolean).join(' ');
  }

  private errorText(error: unknown): string {
    const response = error as HttpErrorResponse;
    if (typeof response.error === 'string') return response.error;
    if (response.error?.title) return response.error.title;
    if (error instanceof Error) return error.message;
    return 'The assignment request failed.';
  }
}
