import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BearerToken } from '../../_models/Account/BearerToken';
import { UserForLogin } from '../../_models/Account/UserForLogin';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  isLoggedIn(): boolean {
    const token = localStorage.getItem('accessToken');
    return token ? true : false;
  }

  login(userForLoginDto: UserForLogin): Observable<BearerToken> {
    return this.http.post<BearerToken>(
      this.baseUrl + 'login/',
      userForLoginDto
    );
  }
}
