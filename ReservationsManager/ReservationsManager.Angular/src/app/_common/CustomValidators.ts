import {
  AbstractControl,
  AsyncValidatorFn,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';
import { catchError, map, Observable } from 'rxjs';
import { handleError } from './ObservableErrorHandlers';
import { AccountService } from '../_services/Account/account.service';

export const confirmPasswordValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const confirmPasswordControl = control.get('confirmPassword');

  const password: string = control.get('password')?.value;
  const secondPassword: string = confirmPasswordControl?.value;

  const identicalPasswords = password.length > 0 && password == secondPassword;

  if (!confirmPasswordControl?.errors?.['required'] && !identicalPasswords) {
    confirmPasswordControl?.setErrors({ confirmPassword: true });
  }

  return identicalPasswords ? null : { confirmPassword: true };
};

export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    let invalid = false;
    let password: string = control.value;

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
      return accountService.chackUsernameAvailability(control.value).pipe(
        map((isAvailable: boolean) =>
          isAvailable ? null : { uniqueUsername: true }
        ),
        catchError(
          handleError<ValidationErrors>('Calling server', {
            lostConnection: true,
          })
        )
      );
    };
  }
}
