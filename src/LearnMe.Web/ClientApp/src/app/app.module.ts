import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule} from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegistrationComponent } from './Components/registration/registration.component';
import { LoginComponent } from './Components/login/login.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbPaginationModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { appRouting } from './routing';

import { CalendarViewComponent } from './components/calendar-view/calendar-view.component';
import { CalendarService } from './services/calendar/calendar-service';

import { DxSchedulerModule } from 'devextreme-angular';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    CalendarViewComponent,
    RegistrationComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'calendar-view', component: CalendarViewComponent },
    ]),
    DxSchedulerModule,
  ],
  providers: [
    CalendarService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
