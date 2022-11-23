export class ReservationRequest {
  constructor(
    public actionEmployeeId: number,
    public date: Date,
    public timeBlockId: number
  ) {}
}
