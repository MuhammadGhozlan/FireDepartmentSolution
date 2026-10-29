import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { HomePage } from './home-page/home-page';
import { DashboardPage } from './dashboard-page/dashboard-page';
import { LookupsPage } from './lookups-page/lookups-page';
import { EmployeesPage } from './employees-page/employees-page';
import { AttendancePage } from './attendance-page/attendance-page';
import { TransferPage } from './transfer-page/transfer-page';
import { PermTransPage } from './permTrans-page/permTrans-page';
import { LeaveManagementPage } from './leave-management-page/leave-management-page';
import { EmployeeAssignmentsPage } from './employee-assignments-page/employee-assignments-page';
import { ReportsPage } from './reports-page/reports-page';
import { LoginPage } from './login-page/login-page';

@NgModule({
  declarations: [
    App,
    HomePage,
    DashboardPage,
    LookupsPage,
    EmployeesPage,
    AttendancePage,
    TransferPage,
    PermTransPage,
    LeaveManagementPage,
    EmployeeAssignmentsPage,
    ReportsPage,
    LoginPage
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient()
  ],
  bootstrap: [App]
})
export class AppModule { }
