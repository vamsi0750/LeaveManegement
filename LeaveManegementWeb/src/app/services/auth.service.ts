import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { }
  baseUrl = environment.apiEndPoint;

  register(data:object){
    return this.http.post(this.baseUrl + '/api/Account/user-registration',data)
  }
}
