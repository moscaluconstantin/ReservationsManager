import { Component, OnInit } from '@angular/core';
import { EmployeeReservationDto } from 'src/app/_models/Reservation/EmployeeReservationDto';
import { ReservationsService } from 'src/app/_services/Reservations/reservations.service';

@Component({
  selector: 'app-employee-reservations-list',
  templateUrl: './employee-reservations-list.component.html',
  styleUrls: ['./employee-reservations-list.component.css'],
})
export class EmployeeReservationsListComponent implements OnInit {
  reservations: Array<EmployeeReservationDto>;
  columnsToDisplay = ['user', 'action', 'date', 'time', 'canceled'];

  constructor(private reservationsService: ReservationsService) {
    this.reservations = new Array<EmployeeReservationDto>();
  }

  ngOnInit(): void {
    this.getReservations();
  }

  getReservations(): void {
    this.reservationsService
      .getEmployeeReservations()
      .subscribe((reservations) => (this.reservations = reservations));
  }
}
