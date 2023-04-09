import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {BehaviorSubject, map} from "rxjs";
import {User} from "../../models/user";
import jwtDecode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {
  }

  login(values: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', values, {withCredentials: true}).pipe(
      map(user => {
        this.currentUserSource.next(user);
      })
    )
  }

  loadCurrentUser() {
    return this.http.get<User>(this.baseUrl + 'account', {withCredentials: true}).pipe(
      map(user => {
        this.currentUserSource.next(user);
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

  private DecodeToken(token: string) {
    return jwtDecode(token);
  }
}
