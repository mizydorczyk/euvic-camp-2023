import {Component, OnInit} from '@angular/core';
import {AccountService} from "./services/account.service";
import {NavigationStart, Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private accountService: AccountService, private router: Router) {
  }

  ngOnInit(): void {
    let event: NavigationStart;
    this.router.events.subscribe({
      next: x => {
        if (x instanceof NavigationStart) {
          event = x;
        }
      }
    });

    this.accountService.loadCurrentUser().subscribe({
      next: () => {
        if (event.url === '/email-confirmed') {
          this.router.navigateByUrl('/email-confirmed');
        } else {
          this.router.navigateByUrl('/ranking');
        }
      },
      error: error => {
        if (event.url === '/email-confirmed') {
          this.router.navigateByUrl('/email-confirmed');
        } else {
          this.router.navigateByUrl('/sign-in');
        }
      }
    });
  }
}
