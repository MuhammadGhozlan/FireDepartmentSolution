import { TestBed } from '@angular/core/testing';
import { RouterModule } from '@angular/router';
import { DashboardPage } from './dashboard-page';

describe('DashboardPage', () => {
  it('links the Attendance card to the attendance page', async () => {
    await TestBed.configureTestingModule({
      declarations: [DashboardPage],
      imports: [RouterModule.forRoot([])],
    }).compileComponents();

    const fixture = TestBed.createComponent(DashboardPage);
    fixture.detectChanges();

    const cards = fixture.nativeElement.querySelectorAll('.feature-card') as NodeListOf<HTMLAnchorElement>;
    expect(cards[0].textContent).toContain('Attendance');
    expect(cards[0].getAttribute('href')).toBe('/attendance');
    expect(cards[1].textContent).toContain('Lookups');
    expect(cards[1].getAttribute('href')).toBe('/lookups');
    expect(cards[2].textContent).toContain('Employees');
    expect(cards[2].getAttribute('href')).toBe('/employees');
    expect(cards[3].textContent).toContain('Employee Assignments');
    expect(cards[3].getAttribute('href')).toBe('/employee-assignments');
  });
});
