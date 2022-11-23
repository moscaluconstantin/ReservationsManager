export class ReservationToAddDto {
  constructor(
    public userId: number,
    public actionEmployeeId: number,
    public timeBlockId: number,
    public date: Date
  ) {}
}
