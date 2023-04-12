import {Injectable} from '@angular/core';
import {CanActivate, Router, UrlTree} from '@angular/router';
import {map, Observable, take} from 'rxjs';
import {AccountService} from "../../services/account.service";

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) {
  }

  canActivate(): Observable<boolean | UrlTree> {
    return this.accountService.currentUser$.pipe(
      take(1),
      map(user => {
        if (user && user.roles.includes('Admin')) {
          return true;
        } else {
          return this.router.createUrlTree(['/ranking']);
        }
      }),
      map(authenticated => authenticated || this.router.createUrlTree(['/ranking']))
    );
  }
}
