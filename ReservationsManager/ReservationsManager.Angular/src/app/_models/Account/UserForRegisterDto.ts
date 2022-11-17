import { CommonDataForRegister } from './CommonDataForRegister';

export class UserForRegisterDto extends CommonDataForRegister {
  constructor(
    name: string,
    phoneNumber: string,
    username: string,
    password: string,
    email?: string
  ) {
    super(name, phoneNumber, username, password, email);
  }
}
