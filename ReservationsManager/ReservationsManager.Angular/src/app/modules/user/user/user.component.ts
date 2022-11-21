import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_services/Account/account.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit {
  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  logout(): void {
    this.accountService.logout();
  }
}
