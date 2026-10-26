import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveManagementPage } from './leave-management-page';

describe('LeaveManagementPage', () => {
  let component: LeaveManagementPage;
  let fixture: ComponentFixture<LeaveManagementPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LeaveManagementPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaveManagementPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
