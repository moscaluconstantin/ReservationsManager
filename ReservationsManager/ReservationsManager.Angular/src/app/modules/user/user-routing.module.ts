import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_guards/auth.guard';
import { AddUserReservationComponent } from './add-user-reservation/add-user-reservation.component';
import { UserReservationsListComponent } from './user-reservations-list/user-reservations-list.component';
import { UserComponent } from './user/user.component';

const userRoutes: Routes = [
  {
    path: '',
    component: UserComponent,
    canActivate: [AuthGuard],
    children: [
      { path: 'reservations', component: UserReservationsListComponent },
      { path: 'addnew', component: AddUserReservationComponent },
      { path: '', redirectTo: 'reservations', pathMatch: 'full' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(userRoutes)],
  exports: [RouterModule],
})
export class UserRoutingModule {}
