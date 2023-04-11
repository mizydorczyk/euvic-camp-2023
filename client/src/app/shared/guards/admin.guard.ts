import {Injectable} from '@angular/core';
import {CanActivate} from '@angular/router';
import {map, Observable} from 'rxjs';
import {AccountService} from "../../services/account.service";

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  constructor(private accountService: AccountService) {
  }

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map(user => {
        if (user && user.roles.includes('Admin')) {
          return true;
        } else {
          return false;
        }
      })
    )
  }
}
