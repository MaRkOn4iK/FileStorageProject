import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { AUTH_API_URL, LoginName } from '../app-injection-tokens';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Token } from '../models/token';
export const ACCESS_TOKEN_KEY = 'filestorage_access_token';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private http: HttpClient,
    @Inject(AUTH_API_URL) private apiUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) {}
  async regist(
    Username: string,
    Password: string,
    Name: string,
    LastName: string,
    Email: string
  ): Promise<boolean> {
    const requestOptions: Object = {
      headers: new HttpHeaders().append(
        'Authorization',
        'Bearer ' + localStorage.getItem(ACCESS_TOKEN_KEY)
      ),
      responseType: 'text',
    };
    var flag = 0;
    const tmp = this.http
      .post<string>(
        `${this.apiUrl}api/Login/Regist`,
        { Username, Password, Name, LastName, Email },
        requestOptions
      )
      .subscribe(
        (data) => {
          flag++;
        },
        (error) => {
          alert(error.error);
        }
      );
    await new Promise((f) => setTimeout(f, 1000));
    if (flag == 1) {
      return true;
    }
    return false;
  }

  login(Username: string, Password: string): Observable<Token> {
    const requestOptions: Object = {
      headers: new HttpHeaders().append(
        'Authorization',
        `Bearer ` + localStorage.getItem(ACCESS_TOKEN_KEY)
      ),
      responseType: 'text',
    };
    return this.http
      .post<Token>(
        `${this.apiUrl}api/Login/Login`,
        { Username, Password },
        requestOptions
      )
      .pipe(
        tap((token) => {
          var tmp = token.toString();
          
          localStorage.setItem(ACCESS_TOKEN_KEY, tmp);
          localStorage.setItem('LoginName', Username);
        })
      );
  }

  isAuthenticated(): boolean {
    let token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token != null && !this.jwtHelper.isTokenExpired(token);
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    localStorage.removeItem('LoginName');
    this.router.navigate(['']);
  }
}
