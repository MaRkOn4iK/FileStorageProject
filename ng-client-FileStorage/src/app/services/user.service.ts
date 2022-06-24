import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AUTH_API_URL } from '../app-injection-tokens';
import { ACCESS_TOKEN_KEY } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private http: HttpClient,
    @Inject(AUTH_API_URL) private apiUrl: string
  ) {}

  GetPersonInfo() {
    const requestOptions: Object = {
      headers: new HttpHeaders().append(
        'Authorization',
        `Bearer ` + localStorage.getItem(ACCESS_TOKEN_KEY)
      ),
    };

    return this.http.get(
      `${this.apiUrl}api/User/GetInfo/${localStorage.getItem('LoginName')}`,
      requestOptions
    );
  }

  DeleteAccount() {
    const requestOptions: Object = {
      headers: new HttpHeaders().append(
        'Authorization',
        `Bearer ` + localStorage.getItem(ACCESS_TOKEN_KEY)
      ),
    };

    return this.http.delete(
      `${this.apiUrl}api/User/DeleteAccount/${localStorage.getItem(
        'LoginName'
      )}`,
      requestOptions
    );
  }

  ChangeAllInformationAboutUser(
    newUsername: string,
    newPassword: string,
    newName: string,
    newLastName: string,
    newEmail: string
  ) {
    const requestOptions: Object = {
      headers: new HttpHeaders().append(
        'Authorization',
        `Bearer ` + localStorage.getItem(ACCESS_TOKEN_KEY)
      ),
    };
    var Username = localStorage.getItem('LoginName');
    return this.http.post(
      `${this.apiUrl}api/User/ChageAllInfo`,
      { Username, newUsername, newPassword, newName, newLastName, newEmail },
      requestOptions
    );
  }
}
