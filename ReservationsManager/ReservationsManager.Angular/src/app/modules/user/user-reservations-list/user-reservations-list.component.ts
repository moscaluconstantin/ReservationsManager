import { Component, OnInit } from '@angular/core';
import { UserReservationDto } from 'src/app/_models/Reservation/UserReservationDto';
import { ReservationsService } from 'src/app/_services/Reservations/reservations.service';

@Component({
  selector: 'app-user-reservations-list',
  templateUrl: './user-reservations-list.component.html',
  styleUrls: ['./user-reservations-list.component.css'],
})
export class UserReservationsListComponent implements OnInit {
  reservations: Array<UserReservationDto>;
  columnsToDisplay = ['employee', 'action', 'date', 'time'];

  constructor(private reservationsService: ReservationsService) {
    this.reservations = new Array<UserReservationDto>();
  }

  ngOnInit(): void {
    this.getUserReservations();
  }

  getUserReservations(): void {
    this.reservationsService
      .getUserReservations()
      .subscribe((reservations) => (this.reservations = reservations));
  }
}
