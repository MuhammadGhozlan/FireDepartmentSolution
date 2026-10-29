import { ChangeDetectorRef } from '@angular/core';
import { of } from 'rxjs';
import { beforeEach, describe, expect, it, vi } from 'vitest';
import { AttendanceResponse, DailyRosterResponse } from '../interfaces';
import { AppServices } from '../services/app-services';
import { AttendancePage } from './attendance-page';

const existing: AttendanceResponse = {
  attendanceId: 41,
  attendanceDate: '2026-08-27',
  roic: '100512',
  employeeAssignmentId: 4406,
  attendanceStatusId: 1,
  attendanceComments: null,
};

const recorded: DailyRosterResponse = {
  employeeAssignmentId: 4406,
  roic: '100512',
  workPeriodNbr: 1,
  attendance: existing,
};

const unrecorded: DailyRosterResponse = {
  employeeAssignmentId: 4407,
  roic: '111708',
  workPeriodNbr: 2,
  attendance: null,
};

describe('AttendancePage', () => {
  let component: AttendancePage;
  let service: {
    GetAttendanceStatuses: ReturnType<typeof vi.fn>;
    GetWorkPeriods: ReturnType<typeof vi.fn>;
    GetAttendance: ReturnType<typeof vi.fn>;
    GetEmployees: ReturnType<typeof vi.fn>;
    GetDailyRoster: ReturnType<typeof vi.fn>;
    FilterAttendance: ReturnType<typeof vi.fn>;
    TakeAttendance: ReturnType<typeof vi.fn>;
    UpdateAttendance: ReturnType<typeof vi.fn>;
    DeleteAttendance: ReturnType<typeof vi.fn>;
  };

  beforeEach(() => {
    service = {
      GetAttendanceStatuses: vi.fn().mockReturnValue(of([
        { attendanceStatusId: 1, attendanceStatusCde: 'P', attendanceStatusDesc: 'Present' },
        { attendanceStatusId: 2, attendanceStatusCde: 'A', attendanceStatusDesc: 'Absent' },
      ])),
      GetWorkPeriods: vi.fn().mockReturnValue(of([
        { workPeriodId: 1, weekDay: 'Thursday', workPeriodNbr: 1 },
        { workPeriodId: 2, weekDay: 'Thursday', workPeriodNbr: 2 },
      ])),
      GetAttendance: vi.fn().mockReturnValue(of([])),
      GetEmployees: vi.fn().mockReturnValue(of([
        { roic: '100512', firstNme: 'Chris', middleInitialNme: 'A', lastNme: 'Payne', suffix: null },
        { roic: '111708', firstNme: 'Jordan', middleInitialNme: null, lastNme: 'Blount', suffix: null },
      ])),
      GetDailyRoster: vi.fn().mockImplementation((_date: string, period: number) =>
        of(period === 1 ? [recorded] : [unrecorded])),
      FilterAttendance: vi.fn().mockReturnValue(of([])),
      TakeAttendance: vi.fn().mockReturnValue(of([])),
      UpdateAttendance: vi.fn().mockReturnValue(of(existing)),
      DeleteAttendance: vi.fn().mockReturnValue(of(existing)),
    };
    component = new AttendancePage(
      service as unknown as AppServices,
      { detectChanges: () => {} } as ChangeDetectorRef
    );
    component.getAttendanceStatuses();
    component.getWorkPeriods();
    component.getEmployees();
  });

  it('defaults to All Statuses and restores it on reset', async () => {
    expect(component.filterRequest.attendanceStatusId).toBeNull();
    component.filterRequest.attendanceStatusId = 2;

    await component.resetFilters();

    expect(component.filterRequest.attendanceStatusId).toBeNull();
  });

  it('loads every work period when All Work Periods is selected', async () => {
    component.filterRequest.attendanceDate = '2026-08-27';
    await component.onSearch();

    expect(service.GetDailyRoster).toHaveBeenCalledWith('2026-08-27', 1);
    expect(service.GetDailyRoster).toHaveBeenCalledWith('2026-08-27', 2);
    expect(component.rows).toHaveLength(2);
    expect(component.rows.map(row => row.key)).toEqual([
      'attendance-41',
      'assignment-4407-2026-08-27',
    ]);
  });

  it('applies ROIC and status filters to a dated roster', async () => {
    component.filterRequest = {
      attendanceDate: '2026-08-27',
      roic: '100512',
      attendanceStatusId: 1,
    };
    await component.onSearch();

    expect(component.rows.map(row => row.roic)).toEqual(['100512']);
  });

  it('shows employee names and searches a dated roster by partial name', async () => {
    component.filterRequest.attendanceDate = '2026-08-27';
    component.nameSearch = 'pay';
    await component.onSearch();

    expect(component.employeeNames['100512']).toBe('Chris A Payne');
    expect(component.rows.map(row => row.roic)).toEqual(['100512']);
  });

  it('creates only selected new rows and updates edited existing rows', async () => {
    component.filterRequest.attendanceDate = '2026-08-27';
    await component.onSearch();
    component.rows[0].attendanceStatusId = 2;
    component.rows[1].attendanceStatusId = 1;
    await component.saveAll();

    expect(service.TakeAttendance).toHaveBeenCalledWith([{
      attendanceDate: '2026-08-27',
      roic: '111708',
      employeeAssignmentId: 4407,
      attendanceStatusId: 1,
      attendanceComments: '',
    }]);
    expect(service.UpdateAttendance).toHaveBeenCalledWith(41, {
      attendanceStatusId: 2,
      attendanceComments: null,
    });
    expect(component.notice).toBe('Attendance changes saved.');
  });

  it('does not create unselected roster rows', async () => {
    component.filterRequest.attendanceDate = '2026-08-27';
    await component.onSearch();
    await component.saveAll();

    expect(service.TakeAttendance).not.toHaveBeenCalled();
    expect(service.UpdateAttendance).not.toHaveBeenCalled();
  });

  it('saves a selected new row without creating other roster rows', async () => {
    component.filterRequest.attendanceDate = '2026-08-27';
    await component.onSearch();
    component.rows[1].attendanceStatusId = 2;

    await component.saveRow(component.rows[1]);

    expect(service.TakeAttendance).toHaveBeenCalledWith([{
      attendanceDate: '2026-08-27',
      roic: '111708',
      employeeAssignmentId: 4407,
      attendanceStatusId: 2,
      attendanceComments: '',
    }]);
    expect(service.UpdateAttendance).not.toHaveBeenCalled();
  });

  it('deletes only a saved attendance row after confirmation', async () => {
    const confirm = vi.spyOn(window, 'confirm').mockReturnValue(true);
    component.filterRequest.attendanceDate = '2026-08-27';
    await component.onSearch();

    await component.deleteRow(component.rows[0]);

    expect(service.DeleteAttendance).toHaveBeenCalledWith(41);
    expect(component.notice).toBe('Attendance deleted.');
    confirm.mockRestore();
  });

  it('passes status and ROIC to the filter endpoint when no date is selected', async () => {
    component.filterRequest = { roic: '100512', attendanceStatusId: 2 };
    await component.onSearch();

    expect(service.FilterAttendance).toHaveBeenCalledWith({
      roic: '100512',
      attendanceStatusId: 2,
    });
  });

  it('searches existing attendance by name without a date', async () => {
    service.GetAttendance.mockReturnValue(of([
      existing,
      { ...existing, attendanceId: 42, roic: '111708', employeeAssignmentId: 4407 },
    ]));
    component.nameSearch = 'blount';
    await component.onSearch();

    expect(service.GetAttendance).toHaveBeenCalled();
    expect(component.rows.map(row => row.roic)).toEqual(['111708']);
  });
});
