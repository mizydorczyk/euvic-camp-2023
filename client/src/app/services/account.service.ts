import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {BehaviorSubject, map} from "rxjs";
import {User} from "../../models/user";
import jwtDecode from "jwt-decode";
import {DecodedToken} from "../../models/decodedToken";

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
    return this.http.post(this.baseUrl + 'account/login', values).pipe(
      map(response => {
        const decodedToken = this.DecodeToken(response.toString()) as DecodedToken;
        const user = {
          userName: decodedToken.nameid,
          email: decodedToken.email,
          token: response.toString(),
          roles: decodedToken.role
        } as User;
        this.currentUserSource.next(user);
      })
    )
  }

  register(values: any) {
    return this.http.post(this.baseUrl + 'account/register', values);
  }

  logout() {
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/sign-in');
  }

  private DecodeToken(token: string) {
    return jwtDecode(token);
  }
}
