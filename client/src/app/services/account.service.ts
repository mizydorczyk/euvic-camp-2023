import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {map, ReplaySubject} from "rxjs";
import {User} from "../../models/user";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {
  }

  login(values: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', values, {withCredentials: true}).pipe(
      map(user => {
        if (user) {
          this.currentUserSource.next(user);
          return user;
        } else {
          this.currentUserSource.next(null);
          return null;
        }
      })
    )
  }

  loadCurrentUser() {
    return this.http.get<User>(this.baseUrl + 'account', {withCredentials: true}).pipe(
      map(user => {
        if (user) {
          this.currentUserSource.next(user);
          return true;
        } else {
          this.currentUserSource.next(null);
          return false;
        }
      })
    )
  }

  register(values: any) {
    return this.http.post(this.baseUrl + 'account/register', values);
  }

  logout() {
    return this.http.post(this.baseUrl + 'account/logout', {}, {withCredentials: true}).pipe(
      map(() => {
        this.currentUserSource.next(null);
      })
    )
  }
}
