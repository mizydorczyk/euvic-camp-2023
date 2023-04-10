import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {BehaviorSubject} from "rxjs";
import {User} from "../../models/user";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  private usersSource = new BehaviorSubject<User[] | null>(null);
  usersSource$ = this.usersSource.asObservable();

  constructor(private http: HttpClient) {
  }

  getUsers() {
    return this.http.get<User[]>(this.baseUrl + 'user', {withCredentials: true});
  }

  deleteUser(email: string) {
    return this.http.delete(this.baseUrl + 'user/' + email, {withCredentials: true});
  }

  updateUser(email: string, values: any) {
    return this.http.put<User>(this.baseUrl + 'user/' + email, values, {withCredentials: true});
  }

  registerUser(values: any) {
    return this.http.post(this.baseUrl + 'account/register', values, {withCredentials: true});
  }
}
