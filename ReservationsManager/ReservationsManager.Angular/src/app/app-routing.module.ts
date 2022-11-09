import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './modules/login/login.component';
import { RegisterEmployeeComponent } from './modules/register/register-employee/register-employee.component';
import { RegisterUserComponent } from './modules/register/register-user/register-user.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register-user', component: RegisterUserComponent },
  { path: 'register-employee', component: RegisterEmployeeComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
