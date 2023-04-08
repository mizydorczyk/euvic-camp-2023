import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {AccountService} from "../services/account.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent {
  passwordPattern = '^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$';
  registerForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.pattern(this.passwordPattern)])
  })
  emailControl = this.registerForm.controls['email'];
  passwordControl = this.registerForm.controls['password'];

  constructor(private accountService: AccountService, private router: Router, private toastr: ToastrService) {
  }

  onSubmit() {
    return this.accountService.register(this.registerForm.value).subscribe({
      next: () => {
        this.toastr.success('User successfully created. Please confirm your email in order to sign in');
        this.registerForm.reset();
        this.router.navigateByUrl('/sign-in');
      },
    })
  }
}
