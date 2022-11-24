import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeReservationsListComponent } from './employee-reservations-list.component';

describe('EmployeeReservationsListComponent', () => {
  let component: EmployeeReservationsListComponent;
  let fixture: ComponentFixture<EmployeeReservationsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeReservationsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeReservationsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
