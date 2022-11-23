export class AvailableTimeBlocksRequestDto {
  constructor(
    public actionId: number,
    public employeeId: number,
    public date: Date
  ) {}
}
