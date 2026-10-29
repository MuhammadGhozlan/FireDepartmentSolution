import { ChangeDetectorRef } from '@angular/core';
import { of, throwError } from 'rxjs';
import { beforeEach, describe, expect, it, vi } from 'vitest';
import { EmployeeDetailResponse } from '../interfaces';
import { AppServices } from '../services/app-services';
import { EmployeesPage } from './employees-page';

const employee: EmployeeDetailResponse = {
  roic: '012345',
  employeeNbr: 12345,
  firstNme: 'Alex',
  middleInitialNme: null,
  lastNme: 'Rivera',
  suffix: null,
  paramedic: true,
  hazmat: false,
  employeeStatusCde: 'A',
  birthDate: null,
  maritalStatusCde: null,
  createdDate: '2026-01-01T12:00:00Z',
  createdBy: 'System',
  lastUpdateDate: '2026-01-01T12:00:00Z',
  lastUpdateBy: 'System',
};

describe('EmployeesPage', () => {
  let page: EmployeesPage;
  let service: {
    GetEmployees: ReturnType<typeof vi.fn>;
    GetEmployeeByRoic: ReturnType<typeof vi.fn>;
    GetEmployeeStatusCodes: ReturnType<typeof vi.fn>;
    CreateEmployee: ReturnType<typeof vi.fn>;
    UpdateEmployee: ReturnType<typeof vi.fn>;
  };

  beforeEach(() => {
    service = {
      GetEmployees: vi.fn().mockReturnValue(of([employee])),
      GetEmployeeByRoic: vi.fn().mockReturnValue(of(employee)),
      GetEmployeeStatusCodes: vi.fn().mockReturnValue(of([
        { employeeStatusCde: 'A', employeeStatusDesc: 'Active' },
      ])),
      CreateEmployee: vi.fn().mockReturnValue(of(employee)),
      UpdateEmployee: vi.fn().mockReturnValue(of(employee)),
    };
    page = new EmployeesPage(
      service as unknown as AppServices,
      { detectChanges: () => {} } as ChangeDetectorRef
    );
  });

  it('loads employees and filters by name, ROIC, or status', async () => {
    await page.loadEmployees();
    await page.loadStatusCodes();
    expect(page.visibleEmployees).toHaveLength(1);
    expect(page.statusName('A')).toBe('Active');

    page.searchText = 'riv';
    expect(page.visibleEmployees).toHaveLength(1);
    page.searchText = '012345';
    expect(page.visibleEmployees).toHaveLength(1);
    page.statusFilter = 'I';
    expect(page.visibleEmployees).toHaveLength(0);
  });

  it('loads one employee by ROIC before editing', async () => {
    await page.startEdit('012345');

    expect(service.GetEmployeeByRoic).toHaveBeenCalledWith('012345');
    expect(page.view).toBe('edit');
    expect(page.form.lastNme).toBe('Rivera');
  });

  it('creates an employee using the create endpoint', async () => {
    page.startCreate();
    page.form.roic = '012345';
    page.form.employeeNbr = 12345;
    page.form.firstNme = 'Alex';
    page.form.lastNme = 'Rivera';
    page.form.employeeStatusCde = 'A';

    await page.saveEmployee();

    expect(service.CreateEmployee).toHaveBeenCalledWith(expect.objectContaining({
      roic: '012345', employeeNbr: 12345, firstNme: 'Alex', lastNme: 'Rivera',
      employeeStatusCde: 'A', socialSecurityNbr: null, createdBy: 'System',
    }));
    expect(page.view).toBe('list');
    expect(page.notice).toBe('Employee saved.');
  });

  it('updates editable fields but not ROIC or employee number', async () => {
    await page.startEdit('012345');
    page.form.lastNme = 'Reyes';
    await page.saveEmployee();

    expect(service.UpdateEmployee).toHaveBeenCalledWith('012345', expect.objectContaining({
      lastNme: 'Reyes', employeeStatusCde: 'A',
    }));
    const request = service.UpdateEmployee.mock.calls[0][1];
    expect(request).not.toHaveProperty('roic');
    expect(request).not.toHaveProperty('employeeNbr');
    expect(request).not.toHaveProperty('socialSecurityNbr');
  });

  it('keeps the form open when an update fails', async () => {
    await page.startEdit('012345');
    service.UpdateEmployee.mockReturnValueOnce(throwError(() => new Error('Update failed')));
    await page.saveEmployee();

    expect(page.view).toBe('edit');
    expect(page.errorMessage).toBe('Update failed');
  });
});
