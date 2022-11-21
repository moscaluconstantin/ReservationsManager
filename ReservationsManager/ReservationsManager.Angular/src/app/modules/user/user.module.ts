import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from 'src/app/material.module';
import { UserRoutingModule } from './user-routing.module';

import { UserComponent } from './user/user.component';
import { UserReservationsListComponent } from './user-reservations-list/user-reservations-list.component';
import { AddUserReservationComponent } from './add-user-reservation/add-user-reservation.component';

const components = [
  UserComponent,
  UserReservationsListComponent,
  AddUserReservationComponent,
];

@NgModule({
  declarations: [components, AddUserReservationComponent],
  imports: [CommonModule, UserRoutingModule, MaterialModule],
  exports: [components],
})
export class UserModule {}
