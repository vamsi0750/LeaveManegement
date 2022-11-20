import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ResponceData } from '../models/responce';
import { UpdatePassword } from '../models/update-password';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }
  baseUrl = environment.apiEndPoint;

  register(data: object) {
    return this.http.post(this.baseUrl + '/api/Account/user-registration', data)
  }

  login(data: object) {
    return this.http.post(this.baseUrl + '/api/Account/login', data)
  }

  isLoggedIn(): boolean {
    if (this.getToken() == null) {
      return false
    }
    return true;
  }

  logout() {
    localStorage.removeItem('token')
  }

  getToken() {
    return localStorage.getItem('token');
  }

  getUserinfo() : object{
    let token = localStorage.getItem('token');
    if (token != null) {
      return JSON.parse(atob(token.split('.')[1]))
    }
    return {}
  }

  forgotPassword(email:string): Observable<ResponceData>{
    return this.http.get<ResponceData>(this.baseUrl + `/api/Account/forgot-password?email=${email}`)
  }

  updatePassword(updatePassword : UpdatePassword): Observable<ResponceData>{
    return this.http.post<ResponceData>(this.baseUrl + `/api/Account/update-password`,updatePassword)
  }

}
