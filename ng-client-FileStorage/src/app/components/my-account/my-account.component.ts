import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.scss'],
})
export class MyAccountComponent implements OnInit {
  Name: string = '';
  LastName: string = '';
  Login: string = '';
  Password: string = '';
  Email: string = '';
  PasswordForCheck: string = '';
  constructor(
    private us: UserService,
    private as: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.us.GetPersonInfo().subscribe({
      next: (data: any) => {
        this.Name = data.name;
        this.LastName = data.lastName;
        this.Login = data.username;
        this.Password = data.password;
        this.PasswordForCheck = data.password;
        this.Email = data.email;     
      },
    });
  }
  back() {
    this.router.navigate(['']);
  }
  update() {
    let pass = prompt('Please enter your password to change your info');
    if (pass == this.PasswordForCheck) {
      this.us
        .ChangeAllInformationAboutUser(
          this.Login,
          this.Password,
          this.Name,
          this.LastName,
          this.Email
        )
        .subscribe({
          next: () => {
            localStorage.setItem('LoginName', this.Login);
            window.location.reload();
          },
          error: (err) => {
            if(err.status == 200)
            {
              localStorage.setItem('LoginName', this.Login);
            window.location.reload();
            }
            else
              console.log(err);
          },
        });
    } else alert('Incorrect password');
  }
  deleteAccount() {
    let pass = prompt(
      'If you really want to delete your account please enter your password to delete'
    );
    if (pass == this.PasswordForCheck) {
      this.us.DeleteAccount().subscribe({
        next: () => {
          this.as.logout();
        },
        error: (err) => {
          if (err.status == 200) this.as.logout();
        },
      });
    }
  }
}
