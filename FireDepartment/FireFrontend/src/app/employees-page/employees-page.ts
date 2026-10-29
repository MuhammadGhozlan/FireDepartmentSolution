import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import {
  CreateEmployeeRequest,
  EmployeeDetailResponse,
  EmployeeStatusCodeResponse,
  UpdateEmployeeRequest,
} from '../interfaces';
import { AppServices } from '../services/app-services';

interface EmployeeForm {
  roic: string;
  employeeNbr: number | null;
  firstNme: string;
  middleInitialNme: string;
  lastNme: string;
  suffix: string;
  paramedic: boolean;
  hazmat: boolean;
  employeeStatusCde: string;
  birthDate: string;
  maritalStatusCde: string;
  socialSecurityNbr: string;
}

@Component({
  selector: 'app-employees-page',
  standalone: false,
  templateUrl: './employees-page.html',
  styleUrl: './employees-page.css',
})
export class EmployeesPage {
  employees: EmployeeDetailResponse[] = [];
  statusCodes: EmployeeStatusCodeResponse[] = [];
  searchText = '';
  statusFilter = '';
  view: 'list' | 'create' | 'edit' = 'list';
  form: EmployeeForm = this.emptyForm();
  selectedEmployee: EmployeeDetailResponse | null = null;
  loading = false;
  loadingDetail = false;
  saving = false;
  errorMessage = '';
  notice = '';

  constructor(private service: AppServices, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    void this.loadEmployees();
    void this.loadStatusCodes();
  }

  async loadEmployees(): Promise<void> {
    this.loading = true;
    this.errorMessage = '';
    this.cdr.detectChanges();

    try {
      const result = await firstValueFrom(this.service.GetEmployees());
      this.employees = result.sort((a, b) => a.lastNme.localeCompare(b.lastNme));
    } catch (error) {
      this.errorMessage = this.errorText(error);
    } finally {
      this.loading = false;
      this.cdr.detectChanges();
    }
  }

  async loadStatusCodes(): Promise<void> {
    try {
      this.statusCodes = await firstValueFrom(this.service.GetEmployeeStatusCodes());
    } catch (error) {
      this.errorMessage = 'Employee statuses could not be loaded: ' + this.errorText(error);
    } finally {
      this.cdr.detectChanges();
    }
  }

  get visibleEmployees(): EmployeeDetailResponse[] {
    const search = this.searchText.trim().toLowerCase();
    const result: EmployeeDetailResponse[] = [];

    for (const employee of this.employees) {
      if (this.statusFilter && employee.employeeStatusCde !== this.statusFilter) continue;
      const name = this.fullName(employee).toLowerCase();
      const matches = !search || employee.roic.toLowerCase().includes(search) ||
        String(employee.employeeNbr).includes(search) || name.includes(search);
      if (matches) result.push(employee);
    }
    return result;
  }

  fullName(employee: EmployeeDetailResponse): string {
    return [employee.firstNme, employee.middleInitialNme, employee.lastNme, employee.suffix]
      .filter(Boolean).join(' ');
  }

  statusName(code: string): string {
    for (const status of this.statusCodes) {
      if (status.employeeStatusCde === code) return status.employeeStatusDesc;
    }
    return code;
  }

  startCreate(): void {
    this.form = this.emptyForm();
    this.selectedEmployee = null;
    this.view = 'create';
    this.errorMessage = '';
    this.notice = '';
  }

  async startEdit(roic: string): Promise<void> {
    this.loadingDetail = true;
    this.errorMessage = '';
    this.notice = '';
    this.cdr.detectChanges();

    try {
      const employee = await firstValueFrom(this.service.GetEmployeeByRoic(roic));
      this.selectedEmployee = employee;
      this.form = {
        roic: employee.roic,
        employeeNbr: employee.employeeNbr,
        firstNme: employee.firstNme,
        middleInitialNme: employee.middleInitialNme ?? '',
        lastNme: employee.lastNme,
        suffix: employee.suffix ?? '',
        paramedic: employee.paramedic,
        hazmat: employee.hazmat,
        employeeStatusCde: employee.employeeStatusCde,
        birthDate: employee.birthDate ?? '',
        maritalStatusCde: employee.maritalStatusCde ?? '',
        socialSecurityNbr: '',
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
    this.form = this.emptyForm();
    this.selectedEmployee = null;
    this.errorMessage = '';
  }

  async saveEmployee(): Promise<void> {
    if (this.saving) return;
    if (!this.form.firstNme.trim() || !this.form.lastNme.trim() || !this.form.employeeStatusCde ||
      (this.view === 'create' && (!this.form.roic.trim() || !this.form.employeeNbr || this.form.employeeNbr < 1))) {
      this.errorMessage = 'Fill in the required employee fields before saving.';
      this.cdr.detectChanges();
      return;
    }

    this.saving = true;
    this.errorMessage = '';
    this.notice = '';
    this.cdr.detectChanges();

    const now = new Date().toISOString();
    const details: UpdateEmployeeRequest = {
      firstNme: this.form.firstNme.trim(),
      middleInitialNme: this.form.middleInitialNme.trim() || null,
      lastNme: this.form.lastNme.trim(),
      suffix: this.form.suffix.trim() || null,
      paramedic: this.form.paramedic,
      hazmat: this.form.hazmat,
      employeeStatusCde: this.form.employeeStatusCde,
      birthDate: this.form.birthDate || null,
      maritalStatusCde: this.form.maritalStatusCde.trim() || null,
      lastUpdateDate: now,
      lastUpdateBy: 'System',
    };

    try {
      if (this.view === 'create') {
        const request: CreateEmployeeRequest = {
          ...details,
          roic: this.form.roic.trim(),
          employeeNbr: this.form.employeeNbr!,
          socialSecurityNbr: this.form.socialSecurityNbr.trim() || null,
          createdDate: now,
          createdBy: 'System',
        };
        await firstValueFrom(this.service.CreateEmployee(request));
      } else if (this.view === 'edit') {
        await firstValueFrom(this.service.UpdateEmployee(this.form.roic, details));
      }

      this.cancelEdit();
      await this.loadEmployees();
      this.notice = this.errorMessage
        ? 'Employee saved, but the list could not be refreshed.'
        : 'Employee saved.';
    } catch (error) {
      this.errorMessage = this.errorText(error);
    } finally {
      this.saving = false;
      this.cdr.detectChanges();
    }
  }

  private emptyForm(): EmployeeForm {
    return {
      roic: '',
      employeeNbr: null,
      firstNme: '',
      middleInitialNme: '',
      lastNme: '',
      suffix: '',
      paramedic: false,
      hazmat: false,
      employeeStatusCde: '',
      birthDate: '',
      maritalStatusCde: '',
      socialSecurityNbr: '',
    };
  }

  private errorText(error: unknown): string {
    const response = error as HttpErrorResponse;
    if (typeof response.error === 'string') return response.error;
    if (response.error?.title) return response.error.title;
    if (error instanceof Error) return error.message;
    return 'The employee request failed.';
  }
}
