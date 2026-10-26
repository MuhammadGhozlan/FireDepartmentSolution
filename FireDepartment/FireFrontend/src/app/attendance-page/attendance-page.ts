import { Component } from '@angular/core';

@Component({
  selector: 'app-attendance-page',
  standalone: false,
  templateUrl: './attendance-page.html',
  styleUrl: './attendance-page.css',
})
export class AttendancePage {
  status: string[] = ["Present", "Sick", "Vacation", "Injury"];
}
