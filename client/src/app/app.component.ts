import {Component, OnInit} from '@angular/core';
import {AccountService} from "./services/account.service";
import {take} from "rxjs";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.accountService.loadCurrentUser().pipe(take(1)).subscribe();
  }
}
