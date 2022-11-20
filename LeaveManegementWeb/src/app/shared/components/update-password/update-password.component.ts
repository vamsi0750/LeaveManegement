import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { ResponceData } from 'src/app/models/responce';
import { UpdatePassword } from 'src/app/models/update-password';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.css'],
  providers: [ConfirmationService]

})
export class UpdatePasswordComponent implements OnInit {

  updatePasswordDeatils: UpdatePassword = new UpdatePassword();
  constructor(
    private activateRoute: ActivatedRoute,
    private confirmationService: ConfirmationService,
    private authService: AuthService,
    private _snackBar: MatSnackBar,

  ) { }

  ngOnInit(): void {
    this.activateRoute.queryParams.subscribe(res => {
      this.updatePasswordDeatils.token = res?.token
    });
  }

  updatePassword(): void {
    if (this.updatePasswordDeatils.password != this.updatePasswordDeatils.confirmPassword) {
      this.confirmationService.confirm({
        message: 'password and confirm password should be same',
        header: 'Confirmation',
        icon: 'pi pi-exclamation-triangle',
        acceptLabel: 'OK',
        rejectVisible: false
      });
      return;
    }
    this.authService.updatePassword(this.updatePasswordDeatils).subscribe((res: ResponceData) => {
      this._snackBar.open(res.responceMessage, "👍", { "horizontalPosition": "right", "verticalPosition": "top", "duration": 2000 })
    }, (err: HttpErrorResponse) => {
      this._snackBar.open(err.error.responceMessage, "", { "horizontalPosition": "right", "verticalPosition": "top", "duration": 2000 })

    })
  }

}
