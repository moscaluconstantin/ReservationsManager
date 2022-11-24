export class UserReservationDto {
  constructor(
    public employeeName: string,
    public actionName: string,
    public date: Date,
    public startTime: string,
    public canceled: boolean
  ) {}
}
