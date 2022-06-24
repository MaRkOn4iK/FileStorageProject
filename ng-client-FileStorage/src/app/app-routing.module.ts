import { NgModule } from '@angular/core';
import { MyFilesComponent } from './components/my-files/my-files.component';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { AboutComponent } from './components/about/about.component';
import { SharedFilesComponent } from './components/shared-files/shared-files.component';
import { AuthGuard } from './guards/auth.guard';
import { CreateNewFileComponent } from './components/create-new-file/create-new-file.component';
import { MyAccountComponent } from './components/my-account/my-account.component';
const routes: Routes = [
  {
    path: '', component: HomeComponent
  },
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'registration', component: RegistrationComponent
  },
  {
    path: 'about', component: AboutComponent
  },
  {
    path: 'shared-files', component: SharedFilesComponent
  },
  {
    path: 'my-files', component: MyFilesComponent, canActivate: [AuthGuard]
  },
  {
    path: 'create-new-file', component: CreateNewFileComponent, canActivate: [AuthGuard]
  }
  ,
  {
    path: 'my-account', component: MyAccountComponent, canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
