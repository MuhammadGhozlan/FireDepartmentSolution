import { ChangeDetectorRef } from '@angular/core';
import { of, throwError } from 'rxjs';
import { beforeEach, describe, expect, it, vi } from 'vitest';
import { EmployeeAssignmentResponse } from '../interfaces';
import { AppServices } from '../services/app-services';
import { EmployeeAssignmentsPage } from './employee-assignments-page';

const assignment: EmployeeAssignmentResponse = {
  employeeAssignmentId: 4406,
  roic: '100512',
  assignmentStartDate: '2026-07-01',
  assignmentEndDate: null,
  isTemp: false,
  companyPositionId: 12,
  workPeriodId: 3,
  companyName: 'E24',
  stationNbr: '24',
};

describe('EmployeeAssignmentsPage', () => {
  let page: EmployeeAssignmentsPage;
  let service: {
    GetEmployeeAssignments: ReturnType<typeof vi.fn>;
    GetEmployeeAssignmentsByRoic: ReturnType<typeof vi.fn>;
    GetEmployeeAssignmentById: ReturnType<typeof vi.fn>;
    CreateEmployeeAssignment: ReturnType<typeof vi.fn>;
    UpdateEmployeeAssignment: ReturnType<typeof vi.fn>;
    DeleteEmployeeAssignment: ReturnType<typeof vi.fn>;
    GetAllCompanyPositions: ReturnType<typeof vi.fn>;
    GetWorkPeriods: ReturnType<typeof vi.fn>;
    GetAllShifts: ReturnType<typeof vi.fn>;
    GetEmployees: ReturnType<typeof vi.fn>;
  };

  beforeEach(() => {
    service = {
      GetEmployeeAssignments: vi.fn().mockReturnValue(of([assignment])),
      GetEmployeeAssignmentsByRoic: vi.fn().mockReturnValue(of([assignment])),
      GetEmployeeAssignmentById: vi.fn().mockReturnValue(of(assignment)),
      CreateEmployeeAssignment: vi.fn().mockReturnValue(of(assignment)),
      UpdateEmployeeAssignment: vi.fn().mockReturnValue(of(assignment)),
      DeleteEmployeeAssignment: vi.fn().mockReturnValue(of(void 0)),
      GetAllCompanyPositions: vi.fn().mockReturnValue(of([{
        companyPositionId: 12, stationNumber: '24', companyName: 'E24',
        jobTitleShortDesc: 'FF', shiftName: 'Fire Shift', shiftId: 1, truckPosition: 1,
      }])),
      GetWorkPeriods: vi.fn().mockReturnValue(of([{ workPeriodId: 3, workPeriodNbr: 9, weekDay: 'Mon' }])),
      GetAllShifts: vi.fn().mockReturnValue(of([{ shiftId: 1, shiftCode: 'A' }])),
      GetEmployees: vi.fn().mockReturnValue(of([{
        roic: '100512', firstNme: 'Chris', middleInitialNme: null,
        lastNme: 'Payne', suffix: null,
      }])),
    };
    page = new EmployeeAssignmentsPage(
      service as unknown as AppServices,
      { detectChanges: () => {} } as ChangeDetectorRef
    );
  });

  it('loads assignments, names, and lookup labels', async () => {
    await page.loadAssignments();
    await page.loadLookups();

    expect(page.assignments).toHaveLength(1);
    expect(page.employeeName('100512')).toBe('Chris Payne');
    expect(page.positionName(12)).toContain('E24');
    expect(page.positionName(12)).toContain('Shift A');
    expect(page.positionSummary(12)).toBe('FF / A / #1');
    expect(page.workPeriodName(3)).toBe('9');
  });

  it('finds assignments by exact ROIC and filters by type and active date', async () => {
    page.roicQuery = '100512';
    await page.findByRoic();
    expect(service.GetEmployeeAssignmentsByRoic).toHaveBeenCalledWith('100512');
    expect(page.visibleAssignments).toHaveLength(1);

    page.typeFilter = 'temporary';
    expect(page.visibleAssignments).toHaveLength(0);
    page.typeFilter = '';
    page.activeDate = '2026-06-30';
    expect(page.visibleAssignments).toHaveLength(0);
  });

  it('loads one assignment before editing', async () => {
    await page.startEdit(assignment);
    expect(service.GetEmployeeAssignmentById).toHaveBeenCalledWith('100512', 4406);
    expect(page.view).toBe('edit');
    expect(page.form.companyPositionId).toBe(12);
  });

  it('creates an assignment with the backend request fields', async () => {
    page.startCreate();
    page.form = {
      roic: '100512', assignmentStartDate: '2026-07-01', assignmentEndDate: '',
      isTemp: true, companyPositionId: 12, workPeriodId: 3,
    };
    await page.saveAssignment();

    expect(service.CreateEmployeeAssignment).toHaveBeenCalledWith({
      roic: '100512', assignmentStartDate: '2026-07-01', assignmentEndDate: null,
      isTemp: true, companyPositionId: 12, workPeriodId: 3,
    });
    expect(page.view).toBe('list');
    expect(page.notice).toBe('Assignment saved.');
  });

  it('blocks an end date before the start date', async () => {
    page.startCreate();
    page.form = {
      roic: '100512', assignmentStartDate: '2026-07-01', assignmentEndDate: '2026-06-30',
      isTemp: false, companyPositionId: 12, workPeriodId: 3,
    };
    await page.saveAssignment();
    expect(service.CreateEmployeeAssignment).not.toHaveBeenCalled();
    expect(page.errorMessage).toContain('End date');
  });

  it('updates an assignment using its ID and unchanged ROIC', async () => {
    await page.startEdit(assignment);
    page.form.isTemp = true;
    await page.saveAssignment();
    expect(service.UpdateEmployeeAssignment).toHaveBeenCalledWith(4406,
      expect.objectContaining({ roic: '100512', isTemp: true }));
  });

  it('keeps the edit form open on a failed update', async () => {
    await page.startEdit(assignment);
    service.UpdateEmployeeAssignment.mockReturnValueOnce(throwError(() => new Error('Update failed')));
    await page.saveAssignment();
    expect(page.view).toBe('edit');
    expect(page.errorMessage).toBe('Update failed');
  });

  it('deletes only after confirmation', async () => {
    const confirm = vi.spyOn(window, 'confirm').mockReturnValueOnce(false).mockReturnValueOnce(true);
    await page.deleteAssignment(assignment);
    expect(service.DeleteEmployeeAssignment).not.toHaveBeenCalled();
    await page.deleteAssignment(assignment);
    expect(service.DeleteEmployeeAssignment).toHaveBeenCalledWith('100512', 4406);
    confirm.mockRestore();
  });
});
