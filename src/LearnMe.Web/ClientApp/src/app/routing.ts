import { Routes } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { RegistrationComponent } from "./Components/registration/registration.component";
import { LoginComponent } from "./Components/login/login.component";
import { CalendarViewComponent } from './components/calendar-view/calendar-view.component';



export const appRouting: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'registration', component: RegistrationComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'calendar-view', component: CalendarViewComponent },
];
