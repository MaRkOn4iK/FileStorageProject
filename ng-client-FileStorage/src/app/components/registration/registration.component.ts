import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent implements OnInit {
  ngOnInit(): void {}
  constructor(private as: AuthService, private router: Router) {}

  async regist(
    Username: string,
    Password: string,
    Name: string,
    LastName: string,
    Email: string
  ) {
    var tmp = this.as.regist(Username, Password, Name, LastName, Email);

    if ((await tmp) == true) {
      this.as.login(Username, Password).subscribe({
        next: () => {
          this.router.navigate(['']);
        },
        error: () => {
          alert('Wrong login or password');
        },
      });
    }
  }
  public get isLoggedIn(): boolean {
    return this.as.isAuthenticated();
  }
  back() {
    this.router.navigate(['']);
  }
}
