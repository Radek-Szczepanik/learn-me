import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { RegisterComponent } from "./register/register.component";
import { LoginComponent } from "./login/login.component";


export const appRouting: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'rejestracja', component: RegisterComponent },
  { path: 'logowanie', component: LoginComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];
