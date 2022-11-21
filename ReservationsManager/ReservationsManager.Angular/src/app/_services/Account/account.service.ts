import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EmployeeForRegisterDto } from 'src/app/_models/Account/EmployeeForRegisterDto';
import { UserForRegisterDto } from 'src/app/_models/Account/UserForRegisterDto';
import { environment } from 'src/environments/environment';
import { LoginResponseDto } from '../../_models/Account/LoginResponseDto';
import { UserForLoginDto } from '../../_models/Account/UserForLoginDto';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl + 'authenticate/';
  registerUrl = this.baseUrl + 'register/';

  constructor(private http: HttpClient) {}

  get accountId(): number {
    let id = localStorage.getItem('accountId');
    return id ? +id : -1;
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('accessToken');
    return token ? true : false;
  }

  haveRole(roles: Array<string>): boolean {
    let role = localStorage.getItem('accountRole');
    return role != null && roles.includes(role);
  }

  login(userForLoginDto: UserForLoginDto): Observable<LoginResponseDto> {
    return this.http.post<LoginResponseDto>(
      this.baseUrl + 'login',
      userForLoginDto
    );
  }

  logout(): void {
    localStorage.removeItem('accessToken');
  }

  registerUser(userForRegister: UserForRegisterDto): Observable<boolean> {
    return this.http.post<boolean>(`${this.registerUrl}user`, userForRegister);
  }

  registerEmployee(
    employeeForRegister: EmployeeForRegisterDto
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
