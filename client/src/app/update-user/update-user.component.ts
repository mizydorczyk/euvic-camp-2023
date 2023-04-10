import {Component} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {UserService} from "../services/user.service";

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.scss']
})
export class UpdateUserComponent {
  passwordPattern = '^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$';

  updateForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.pattern(this.passwordPattern)])
  });

  email: string;

  constructor(private router: Router, private toastr: ToastrService, private userService: UserService) {
    const currentNavigation = this.router.getCurrentNavigation();
    this.email = currentNavigation?.extras?.state?.['email'] as string;
    this.updateForm.controls?.['email'].setValue(this.email);
  }

  onSubmit() {
    return this.userService.updateUser(this.email, this.updateForm.value).subscribe({
      next: () => {
        this.toastr.success('User successfully updated.');
        this.router.navigateByUrl('/users');
      },
    })
  }
}
