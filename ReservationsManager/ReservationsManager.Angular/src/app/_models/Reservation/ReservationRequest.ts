export class ReservationRequest {
  constructor(
    public actionId: number | null,
    public employeeId: number | null,
    public date: Date | null,
    public startTimeBlockId: number | null
  ) {}
}
