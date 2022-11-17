import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { UserForRegister } from 'src/app/_models/Account/UserForRegister';
import { AccountService } from 'src/app/_services/Account/account.service';
import {
  confirmPasswordValidator,
  passwordValidator,
  UniqueUsernameValidator,
} from 'src/app/_common/CustomValidators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css'],
})
export class RegisterUserComponent implements OnInit {
  userRegisterForm = this.fb.group(
    {
      name: this.fb.control('', Validators.required),
      phoneNumber: this.fb.control('', [
        Validators.required,
        Validators.pattern(/[0-9]{8}/),
      ]),
      email: this.fb.control('', Validators.email),
      username: this.fb.control(
        '',
        Validators.required,
        UniqueUsernameValidator.createValidator(this.accountService)
      ),
      password: this.fb.control('', [Validators.required, passwordValidator()]),
      confirmPassword: this.fb.control('', Validators.required),
    },
    { validators: confirmPasswordValidator }
  );

  registrationInProccess = false;
  error: string | null = null;

  get name(): AbstractControl | null {
    return this.userRegisterForm.get('name');
  }

  get phoneNumber(): AbstractControl | null {
    return this.userRegisterForm.get('phoneNumber');
  }

  get email(): AbstractControl | null {
    return this.userRegisterForm.get('email');
  }

  get username(): AbstractControl | null {
    return this.userRegisterForm.get('username');
  }

  get password(): AbstractControl | null {
    return this.userRegisterForm.get('password');
  }

  get confirmPassword(): AbstractControl | null {
    return this.userRegisterForm.get('confirmPassword');
  }

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {}

  register(): void {
    if (this.userRegisterForm.invalid) return;

    this.error = null;
    this.registrationInProccess = true;

    let userForRegister = this.userRegisterForm.value as UserForRegister;
    userForRegister.phoneNumber = `+373${userForRegister.phoneNumber}`;

    this.accountService.registerUser(userForRegister).subscribe({
      next: (result: boolean) => this.onRegistrationCompleted(result),
      error: (error: any) => (this.error = error),
      complete: () => (this.registrationInProccess = false),
    });
  }

  private onRegistrationCompleted(status: boolean): void {
    this.error = status ? null : 'Failed to registrate user.';

    if (status) {
      this.router.navigate(['/login']);
    }
  }
}
