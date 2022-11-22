import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { AssignedActionDto } from 'src/app/_models/Action/AssignedActionDto';
import { ActionsService } from 'src/app/_services/Actions/actions.service';

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

  actions: Array<AssignedActionDto> = [];

  private controls: Array<AbstractControl | null> = [
    this.employee,
    this.date,
    this.timeBlockId,
  ];

  constructor(
    private fb: FormBuilder,
    private actionsService: ActionsService
  ) {}

  ngOnInit(): void {
    this.actionsService
      .getAssignedActions()
      .subscribe((result) => (this.actions = result));
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
