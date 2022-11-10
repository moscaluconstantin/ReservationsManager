import {
  AbstractControl,
  AsyncValidatorFn,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';
import { map, Observable } from 'rxjs';
import { AccountService } from './app/_services/Account/account.service';

export const confirmPasswordValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const password = control.get('password')?.value;
  const secondPassword = control.get('confirmPassword')?.value;

  return password != secondPassword ? { confirmPassword: true } : null;
};

export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    let password: string = control.value;
    let invalid = false;

    if (password.length < 8) invalid = true;
    if (!/\d/.test(password)) invalid = true;
    if (!/[A-Z]/.test(password)) invalid = true;
    if (!/[a-z]/.test(password)) invalid = true;
    if (!/\W/.test(password)) invalid = true;

    return invalid ? { invalidPassword: true } : null;
  };
}

export function phoneNumberValidator(expectedLength: number): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const differentLength =
      !control.value || control.value.toString().length != expectedLength;

    return differentLength || isNaN(control.value)
      ? { phoneNumber: true }
      : null;
  };
}

export class UniqueUsernameValidator {
  static createValidator(accountService: AccountService): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      return accountService
        .chackUsernameAvailability(control.value)
        .pipe(
          map((isAvailable: boolean) =>
            isAvailable ? null : { uniqueUsername: true }
          )
        );
    };
  }
}