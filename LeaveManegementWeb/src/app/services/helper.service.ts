import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class HelperService {

  requiresMessage: string = "This Filed is Required";
  emailMessage: string = "Please Enter Valid Email";
  passwordMessage: string = "Please Enter More than 5 charaters"
  constructor() { }

  validate(formGroup: FormGroup, controlName: string) {
    return (
      formGroup.get(controlName).touched &&
      formGroup.get(controlName).errors
    );
  }
}
