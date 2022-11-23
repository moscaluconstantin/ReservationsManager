import { ReservationToAddDto } from './ReservationToAddDto';

export class ReservationRequest {
  constructor(
    public actionEmployeeId: number,
    public date: string,
    public timeBlockId: number
  ) {}
}
