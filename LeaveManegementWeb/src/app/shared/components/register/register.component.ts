import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { HelperService } from 'src/app/services/helper.service';
import { LoginComponent } from '../login/login.component';

LoginComponent
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['../login/login.component.scss']
})
export class RegisterComponent implements OnInit {

  signupForm: FormGroup;
  confirmPasswordMessage: boolean = false;
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    public helper: HelperService) { }

  ngOnInit(): void {
    this.signupForm = this.fb.group({
      name: ["", Validators.required],
      email: ["", [Validators.email, Validators.required]],
      password: ["", [Validators.required, Validators.minLength(6)]],
      confirmPassword: ["", [Validators.required, Validators.minLength(6)]]
    });
  }

  signup() {
    this.confirmPasswordMessage = false;
    if (this.signupForm.value.password != this.signupForm.value.confirmPassword) {
      this.confirmPasswordMessage = true;
      return;
    }
    this.auth.register(this.signupForm.value).subscribe();
  }

}
