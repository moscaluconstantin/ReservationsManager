export class ReservationToAddDto {
  constructor(
    public userId: number,
    public actionId: number,
    public employeeId: number,
    public startTimeBlockId: number,
    public date: Date
  ) {}
}
