import { Component } from '@angular/core';
import { AppServices } from '../services/app-services';

@Component({
  selector: 'app-leave-request-page',
  standalone: false,
  templateUrl: './leave-request-page.html',
  styleUrl: './leave-request-page.css',
})
export class LeaveRequestPage {
  constructor(public service: AppServices) { }

  claims: any[] = []

  ngOnInit() {
    this.getClaims();
  }

  getClaims() {
    this.service.GetClaims().subscribe(res => {
      this.claims = res;
    })
  }
}
