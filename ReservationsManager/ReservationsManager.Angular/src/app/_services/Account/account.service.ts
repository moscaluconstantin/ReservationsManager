import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeForRegister } from 'src/app/_models/Account/EmployeeForRegister';
import { UserForRegister } from 'src/app/_models/Account/UserForRegister';
import { environment } from 'src/environments/environment';
import { LoginResponse } from '../../_models/Account/LoginResponse';
import { UserForLogin } from '../../_models/Account/UserForLogin';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl + 'authenticate/';
  registerUrl = this.baseUrl + 'register/';

  constructor(private http: HttpClient) {}

  isLoggedIn(): boolean {
    const token = localStorage.getItem('accessToken');
    return token ? true : false;
  }

  login(userForLoginDto: UserForLogin): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      this.baseUrl + 'login',
      userForLoginDto
    );
  }

  registerUser(userForRegister: UserForRegister): Observable<boolean> {
    return this.http.post<boolean>(`${this.registerUrl}user`, userForRegister);
  }

  registerEmployee(
    employeeForRegister: EmployeeForRegister
  ): Observable<boolean> {
    return this.http.post<boolean>(
      `${this.registerUrl}employee`,
      employeeForRegister
    );
  }

  chackUsernameAvailability(username: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}IsAvailable/${username}`);
  }
}
