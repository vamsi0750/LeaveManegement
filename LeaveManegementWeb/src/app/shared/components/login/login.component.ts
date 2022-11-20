import { HttpErrorResponse, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarHorizontalPosition } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ResponceData } from 'src/app/models/responce';
import { AuthService } from 'src/app/services/auth.service';
import { HelperService } from 'src/app/services/helper.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  horizontalPosition: MatSnackBarHorizontalPosition = 'start';
  display: boolean = false;

  constructor(
    private auth: AuthService,
    private fb: FormBuilder,
    private router: Router,
    private _snackBar: MatSnackBar,
    public helper: HelperService
  ) { }

  ngOnInit(): void {
    if (this.auth.isLoggedIn()) {
      this.router.navigateByUrl('/')
    }
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    })
  }

  login() {
    this.auth.login(this.loginForm.value).subscribe((res: any) => {
      localStorage.setItem("token", res.data.token)
      this._snackBar.open("Login  Success", "👍", { "horizontalPosition": "right", "verticalPosition": "top", "duration": 2000 })
      this.router.navigateByUrl('/')
    }, (error: HttpErrorResponse) => {
    });
  }

  showDialog(): void {
    this.display = true;
  }

  forgotPassword() {
    this.auth.forgotPassword(this.loginForm.value.email).subscribe((x: ResponceData) => {
      this._snackBar.open(x.responceMessage, "👍", { "horizontalPosition": "right", "verticalPosition": "bottom", "duration": 2000 })
    }, (err: HttpErrorResponse) => {
      this._snackBar.open(err.error.responceMessage,'', { "horizontalPosition": "right", "verticalPosition": "bottom", "duration": 2000 })
    });
  }




}
