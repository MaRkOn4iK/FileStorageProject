import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  constructor(private as: AuthService, private router: Router) {}
  logout() {
    this.as.logout();
    this.router.navigate(['']);
  }
  public get isLoggedIn(): boolean {
    return this.as.isAuthenticated();
  }
}
