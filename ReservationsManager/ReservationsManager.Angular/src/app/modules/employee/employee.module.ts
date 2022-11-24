import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeComponent } from './employee/employee.component';
import { MaterialModule } from 'src/app/material.module';
import { LoadingSpinnerModule } from '../loading-spinner/loading-spinner.module';
import { EmployeeReservationsListComponent } from './employee-reservations-list/employee-reservations-list.component';

@NgModule({
  declarations: [EmployeeComponent, EmployeeReservationsListComponent],
  imports: [CommonModule, MaterialModule, LoadingSpinnerModule],
  exports: [EmployeeComponent],
})
export class EmployeeModule {}
