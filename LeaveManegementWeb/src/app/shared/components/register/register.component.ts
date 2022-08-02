import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { LoginComponent } from '../login/login.component';

LoginComponent
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['../login/login.component.scss']
})
export class RegisterComponent implements OnInit {

  signupForm:FormGroup;
  constructor(
    private fb:FormBuilder,
    private auth:AuthService
    ) { }

  ngOnInit(): void {
    this.signupForm = this.fb.group({
      name:["",Validators.required],
      email:["",Validators.email],
      password:[""],
      confirmPassword:[""]
    });
  }

  signup(){
    this.auth.register(this.signupForm.value).subscribe(res=>{
      console.log(res);
    });
  }

}
