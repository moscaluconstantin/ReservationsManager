import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserForGreetDto } from 'src/app/_models/UserForGreetDto';
import { environment } from 'src/environments/environment';
import { AccountService } from '../Account/account.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseUrl = environment.apiUrl + 'Users/';

  constructor(
    private http: HttpClient,
    private accountService: AccountService
  ) {}

  getUserForGreet(): Observable<UserForGreetDto> {
    return this.http.get<UserForGreetDto>(
      `${this.baseUrl}UserForGreet/${this.accountService.accountId}`
    );
  }
}
