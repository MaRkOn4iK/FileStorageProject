import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './components/home/home.component';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import {
  AUTH_API_URL,
  FileSTORAGE_API_URL,
  LoginName,
} from './app-injection-tokens';
import { environment } from 'src/environments/environment';
import { JwtModule } from '@auth0/angular-jwt';
import { ACCESS_TOKEN_KEY } from './services/auth.service';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { AboutComponent } from './components/about/about.component';
import { SharedFilesComponent } from './components/shared-files/shared-files.component';
import { MyFilesComponent } from './components/my-files/my-files.component';
import { CreateNewFileComponent } from './components/create-new-file/create-new-file.component';
import { MyAccountComponent } from './components/my-account/my-account.component';
export function mytokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}
@NgModule({
  exports: [AppComponent, HomeComponent],
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegistrationComponent,
    NavbarComponent,
    AboutComponent,
    SharedFilesComponent,
    MyFilesComponent,
    CreateNewFileComponent,
    MyAccountComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatFormFieldModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: mytokenGetter,
        throwNoTokenError: true,
        allowedDomains: environment.tokenWhiteListedDomains,
      },
    }),
    RouterModule.forRoot([{ path: '', component: AppComponent }]),
  ],
  providers: [
    {
      provide: AUTH_API_URL,
      useValue: environment.authApi,
    },
    {
      provide: FileSTORAGE_API_URL,
      useValue: environment.authApi,
    },
    {
      provide: LoginName,
      useValue: environment.login,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
