import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(
    private auth: AuthService,
    private fb: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    if (this.auth.isLoggedIn()) {
      this.router.navigateByUrl('/')
    }
    this.loginForm = this.fb.group({
      email: ["", Validators.required],
      password: ["", Validators.required],
    })
  }

  login() {
    this.auth.login(this.loginForm.value).subscribe((res: any) => {
      console.log(res);
      localStorage.setItem("token", res.data.token)
      this.router.navigateByUrl('/')
    });
  }


}
