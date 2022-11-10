import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterUserComponent } from './register-user.component';
import { MaterialModule } from 'src/app/material.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [RegisterUserComponent],
  imports: [CommonModule, MaterialModule, ReactiveFormsModule],
})
export class RegisterUserModule {}
