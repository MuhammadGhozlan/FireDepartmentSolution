import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { HomePage } from './home-page/home-page';
import { AttendancePage } from './attendance-page/attendance-page';
import { TransferPage } from './transfer-page/transfer-page';
import { PermTransPage } from './permTrans-page/permTrans-page';
import { LeaveManagementPage } from './leave-management-page/leave-management-page';
import { StaffAssignmentManagementPage } from './staff-assignment-management-page/staff-assignment-management-page';
import { ReportsPage } from './reports-page/reports-page';
import { LoginPage } from './login-page/login-page';

@NgModule({
  declarations: [
    App,
    HomePage,
    AttendancePage,
    TransferPage,
    PermTransPage,
    LeaveManagementPage,
    StaffAssignmentManagementPage,
    ReportsPage,
    LoginPage
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
  ],
  bootstrap: [App]
})
export class AppModule { }
