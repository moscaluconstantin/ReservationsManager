import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-user-reservation',
  templateUrl: './add-user-reservation.component.html',
  styleUrls: ['./add-user-reservation.component.css'],
})
export class AddUserReservationComponent implements OnInit {
  reservationForm = this.fb.group({
    actionId: this.fb.control('', Validators.required),
    employee: this.fb.control('', Validators.required),
    date: this.fb.control('', Validators.required),
    timeBlockId: this.fb.control('', Validators.required),
  });

  get actionId(): AbstractControl | null {
    return this.reservationForm.controls['actionId'];
  }

  get employee(): AbstractControl | null {
    return this.reservationForm.controls['employee'];
  }

  get date(): AbstractControl | null {
    return this.reservationForm.controls['date'];
  }

  get timeBlockId(): AbstractControl | null {
    return this.reservationForm.controls['timeBlockId'];
  }

  selectedEmployee: number | null = null;
  selectedTime: number | null = null;
  selectedAction: number | null = null;
  minDate: Date = new Date();

  employees: EmployeeOption[] = [
    { id: 0, name: 'Name 1' },
    { id: 1, name: 'Name 2' },
    { id: 2, name: 'Name 3' },
    { id: 3, name: 'Name 4' },
  ];
  timeBlocks: TimeBlockOption[] = [
    { id: 0, startTime: '12:30' },
    { id: 1, startTime: '13:00' },
    { id: 2, startTime: '16:00' },
    { id: 3, startTime: '18:00' },
  ];

  actions: ActionOption[] = [
    { id: 0, name: 'Hair Wash' },
    { id: 1, name: 'Hair Cut' },
    { id: 2, name: 'Regular Manicure' },
    { id: 3, name: 'Hard Gel' },
  ];

  private controls: Array<AbstractControl | null> = [
    this.employee,
    this.date,
    this.timeBlockId,
  ];

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    //get actions
  }

  addReservation(): void {
    console.log(this.reservationForm.value);
  }

  onActionChanged(): void {
    this.resetControls(0);
    //get employees
  }

  onEmployeeChanged(): void {
    this.resetControls(1);
  }

  onDatePickerClosed(): void {
    if (this.date?.invalid) return;

    this.resetControls(2);
    //get time blocks
  }

  private resetControls(beginWith: number): void {
    for (let i = beginWith; i < this.controls.length; i++) {
      this.controls[i]?.setValue(null);
      this.controls[i]?.markAsUntouched();
    }
  }
}

interface EmployeeOption {
  id: number;
  name: string;
}
interface TimeBlockOption {
  id: number;
  startTime: string;
}
interface ActionOption {
  id: number;
  name: string;
}
