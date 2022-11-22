import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { AssignedActionDto } from 'src/app/_models/Action/AssignedActionDto';
import { AvailableTimeBlocksRequestDto } from 'src/app/_models/AvailableTimeBlocksRequestDto';
import { WorkingEmployeeDto } from 'src/app/_models/Employee/WorkingEmployeeDto';
import { TimeBlockDto } from 'src/app/_models/TimeBlockDto';
import { ActionsService } from 'src/app/_services/Actions/actions.service';
import { EmployeeService } from 'src/app/_services/Employee/employee.service';
import { ReservationsService } from 'src/app/_services/Reservations/reservations.service';

@Component({
  selector: 'app-add-user-reservation',
  templateUrl: './add-user-reservation.component.html',
  styleUrls: ['./add-user-reservation.component.css'],
})
export class AddUserReservationComponent implements OnInit {
  reservationForm = this.fb.group({
    actionId: this.fb.control('', Validators.required),
    employeeId: this.fb.control('', Validators.required),
    date: this.fb.control('', Validators.required),
    timeBlockId: this.fb.control('', Validators.required),
  });

  get actionId(): AbstractControl | null {
    return this.reservationForm.controls['actionId'];
  }

  get employeeId(): AbstractControl | null {
    return this.reservationForm.controls['employeeId'];
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

  actions: Array<AssignedActionDto> = [];
  employees: Array<WorkingEmployeeDto> = [];
  timeBlocks: TimeBlockDto[] = [];

  private get reservationRequest(): ReservationRequest {
    return this.reservationForm.value as ReservationRequest;
  }

  constructor(
    private fb: FormBuilder,
    private actionsService: ActionsService,
    private employeeService: EmployeeService,
    private reservationsService: ReservationsService
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
    this.resetControl(this.employeeId);
    this.resetControl(this.timeBlockId);
    this.updateTimeBlocks();

    this.employeeService
      .getEmployeesAssignedToAction(this.reservationRequest.actionId!)
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

  private getTimeBlocks(): void {
    let requestDto = {
      ...new AvailableTimeBlocksRequestDto(0, 0, new Date()),
      ...this.reservationForm.value,
    } as AvailableTimeBlocksRequestDto;

    this.reservationsService
      .getAvailableTimeBlocks(requestDto)
      .subscribe((result) => (this.timeBlocks = result));
  }

  private updateTimeBlocks() {
    if (this.date?.invalid) return;

    this.getTimeBlocks();
  }
}

interface ReservationRequest {
  actionId: number | null;
  employee: number | null;
  date: Date | null;
  timeBlockId: number | null;
}
