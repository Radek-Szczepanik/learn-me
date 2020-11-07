import 'hammerjs';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule} from '@angular/common/http';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { RegistrationComponent } from './components/account/registration/registration.component';
import { LoginComponent } from './components/account/login/login.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { appRouting } from './routing';
import { CalendarService } from './services/calendar/calendar-service';
import { DxSchedulerModule } from 'devextreme-angular';
import { CalendarViewComponent } from './components/calendar/calendar-view/calendar-view.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatListModule, MatSidenavModule, MatIconModule, MatToolbarModule} from '@angular/material';
import { FaqComponent } from './components/main/faq/faq.component';
import { NewsComponent } from './components/main/news/news.component';
import { TranslationsComponent } from './components/main/translations/translations.component';
import { ExerciseComponent } from './components/main/exercise/exercise.component';
import { PrivateLessonsComponent } from './components/main/private-lessons/private-lessons.component';
import { ShopComponent } from './components/main/shop/shop.component';
import { ContactComponent } from './components/main/contact/contact.component';
import { StudentMailComponent } from './components/student/student-mail/student-mail.component';
import { StudentLessonComponent } from './components/student/student-lesson/student-lesson.component';
import { StudentCalendarComponent } from './components/student/student-calendar/student-calendar.component';
import { StudentPaymentComponent } from './components/student/student-payment/student-payment.component';
import { MentorPaymentComponent } from './components/mentor/mentor-payment/mentor-payment.component';
import { MentorCalendarComponent } from './components/mentor/mentor-calendar/mentor-calendar.component';
import { MentorLessonComponent } from './components/mentor/mentor-lesson/mentor-lesson.component';
import { MentorPupilsComponent } from './components/mentor/mentor-pupils/mentor-pupils.component';
import { MentorMailComponent } from './components/mentor/mentor-mail/mentor-mail.component';
import { MentorNewsComponent } from './components/mentor/mentor-news/mentor-news.component';
import { MentorFaqComponent } from './components/mentor/mentor-faq/mentor-faq.component';
import { MentorExerciseComponent } from './components/mentor/mentor-exercise/mentor-exercise.component';
import { MentorOpinionsComponent } from './components/mentor/mentor-opinions/mentor-opinions.component';
import { MatSliderModule } from '@angular/material/slider';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatDialogModule } from '@angular/material/dialog';
import { AddPupilDialog } from './components/mentor/mentor-pupils/mentor-pupils.component.add.pupil';
import { DeletePupilDialog } from './components/mentor/mentor-pupils/mentor-pupils.component.delete.pupil';
import { UpdatePupilDialog } from './components/mentor/mentor-pupils/mentor-pupils.component.update.pupil';
import { AddNewsDialog } from "./components/mentor/mentor-news/mentor-news-add.component";
import { DeleteNewsDialog } from "./components/mentor/mentor-news/mentor-news-delete.components";
import { UpdateNewsDialog } from "./components/mentor/mentor-news/mentor-news-update.component";
import { AddOpinionsDialog } from "./components/mentor/mentor-opinions/mentor-opinions-add.component";
import { UpdateOpinionsDialog } from "./components/mentor/mentor-opinions/mentor-opinions-update.component";
import { DeleteOpinionsDialog } from "./components/mentor/mentor-opinions/mentor-opinions-delete.component";



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CalendarViewComponent,
    RegistrationComponent,
    LoginComponent,
    FaqComponent,
    NewsComponent,
    TranslationsComponent,
    ExerciseComponent,
    PrivateLessonsComponent,
    ShopComponent,
    ContactComponent,
    StudentMailComponent,
    StudentLessonComponent,
    StudentCalendarComponent,
    StudentPaymentComponent,
    MentorPaymentComponent,
    MentorCalendarComponent,
    MentorLessonComponent,
    MentorPupilsComponent,
    MentorMailComponent,
    MentorNewsComponent,
    MentorFaqComponent,
    MentorExerciseComponent,
    MentorOpinionsComponent,
    AddPupilDialog,
    DeletePupilDialog,
    UpdatePupilDialog,
    AddNewsDialog,
    DeleteNewsDialog,
    UpdateNewsDialog,
    AddOpinionsDialog,
    UpdateOpinionsDialog,
    DeleteOpinionsDialog

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    DxSchedulerModule,
    RouterModule.forRoot(appRouting),
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatSidenavModule,
    MatListModule,
    MatButtonModule,
    MatSliderModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatSortModule,
    MatDialogModule
  ],
  providers: [
    CalendarService
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    AddPupilDialog,
    DeletePupilDialog,
    UpdatePupilDialog,
    AddNewsDialog,
    DeleteNewsDialog,
    UpdateNewsDialog,
    AddOpinionsDialog,
    UpdateOpinionsDialog,
    DeleteOpinionsDialog

  ]
})
export class AppModule { }
