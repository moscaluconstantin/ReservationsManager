import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { catchError } from 'rxjs';
import { UserForRegister } from 'src/app/_models/Account/UserForRegister';
import { AccountService } from 'src/app/_services/Account/account.service';
import {
  confirmPasswordValidator,
  passwordValidator,
  phoneNumberValidator,
  UniqueUsernameValidator,
} from 'src/CustomValidators';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css'],
})
export class RegisterUserComponent implements OnInit {
  userRegisterForm = new FormGroup(
    {
      name: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', [
        Validators.required,
        phoneNumberValidator(8),
      ]),
      email: new FormControl('', Validators.email),
      username: new FormControl(
        '',
        Validators.required,
        UniqueUsernameValidator.createValidator(this.accountService)
      ),
      password: new FormControl('', [Validators.required, passwordValidator()]),
      confirmPassword: new FormControl('', Validators.required),
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

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  register(): void {
    if (this.userRegisterForm.invalid) return;

    this.registrationInProccess = true;

    let userForRegister = this.userRegisterForm.value as UserForRegister;

    this.accountService
      .registerUser(userForRegister)
      .subscribe((x) => this.onRegistrationCompleted(x));
  }

  private onRegistrationCompleted(status: boolean): void {
    this.registrationInProccess = false;

    this.error = status ? null : 'Failed to registrate user.';

    if (status) {
      console.log('Redirecting to login page.');
    }
  }
}
