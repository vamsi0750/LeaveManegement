import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: []
})
export class AppHeaderComponent {
  constructor(
    private auth:AuthService,
    private router:Router
    ){
  }

  logout(){
    this.auth.logout();
    this.router.navigateByUrl("/login")
  }
}
