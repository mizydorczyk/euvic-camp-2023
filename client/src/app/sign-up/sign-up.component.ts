import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AccountService} from "../services/account.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {UserService} from "../services/user.service";

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent {
  registerAsAdmin = false;

  passwordPattern = '^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$';

  registerForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.pattern(this.passwordPattern)])
  })

  constructor(private accountService: AccountService, private router: Router, private toastr: ToastrService, private userService: UserService) {
    const currentNavigation = this.router.getCurrentNavigation();
    if (this.router.routerState.snapshot.url === '/register-new-user') {
      this.registerAsAdmin = true;
    }
  }

  onSubmit() {
    this.registerForm.reset();
    if (this.registerAsAdmin) {
      return this.userService.registerUser(this.registerForm.value).subscribe({
        next: () => {
          this.toastr.success('User successfully created.');
          this.router.navigateByUrl('/users');
        },
      })
    } else {
      return this.accountService.register(this.registerForm.value).subscribe({
        next: () => {
          this.toastr.success('User successfully created. Please confirm your email in order to sign in');
          this.router.navigateByUrl('/sign-in');
        },
      })
    }
  }
}
