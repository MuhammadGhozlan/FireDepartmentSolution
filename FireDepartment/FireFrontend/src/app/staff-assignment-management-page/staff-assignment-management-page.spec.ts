import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffAssignmentManagementPage } from './staff-assignment-management-page';

describe('StaffAssignmentManagementPage', () => {
  let component: StaffAssignmentManagementPage;
  let fixture: ComponentFixture<StaffAssignmentManagementPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StaffAssignmentManagementPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StaffAssignmentManagementPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
