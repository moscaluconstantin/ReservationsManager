import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AvailableTimeBlocksRequestDto } from 'src/app/_models/AvailableTimeBlocksRequestDto';
import { UserReservationDto } from 'src/app/_models/Reservation/UserReservationDto';
import { TimeBlockDto } from 'src/app/_models/TimeBlockDto';
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
      .set('ActionId', requestDto.actionId)
      .set('EmployeeId', requestDto.employeeId)
      .set('Date', requestDto.date.toDateString());

    return this.http.get<Array<TimeBlockDto>>(
      `${this.baseUrl}AvailableTimeBlocks`,
      {
        params: requestParams,
      }
    );
  }
}
