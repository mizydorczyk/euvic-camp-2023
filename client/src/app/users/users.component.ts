import {Component, OnInit} from '@angular/core';
import {User} from "../../models/user";
import {UserService} from "../services/user.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  users: User[] = [];

  constructor(private userService: UserService, private toastr: ToastrService, private router: Router) {
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    return this.userService.getUsers().subscribe({
      next: users => this.users = users
    })
  }

  delete(user: User) {
    return this.userService.deleteUser(user.email).subscribe({
      next: () => {
        this.toastr.success(user.email + ' deleted successfully');
        this.users = this.users.filter(x => x.email !== user.email);
      }
    })
  }

  register() {
    this.router.navigateByUrl('/register-new-user');
  }

  update(user: User) {
    this.router.navigateByUrl('/update-user', {state: {email: user.email}});
  }
}
