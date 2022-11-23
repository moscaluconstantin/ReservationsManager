import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/material.module';
import { UserRoutingModule } from './user-routing.module';
import { ReactiveFormsModule } from '@angular/forms';

import { UserComponent } from './user/user.component';
import { UserReservationsListComponent } from './user-reservations-list/user-reservations-list.component';
import { AddUserReservationComponent } from './add-user-reservation/add-user-reservation.component';
import { LoadingSpinnerModule } from '../loading-spinner/loading-spinner.module';

const components = [
  UserComponent,
  UserReservationsListComponent,
  AddUserReservationComponent,
];

@NgModule({
  declarations: [components],
  imports: [
    CommonModule,
    UserRoutingModule,
    MaterialModule,
    ReactiveFormsModule,
    LoadingSpinnerModule,
  ],
  exports: [components],
})
export class UserModule {}
