import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { AssignedActionDto } from 'src/app/_models/Action/AssignedActionDto';
import { AvailableTimeBlocksRequestDto } from 'src/app/_models/Reservation/AvailableTimeBlocksRequestDto';
import { WorkingEmployeeDto } from 'src/app/_models/Employee/WorkingEmployeeDto';
import { ReservationRequest } from 'src/app/_models/Reservation/ReservationRequest';
import { TimeBlockDto } from 'src/app/_models/TimeBlock/TimeBlockDto';
import { ActionsService } from 'src/app/_services/Actions/actions.service';
import { EmployeeService } from 'src/app/_services/Employee/employee.service';
import { ReservationsService } from 'src/app/_services/Reservations/reservations.service';
import { DatePipe } from '@angular/common';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-add-user-reservation',
  templateUrl: './add-user-reservation.component.html',
  styleUrls: ['./add-user-reservation.component.css'],
})
export class AddUserReservationComponent implements OnInit {
  reservationForm = this.fb.group({
    actionId: this.fb.control('', Validators.required),
    actionEmployeeId: this.fb.control('', Validators.required),
    date: this.fb.control('', Validators.required),
    timeBlockId: this.fb.control('', Validators.required),
  });

  get actionId(): AbstractControl | null {
    return this.reservationForm.controls['actionId'];
  }

  get employeeId(): AbstractControl | null {
    return this.reservationForm.controls['actionEmployeeId'];
  }

  get date(): AbstractControl | null {
    return this.reservationForm.controls['date'];
  }

  get timeBlockId(): AbstractControl | null {
    return this.reservationForm.controls['timeBlockId'];
  }

  get selectedDate(): string {
    return this.datePipe.transform(this.date?.value, 'yyyy-MM-dd')!;
  }

  actions: Array<AssignedActionDto> = [];
  employees: Array<WorkingEmployeeDto> = [];
  timeBlocks: TimeBlockDto[] = [];
  minDate: Date = new Date();

  private sending: boolean = false;

  constructor(
    private fb: FormBuilder,
    private actionsService: ActionsService,
    private employeeService: EmployeeService,
    private reservationsService: ReservationsService,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.getActions();
  }

  addReservation(): void {
    if (this.reservationForm.invalid || this.sending) return;

    this.sending = true;

    let reservationRequest = new ReservationRequest(
      this.employeeId?.value,
      this.selectedDate,
      this.timeBlockId?.value
    );

    this.reservationsService
      .addReservation(reservationRequest)
      .pipe(finalize(() => (this.sending = false)))
      .subscribe();
  }

  onActionChanged(): void {
    this.resetControl(this.employeeId);
    this.resetControl(this.timeBlockId);
    this.updateTimeBlocks();

    this.employeeService
      .getEmployeesAssignedToAction(this.actionId?.value)
      .subscribe((result) => (this.employees = result));
  }

  onEmployeeChanged(): void {
    this.resetControl(this.timeBlockId);
    this.updateTimeBlocks();
  }

  onDatePickerClosed(): void {
    if (this.date?.invalid) return;

    this.resetControl(this.timeBlockId);
    this.getTimeBlocks();
  }

  private resetControl(control: AbstractControl | null): void {
    control?.setValue(null);
    control?.markAsUntouched();
  }

  private getActions(): void {
    this.actionsService
      .getAssignedActions()
      .subscribe((result) => (this.actions = result));
  }

  private getTimeBlocks(): void {
    let requestDto = new AvailableTimeBlocksRequestDto(
      this.employeeId?.value,
      this.selectedDate
    );

    this.reservationsService
      .getAvailableTimeBlocks(requestDto)
      .subscribe((result) => {
        this.timeBlocks = result;

        if (this.timeBlocks.length == 0) {
          this.date?.setErrors({ busyDay: true });
        }
      });
  }

  private updateTimeBlocks() {
    if (!this.date?.valid) return;

    this.getTimeBlocks();
  }
}
