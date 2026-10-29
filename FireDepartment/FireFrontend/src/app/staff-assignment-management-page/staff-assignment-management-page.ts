import { Component } from '@angular/core';

@Component({
  selector: 'app-staff-assignment-management-page',
  standalone: false,
  templateUrl: './staff-assignment-management-page.html',
  styleUrl: './staff-assignment-management-page.css',
})
export class StaffAssignmentManagementPage {
  view: string = 'work';
  drop1 = false;
  drop2 = false;
  drop3 = false;
}
