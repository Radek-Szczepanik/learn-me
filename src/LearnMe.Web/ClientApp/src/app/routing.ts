import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { RegistrationComponent } from "./Components/registration/registration.component";
import { LoginComponent } from "./Components/login/login.component";


export const appRouting: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];
