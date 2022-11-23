import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AvailableTimeBlocksRequestDto } from 'src/app/_models/Reservation/AvailableTimeBlocksRequestDto';
import { ReservationRequest } from 'src/app/_models/Reservation/ReservationRequest';
import { ReservationToAddDto } from 'src/app/_models/Reservation/ReservationToAddDto';
import { UserReservationDto } from 'src/app/_models/Reservation/UserReservationDto';
import { TimeBlockDto } from 'src/app/_models/TimeBlock/TimeBlockDto';
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

  getAvailableTimeBlocks(
    requestDto: AvailableTimeBlocksRequestDto
  ): Observable<Array<TimeBlockDto>> {
    let requestParams = new HttpParams()
      .set('ActionEmployeeId', requestDto.actionEmployeeId)
      .set('Date', requestDto.date);

    return this.http.get<Array<TimeBlockDto>>(
      `${this.baseUrl}AvailableTimeBlocks`,
      {
        params: requestParams,
      }
    );
  }

  addReservation(request: ReservationRequest): Observable<any> {
    let reservationToAddDto = ReservationToAddDto.Create(
      this.accountService.accountId,
      request
    );

    return this.http.post<any>(this.baseUrl, reservationToAddDto);
  }
}
