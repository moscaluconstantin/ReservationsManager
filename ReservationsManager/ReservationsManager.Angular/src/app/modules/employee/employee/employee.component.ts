import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_services/Account/account.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css'],
})
export class EmployeeComponent implements OnInit {
  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  logout(): void {
    this.accountService.logout();
  }
}
