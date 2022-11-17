import { CommonDataForRegister } from './CommonDataForRegister';

export class EmployeeForRegisterDto extends CommonDataForRegister {
  eperience: number;
  description: string;

  constructor(
    name: string,
    phoneNumber: string,
    username: string,
    password: string,
    eperience: number,
    description: string,
    email?: string
  ) {
    super(name, phoneNumber, username, password, email);
    this.eperience = eperience;
    this.description = description;
  }
}
