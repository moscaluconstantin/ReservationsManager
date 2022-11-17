export class LoginResponseDto {
  id: number;
  role: string;
  accessToken: string;

  constructor(id: number, role: string, accessToken: string) {
    this.id = id;
    this.role = role;
    this.accessToken = accessToken;
  }
}
