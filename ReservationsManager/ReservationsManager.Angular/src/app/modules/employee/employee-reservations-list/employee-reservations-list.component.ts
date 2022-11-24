import { Component, OnInit } from '@angular/core';
import { EmployeeReservationDto } from 'src/app/_models/Reservation/EmployeeReservationDto';
import { ReservationCanceledUpdateDto } from 'src/app/_models/Reservation/ReservationCanceledUpdateDto';
import { ReservationsService } from 'src/app/_services/Reservations/reservations.service';

@Component({
  selector: 'app-employee-reservations-list',
  templateUrl: './employee-reservations-list.component.html',
  styleUrls: ['./employee-reservations-list.component.css'],
})
export class EmployeeReservationsListComponent implements OnInit {
  reservations: Array<EmployeeReservationDto>;
  columnsToDisplay = ['user', 'action', 'date', 'time', 'canceled', 'button'];

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

  update(reservation: EmployeeReservationDto): void {
    reservation.canceled = !reservation.canceled;
    let updateDto = new ReservationCanceledUpdateDto(
      reservation.id,
      reservation.canceled
    );

    this.reservationsService.updateCanceledState(updateDto).subscribe();
  }
}
