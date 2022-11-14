import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterEmployeeComponent } from './register-employee.component';
import { MaterialModule } from 'src/app/material.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [RegisterEmployeeComponent],
  imports: [CommonModule, MaterialModule, ReactiveFormsModule],
})
export class RegisterEmployeeModule {}
