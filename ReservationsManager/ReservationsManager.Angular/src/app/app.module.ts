import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { LoginModule } from './modules/login/login.module';
import { RegisterUserModule } from './modules/register/register-user/register-user.module';
import { RegisterEmployeeModule } from './modules/register/register-employee/register-employee.module';
import { UserModule } from './modules/user/user.module';
import { AuthGuard } from './_guards/auth.guard';
import { AuthInterceptor } from './_interceptors/auth/auth.interceptor';
import { DatePipe } from '@angular/common';
import { LoadingInterceptor } from './_interceptors/loading/loading.interceptor';
import { EmployeeModule } from './modules/employee/employee.module';

const modules = [
  BrowserModule,
  HttpClientModule,
  AppRoutingModule,
  BrowserAnimationsModule,
  LoginModule,
  RegisterUserModule,
  RegisterEmployeeModule,
  UserModule,
  EmployeeModule,
];

@NgModule({
  declarations: [AppComponent],
  imports: [modules],
  providers: [
    AuthGuard,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    DatePipe,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
