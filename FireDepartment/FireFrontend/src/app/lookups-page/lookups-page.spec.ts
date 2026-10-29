import { ChangeDetectorRef } from '@angular/core';
import { of, throwError } from 'rxjs';
import { beforeEach, describe, expect, it, vi } from 'vitest';
import { AppServices } from '../services/app-services';
import { LookupsPage } from './lookups-page';

describe('LookupsPage', () => {
  let page: LookupsPage;
  let service: {
    GetAllStations: ReturnType<typeof vi.fn>;
    GetAllCompanies: ReturnType<typeof vi.fn>;
    GetAllCompanyPositions: ReturnType<typeof vi.fn>;
    GetAllShifts: ReturnType<typeof vi.fn>;
    GetAllJobTitles: ReturnType<typeof vi.fn>;
    GetWorkPeriods: ReturnType<typeof vi.fn>;
    GetEmployeeStatusCodes: ReturnType<typeof vi.fn>;
  };

  beforeEach(() => {
    service = {
      GetAllStations: vi.fn().mockReturnValue(of([{ stationId: 1, stationNbr: '01' }])),
      GetAllCompanies: vi.fn().mockReturnValue(of([{ companyId: 2, companyNme: 'E01' }])),
      GetAllCompanyPositions: vi.fn().mockReturnValue(of([{ companyPositionId: 3 }])),
      GetAllShifts: vi.fn().mockReturnValue(of([{ shiftId: 4, shiftCode: 'A' }])),
      GetAllJobTitles: vi.fn().mockReturnValue(of([{ jobTitleId: 5 }])),
      GetWorkPeriods: vi.fn().mockReturnValue(of([{ workPeriodId: 6 }])),
      GetEmployeeStatusCodes: vi.fn().mockReturnValue(of([{ employeeStatusCde: 'A' }])),
    };
    page = new LookupsPage(
      service as unknown as AppServices,
      { detectChanges: () => {} } as ChangeDetectorRef
    );
  });

  it('loads stations on entry and each remaining lookup when selected', async () => {
    await page.loadCurrent();
    expect(page.stations).toHaveLength(1);
    expect(page.rowCount).toBe(1);

    for (const tab of [
      'companies', 'company-positions', 'shifts', 'job-titles',
      'work-periods', 'employee-status-codes',
    ] as const) {
      page.activeTab = tab;
      await page.loadCurrent();
      expect(page.rowCount).toBe(1);
    }

    expect(service.GetAllStations).toHaveBeenCalledOnce();
    expect(service.GetAllCompanies).toHaveBeenCalledOnce();
    expect(service.GetAllCompanyPositions).toHaveBeenCalledOnce();
    expect(service.GetAllShifts).toHaveBeenCalledOnce();
    expect(service.GetAllJobTitles).toHaveBeenCalledOnce();
    expect(service.GetWorkPeriods).toHaveBeenCalledOnce();
    expect(service.GetEmployeeStatusCodes).toHaveBeenCalledOnce();
  });

  it('keeps an already loaded tab without another request', async () => {
    await page.loadCurrent();
    page.selectTab('stations');
    expect(service.GetAllStations).toHaveBeenCalledOnce();
  });

  it('filters the active lookup without another API request', async () => {
    service.GetAllStations.mockReturnValue(of([
      { stationId: 1, stationNbr: '01', districtId: 2 },
      { stationId: 2, stationNbr: 'HQ', districtId: 3 },
    ]));
    await page.loadCurrent();
    page.searchText = 'hq';

    expect(page.visibleStations.map(item => item.stationNbr)).toEqual(['HQ']);
    expect(page.visibleCount).toBe(1);
    expect(service.GetAllStations).toHaveBeenCalledOnce();
  });

  it('shows an error and allows a retry', async () => {
    service.GetAllStations.mockReturnValueOnce(throwError(() => new Error('offline')));
    await page.loadCurrent();
    expect(page.errorMessage).toContain('could not be loaded');
    expect(page.loadedTabs).not.toContain('stations');

    await page.loadCurrent();
    expect(page.errorMessage).toBe('');
    expect(page.stations).toHaveLength(1);
  });
});
