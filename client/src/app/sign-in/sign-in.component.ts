import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AccountService} from "../services/account.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {take} from "rxjs";
import {User} from "../../models/user";

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  constructor(private accountService: AccountService, private router: Router, private toastr: ToastrService) {
    accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user: User | null) => {
        if (user) this.router.navigateByUrl('ranking');
      }
    });
  }

  onSubmit() {
    return this.accountService.login(this.loginForm.value).subscribe({
      next: () => {
        this.loginForm.reset();
        this.toastr.success('User successfully logged in');
        this.router.navigateByUrl('/ranking');
      },
    });
  }
}
