import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserReservationDto } from 'src/app/_models/Reservation/UserReservationDto';
import { environment } from 'src/environments/environment';
import { AccountService } from '../Account/account.service';

@Injectable({
  providedIn: 'root',
})
export class ReservationsService {
  baseUrl = environment.apiUrl + 'Reservations/';

  constructor(
    private http: HttpClient,
    private accountService: AccountService
  ) {}

  getUserReservations(): Observable<Array<UserReservationDto>> {
    return this.http.get<Array<UserReservationDto>>(
      `${this.baseUrl}ForUser/${this.accountService.accountId}`
    );
  }
}
