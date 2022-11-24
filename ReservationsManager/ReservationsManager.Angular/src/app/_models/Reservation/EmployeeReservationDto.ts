export class EmployeeReservationDto {
  constructor(
    public id: number,
    public userName: string,
    public actionName: string,
    public date: Date,
    public startTime: string,
    public canceled: boolean
  ) {}
}
