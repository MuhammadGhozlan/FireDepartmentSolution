import { NgModule } from '@angular/core';
import { RouterModule, Routes, UrlSerializer } from '@angular/router';
import { HomePage } from './home-page/home-page';
import { AttendancePage } from './attendance-page/attendance-page';
import { TransferPage } from './transfer-page/transfer-page';
import { PermTransPage } from './permTrans-page/permTrans-page';
import { LeaveManagementPage } from './leave-management-page/leave-management-page';
import { StaffAssignmentManagementPage } from './staff-assignment-management-page/staff-assignment-management-page';
import { ReportsPage } from './reports-page/reports-page';
import { LoginPage } from './login-page/login-page';

const routes: Routes = [
  { path: 'home', component: HomePage },
  { path: '', component: HomePage },
  { path: 'attendance', component: AttendancePage },
  { path: 'transfer', component: TransferPage },
  { path: 'permTransfer', component: PermTransPage },
  { path: 'leaveManagement', component: LeaveManagementPage },
  { path: 'assignment', component: StaffAssignmentManagementPage },
  { path: 'reports', component: ReportsPage },
  { path: 'login', component: LoginPage }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
