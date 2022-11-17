import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { LoginResponseDto } from 'src/app/_models/Account/LoginResponseDto';
import { UserForLoginDto } from 'src/app/_models/Account/UserForLoginDto';
import { AccountService } from 'src/app/_services/Account/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });

  error: string | null = null;
  logging: boolean = false;

  get username(): AbstractControl | null {
    return this.loginForm.get('username');
  }

  get password(): AbstractControl | null {
    return this.loginForm.get('password');
  }

  constructor(private accountService: AccountService, private router: Router) {}

  ngOnInit(): void {}

  login(): void {
    if (this.loginForm.invalid || this.logging) return;

    let loginRequest = this.loginForm.value as UserForLoginDto;
    this.logging = true;

    this.accountService.login(loginRequest).subscribe({
      next: (response) => {
        this.logging = false;
        this.saveResponse(response);
        this.router.navigate([`/account/${response.role.toLowerCase()}`]);
      },
      error: (error) => {
        this.logging = false;
        this.error = 'Invalid username or/and password';
      },
    });
  }

  private saveResponse(loginResponse: LoginResponseDto) {
    localStorage.setItem('accessToken', loginResponse.accessToken);
    localStorage.setItem('userRole', loginResponse.role);
    localStorage.setItem('userId', loginResponse.id.toString());
  }
}
