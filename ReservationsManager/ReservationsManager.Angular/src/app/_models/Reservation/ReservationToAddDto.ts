import { ReservationRequest } from './ReservationRequest';

export class ReservationToAddDto {
  constructor(
    public userId: number,
    public actionEmployeeId: number,
    public timeBlockId: number,
    public date: string
  ) {}

  static Create(
    userId: number,
    reservationRequest: ReservationRequest
  ): ReservationToAddDto {
    return new ReservationToAddDto(
      userId,
      reservationRequest.actionEmployeeId,
      reservationRequest.timeBlockId,
      reservationRequest.date
    );
  }
}
