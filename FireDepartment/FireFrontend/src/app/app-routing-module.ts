import { NgModule } from '@angular/core';
import { RouterModule, Routes, UrlSerializer } from '@angular/router';
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

const routes: Routes = [
  { path: 'home', component: HomePage },
  { path: '', component: HomePage },
  { path: 'dashboard', component: DashboardPage },
  { path: 'lookups', component: LookupsPage },
  { path: 'employees', component: EmployeesPage },
  { path: 'attendance', component: AttendancePage },
  { path: 'transfer', component: TransferPage },
  { path: 'permTransfer', component: PermTransPage },
  { path: 'leaveManagement', component: LeaveManagementPage },
  { path: 'employee-assignments', component: EmployeeAssignmentsPage },
  { path: 'assignment', redirectTo: 'employee-assignments', pathMatch: 'full' },
  { path: 'reports', component: ReportsPage },
  { path: 'login', component: LoginPage }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
