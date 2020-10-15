import { Routes } from "@angular/router";
import { HomeComponent } from "./components/home/home.component";
import { RegistrationComponent } from "./Components/registration/registration.component";
import { LoginComponent } from "./Components/login/login.component";
import { CalendarViewComponent } from './components/calendar-view/calendar-view.component';
import { SidebarModule } from 'ng-sidebar';




export const appRouting: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'registration', component: RegistrationComponent },
  { path: 'reg', component: SidebarModule },
  { path: 'login', component: LoginComponent },
  //{ path: 'calendar-view', component: CalendarViewComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
  { path: 'calendar-view', component: CalendarViewComponent },
];
