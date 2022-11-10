import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserForRegister } from 'src/app/_models/Account/UserForRegister';
import { environment } from 'src/environments/environment';
import { BearerToken } from '../../_models/Account/BearerToken';
import { UserForLogin } from '../../_models/Account/UserForLogin';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl + 'authenticate/';

  constructor(private http: HttpClient) {}

  isLoggedIn(): boolean {
    const token = localStorage.getItem('accessToken');
    return token ? true : false;
  }

  login(userForLoginDto: UserForLogin): Observable<BearerToken> {
    return this.http.post<BearerToken>(this.baseUrl + 'login', userForLoginDto);
  }

  registerUser(userForRegister: UserForRegister): Observable<boolean> {
    return this.http.post<boolean>(
      `${this.baseUrl}/register/user`,
      userForRegister
    );
  }

  chackUsernameAvailability(username: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}IsAvailable/${username}`);
  }
}
