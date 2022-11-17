export class CommonDataForRegister {
  name: string;
  phoneNumber: string;
  email?: string;
  username: string;
  password: string;

  constructor(
    name: string,
    phoneNumber: string,
    username: string,
    password: string,
    email?: string
  ) {
    this.name = name;
    this.phoneNumber = phoneNumber;
    this.email = email;
    this.username = username;
    this.password = password;
  }
}
