import { Component } from '@angular/core';

@Component({
  selector: 'app-leave-management-page',
  standalone: false,
  templateUrl: './leave-management-page.html',
  styleUrl: './leave-management-page.css',
})
export class LeaveManagementPage {
activeTab: string = 'requests';
 
}
