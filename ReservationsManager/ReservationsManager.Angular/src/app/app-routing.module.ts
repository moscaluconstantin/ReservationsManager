import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeComponent } from './modules/employee/employee/employee.component';
import { LoginComponent } from './modules/login/login.component';
import { RegisterEmployeeComponent } from './modules/register/register-employee/register-employee.component';
import { RegisterUserComponent } from './modules/register/register-user/register-user.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register-user', component: RegisterUserComponent },
  { path: 'register-employee', component: RegisterEmployeeComponent },
  {
    path: 'account/user',
    loadChildren: () =>
      import('./modules/user/user.module').then((m) => m.UserModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'account/employee',
    component: EmployeeComponent,
    canActivate: [AuthGuard],
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
