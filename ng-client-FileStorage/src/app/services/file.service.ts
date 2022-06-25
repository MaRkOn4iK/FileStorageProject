import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AUTH_API_URL } from '../app-injection-tokens';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { ACCESS_TOKEN_KEY } from './auth.service';
@Injectable({
  providedIn: 'root',
})
export class FileService {
  constructor(
    private http: HttpClient,
    @Inject(AUTH_API_URL) private apiUrl: string
  ) {}

  GetSceletonOfPublicFiles(): Observable<any[]> {
    return this.http.get<any>(
      `${this.apiUrl}api/File/GetSceletonOfPublicFiles`
    );
  }
Download(fileId:Number) :Observable<any>
{
  const requestOptions: Object = {
    headers: new HttpHeaders().append(
      'Authorization',
      `Bearer ` + localStorage.getItem(ACCESS_TOKEN_KEY)
    ),
    
  };
  return this.http.get<any>(
    `${this.apiUrl}api/File/GetPrivateFile/${localStorage.getItem('LoginName')}/${fileId}`,
    requestOptions
  );

}




  GetSceletonOfAllFiles(): Observable<any[]> {
    const requestOptions: Object = {
      headers: new HttpHeaders().append(
        'Authorization',
        `Bearer ` + localStorage.getItem(ACCESS_TOKEN_KEY)
      ),
    };
    return this.http.get<any>(
      `${this.apiUrl}api/File/GetSceletonOfAllFiles/${localStorage.getItem(
        'LoginName'
      )}`,
      requestOptions
    );
  }
  deleteItem(id: number): Observable<any> {
    const requestOptions: Object = {
      headers: new HttpHeaders().append(
        'Authorization',
        `Bearer ` + localStorage.getItem(ACCESS_TOKEN_KEY)
      ),
    };
    return this.http.delete(
      `${this.apiUrl}api/File/DeleteFile/${localStorage.getItem(
        'LoginName'
      )}/${id}`,
      requestOptions
    );
  }
  CreateFile(typeOfFile: string, formData: FormData): Observable<any> {
    const requestOptions: Object = {
      headers: new HttpHeaders().append(
        'Authorization',
        `Bearer ` + localStorage.getItem(ACCESS_TOKEN_KEY)
      ),
      reportProgress: true,
      observe: 'events',
    };
    return this.http.post(
      `https://localhost:44331/api/File/CreateFile/${localStorage.getItem(
        'LoginName'
      )}/${typeOfFile}`,
      formData,
      requestOptions
    );
  }
}
