import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {
  confirmPasswordValidator,
  passwordValidator,
  UniqueUsernameValidator,
} from 'src/app/_common/CustomValidators';
import { EmployeeForRegister } from 'src/app/_models/Account/EmployeeForRegister';
import { AccountService } from 'src/app/_services/Account/account.service';

@Component({
  selector: 'app-register-employee',
  templateUrl: './register-employee.component.html',
  styleUrls: ['./register-employee.component.css'],
})
export class RegisterEmployeeComponent implements OnInit {
  registerForm = this.fb.group(
    {
      name: this.fb.control('', Validators.required),
      experience: this.fb.control('', [
        Validators.required,
        Validators.pattern(/^[1-9]+[0-9]*/),
      ]),
      description: this.fb.control('', Validators.required),
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
  descriptionLength = 300;

  get name(): AbstractControl | null {
    return this.registerForm.get('name');
  }

  get experience(): AbstractControl | null {
    return this.registerForm.get('experience');
  }

  get description(): AbstractControl | null {
    return this.registerForm.get('description');
  }

  get phoneNumber(): AbstractControl | null {
    return this.registerForm.get('phoneNumber');
  }

  get email(): AbstractControl | null {
    return this.registerForm.get('email');
  }

  get username(): AbstractControl | null {
    return this.registerForm.get('username');
  }

  get password(): AbstractControl | null {
    return this.registerForm.get('password');
  }

  get confirmPassword(): AbstractControl | null {
    return this.registerForm.get('confirmPassword');
  }

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {}

  register(): void {
    if (this.registerForm.invalid) return;

    this.error = null;
    this.registrationInProccess = true;

    let employeeForRegister = this.registerForm.value as EmployeeForRegister;
    employeeForRegister.phoneNumber = `+373${employeeForRegister.phoneNumber}`;

    this.accountService.registerEmployee(employeeForRegister).subscribe({
      next: this.onRegistrationCompleted,
      error: (error: any) => {
        this.error = error;
        this.registrationInProccess = false;
      },
      complete: () => {
        this.registrationInProccess = false;
      },
    });
  }

  private onRegistrationCompleted(status: boolean): void {
    this.error = status ? null : 'Registration failed.';

    if (status) {
      console.log('Redirecting to login page.');
      // this.router.navigate(['/login']);
    }
  }
}
