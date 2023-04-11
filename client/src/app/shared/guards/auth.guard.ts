import {Injectable} from '@angular/core';
import {CanActivate} from '@angular/router';
import {map} from 'rxjs';
import {AccountService} from "../../services/account.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService) {
  }

  canActivate() {
    return this.accountService.currentUser$.pipe(
      map(user => {
          if (user && (user.roles.includes('User') || user.roles.includes('Admin'))) {
            return true;
          } else {
            return false;
          }
        }
      ))
  }
}
