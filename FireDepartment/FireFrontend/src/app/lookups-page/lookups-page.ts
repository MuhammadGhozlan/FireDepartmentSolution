import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import {
  CompanyPositionDetailResponse,
  CompanyResponse,
  EmployeeStatusCodeResponse,
  JobTitleResponse,
  ShiftResponse,
  StationResponse,
  WorkPeriodResponse,
} from '../interfaces';
import { AppServices } from '../services/app-services';

type LookupTab = 'stations' | 'companies' | 'company-positions' | 'shifts' |
  'job-titles' | 'work-periods' | 'employee-status-codes';

@Component({
  selector: 'app-lookups-page',
  standalone: false,
  templateUrl: './lookups-page.html',
  styleUrl: './lookups-page.css',
})
export class LookupsPage {
  activeTab: LookupTab = 'stations';
  loadedTabs: LookupTab[] = [];
  loading = false;
  errorMessage = '';
  searchText = '';

  stations: StationResponse[] = [];
  companies: CompanyResponse[] = [];
  companyPositions: CompanyPositionDetailResponse[] = [];
  shifts: ShiftResponse[] = [];
  jobTitles: JobTitleResponse[] = [];
  workPeriods: WorkPeriodResponse[] = [];
  employeeStatusCodes: EmployeeStatusCodeResponse[] = [];

  constructor(private service: AppServices, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    void this.loadCurrent();
  }

  selectTab(tab: LookupTab): void {
    this.activeTab = tab;
    this.errorMessage = '';
    this.searchText = '';
    if (!this.loadedTabs.includes(tab)) {
      void this.loadCurrent();
    }
  }

  async loadCurrent(): Promise<void> {
    this.loading = true;
    this.errorMessage = '';
    this.cdr.detectChanges();

    try {
      switch (this.activeTab) {
        case 'stations':
          this.stations = await firstValueFrom(this.service.GetAllStations());
          break;
        case 'companies':
          this.companies = await firstValueFrom(this.service.GetAllCompanies());
          break;
        case 'company-positions':
          this.companyPositions = await firstValueFrom(this.service.GetAllCompanyPositions());
          break;
        case 'shifts':
          this.shifts = await firstValueFrom(this.service.GetAllShifts());
          break;
        case 'job-titles':
          this.jobTitles = await firstValueFrom(this.service.GetAllJobTitles());
          break;
        case 'work-periods':
          this.workPeriods = await firstValueFrom(this.service.GetWorkPeriods());
          break;
        case 'employee-status-codes':
          this.employeeStatusCodes = await firstValueFrom(this.service.GetEmployeeStatusCodes());
          break;
      }
      if (!this.loadedTabs.includes(this.activeTab)) {
        this.loadedTabs.push(this.activeTab);
      }
    } catch (error) {
      const response = error as HttpErrorResponse;
      this.errorMessage = typeof response.error === 'string'
        ? response.error
        : 'The lookup could not be loaded. Please try again.';
    } finally {
      this.loading = false;
      this.cdr.detectChanges();
    }
  }

  get rowCount(): number {
    switch (this.activeTab) {
      case 'stations': return this.stations.length;
      case 'companies': return this.companies.length;
      case 'company-positions': return this.companyPositions.length;
      case 'shifts': return this.shifts.length;
      case 'job-titles': return this.jobTitles.length;
      case 'work-periods': return this.workPeriods.length;
      case 'employee-status-codes': return this.employeeStatusCodes.length;
    }
  }

  get visibleStations(): StationResponse[] {
    return this.stations.filter(item => this.matchesSearch(item.stationNbr, item.stationId, item.districtId));
  }

  get visibleCompanies(): CompanyResponse[] {
    return this.companies.filter(item => this.matchesSearch(item.companyNme, item.companyId, item.stationId));
  }

  get visibleCompanyPositions(): CompanyPositionDetailResponse[] {
    return this.companyPositions.filter(item => this.matchesSearch(
      item.companyName, item.stationNumber, item.shiftName, item.jobTitleShortDesc, item.companyPositionId
    ));
  }

  get visibleShifts(): ShiftResponse[] {
    return this.shifts.filter(item => this.matchesSearch(item.shiftCode, item.shiftNme, item.shiftId));
  }

  get visibleJobTitles(): JobTitleResponse[] {
    return this.jobTitles.filter(item => this.matchesSearch(item.jobTitleDesc, item.jobTitleShortDesc, item.jobTitleId));
  }

  get visibleWorkPeriods(): WorkPeriodResponse[] {
    return this.workPeriods.filter(item => this.matchesSearch(item.weekDay, item.workPeriodNbr));
  }

  get visibleEmployeeStatusCodes(): EmployeeStatusCodeResponse[] {
    return this.employeeStatusCodes.filter(item => this.matchesSearch(item.employeeStatusCde, item.employeeStatusDesc));
  }

  get visibleCount(): number {
    switch (this.activeTab) {
      case 'stations': return this.visibleStations.length;
      case 'companies': return this.visibleCompanies.length;
      case 'company-positions': return this.visibleCompanyPositions.length;
      case 'shifts': return this.visibleShifts.length;
      case 'job-titles': return this.visibleJobTitles.length;
      case 'work-periods': return this.visibleWorkPeriods.length;
      case 'employee-status-codes': return this.visibleEmployeeStatusCodes.length;
    }
  }

  private matchesSearch(...values: (string | number | null | undefined)[]): boolean {
    const search = this.searchText.trim().toLowerCase();
    if (!search) return true;

    for (const value of values) {
      if (String(value ?? '').toLowerCase().includes(search)) return true;
    }
    return false;
  }
}
