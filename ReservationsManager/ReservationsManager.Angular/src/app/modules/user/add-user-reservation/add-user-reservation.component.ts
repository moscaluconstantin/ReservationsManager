import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { AssignedActionDto } from 'src/app/_models/Action/AssignedActionDto';
import { AvailableTimeBlocksRequestDto } from 'src/app/_models/Reservation/AvailableTimeBlocksRequestDto';
import { WorkingEmployeeDto } from 'src/app/_models/Employee/WorkingEmployeeDto';
import { ReservationRequest } from 'src/app/_models/Reservation/ReservationRequest';
import { ReservationToAddDto } from 'src/app/_models/Reservation/ReservationToAddDto';
import { TimeBlockDto } from 'src/app/_models/TimeBlock/TimeBlockDto';
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
    startTimeBlockId: this.fb.control('', Validators.required),
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
    return this.reservationForm.controls['startTimeBlockId'];
  }

  actions: Array<AssignedActionDto> = [];
  employees: Array<WorkingEmployeeDto> = [];
  timeBlocks: TimeBlockDto[] = [];
  minDate: Date = new Date();
  loading: boolean = false;

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
    this.getActions();
  }

  addReservation(): void {
    if (this.reservationForm.invalid || this.loading) return;

    this.loading = true;
    this.reservationsService.addReservation(this.reservationRequest).subscribe({
      next: (result) => {
        this.loading = false;
        console.log(`Reservation add status: ${result}`);
      },
      error: (err) => {
        this.loading = false;
        console.log(`Failed: ${err}`);
      },
    });
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

  private getActions(): void {
    this.loading = true;

    this.actionsService.getAssignedActions().subscribe({
      next: (result) => {
        this.loading = false;
        this.actions = result;
      },
      error: (err) => (this.loading = false),
    });
  }

  private getTimeBlocks(): void {
    let requestDto = {
      ...new AvailableTimeBlocksRequestDto(0, 0, new Date()),
      ...this.reservationForm.value,
    } as AvailableTimeBlocksRequestDto;

    this.reservationsService.getAvailableTimeBlocks(requestDto).subscribe({
      next: (result) => {
        this.loading = false;
        this.timeBlocks = result;
      },
      error: (err) => (this.loading = false),
    });
  }

  private updateTimeBlocks() {
    if (this.date?.invalid) return;

    this.getTimeBlocks();
  }
}
