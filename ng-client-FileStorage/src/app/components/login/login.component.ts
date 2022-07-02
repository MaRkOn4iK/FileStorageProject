import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { FileService } from 'src/app/services/file.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(private as: AuthService, private router: Router, private fs: FileService) {}
  login(Username: string, Password: string) {
    this.as.login(Username, Password).subscribe({
      next: () => {
        this.router.navigate(['']);
        this.fs.GetSceletonOfAllFiles();
      },
      error: () => {
        alert('Wrong login or password');
      },
    });
  }
  back() {
    this.router.navigate(['']);
  }
  public get isLoggedIn(): boolean {
    return this.as.isAuthenticated();
  }
  ngOnInit(): void {}
}
