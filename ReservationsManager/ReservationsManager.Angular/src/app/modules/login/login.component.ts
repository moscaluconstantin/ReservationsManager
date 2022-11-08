import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserForLogin } from 'src/app/_models/Account/UserForLogin';
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

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  login(): void {
    if (this.loginForm.invalid || this.logging) return;

    let loginRequest = this.loginForm.value as UserForLogin;

    console.log(
      `Try login with: ${loginRequest.username} - ${loginRequest.password}`
    );

    if (this.logging) return;

    this.logging = true;

    this.accountService.login(loginRequest).subscribe((token) => {
      localStorage.setItem('accessToken', token.accessToken);
      this.logging = false;
      console.log(token.accessToken);
    });
  }
}