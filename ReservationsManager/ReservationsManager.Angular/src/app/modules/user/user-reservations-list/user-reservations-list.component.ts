import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { finalize } from 'rxjs';
import { ReservationCanceledUpdateDto } from 'src/app/_models/Reservation/ReservationCanceledUpdateDto';
import { UserReservationDto } from 'src/app/_models/Reservation/UserReservationDto';
import { ReservationsService } from 'src/app/_services/Reservations/reservations.service';

@Component({
  selector: 'app-user-reservations-list',
  templateUrl: './user-reservations-list.component.html',
  styleUrls: ['./user-reservations-list.component.css'],
})
export class UserReservationsListComponent implements OnInit {
  @ViewChild('myTable') table: MatTable<any> | undefined;
  reservations: Array<UserReservationDto>;
  columnsToDisplay = ['employee', 'action', 'date', 'time', 'status', 'button'];

  private inProcess: boolean = false;

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

  update(reservation: UserReservationDto): void {
    if (this.inProcess) return;

    this.inProcess = true;
    let updateDto = new ReservationCanceledUpdateDto(
      reservation.id,
      !reservation.canceled
    );

    this.reservationsService
      .updateCanceledState(updateDto)
      .pipe(finalize(() => (this.inProcess = false)))
      .subscribe(() => this.removeReservation(reservation));
  }

  private removeReservation(reservation: UserReservationDto): void {
    const index = this.reservations.indexOf(reservation, 0);

    if (index >= 0) {
      this.reservations.splice(index, 1);
      this.table?.renderRows();
    }
  }
}
