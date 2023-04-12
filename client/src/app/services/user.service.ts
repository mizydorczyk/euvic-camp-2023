import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {User} from "../../models/user";
import {map, of} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  users: User[] = [];
  changed = false;
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getUsers() {
    if (!this.changed && this.users.length > 0) return of(this.users);
    return this.http.get<User[]>(this.baseUrl + 'user', {withCredentials: true}).pipe(
      map(users => {
        this.users = users;
        this.changed = false;
        return users;
      }));
  }

  deleteUser(email: string) {
    return this.http.delete(this.baseUrl + 'user/' + email, {withCredentials: true});
  }

  updateUser(email: string, values: any) {
    return this.http.put<User>(this.baseUrl + 'user/' + email, values, {withCredentials: true}).pipe(map(() => this.changed = true));
  }

  registerUser(values: any) {
    return this.http.post(this.baseUrl + 'account/register', values, {withCredentials: true}).pipe(map(() => this.changed = true));
  }
}
