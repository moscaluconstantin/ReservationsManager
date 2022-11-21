import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUserReservationComponent } from './add-user-reservation.component';

describe('AddUserReservationComponent', () => {
  let component: AddUserReservationComponent;
  let fixture: ComponentFixture<AddUserReservationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddUserReservationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddUserReservationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
