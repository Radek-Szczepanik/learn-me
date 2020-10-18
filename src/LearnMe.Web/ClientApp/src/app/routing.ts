import { Routes } from "@angular/router";
import { HomeComponent } from "./components/home/home.component";
import { RegistrationComponent } from "./Components/account/registration/registration.component";
import { LoginComponent } from "./Components/account/login/login.component";
import { CalendarViewComponent } from './components/calendar/calendar-view/calendar-view.component';
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


export const appRouting: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'registration', component: RegistrationComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
  { path: 'calendar-view', component: CalendarViewComponent },
  { path: 'news', component: NewsComponent},
  { path: 'translation', component: TranslationsComponent },
  { path: 'exercise', component: ExerciseComponent },
  { path: 'lessons', component: PrivateLessonsComponent },
  { path: 'shop', component: ShopComponent },
  { path: 'faq', component: FaqComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'student/mail', component: StudentMailComponent },
  { path: 'student/lesson', component: StudentLessonComponent  },
  { path: 'student/calendar', component: StudentCalendarComponent },
  { path: 'student/payment', component: StudentPaymentComponent },
  { path: 'mentor/payment', component: MentorPaymentComponent },
  { path: 'mentor/calendar', component: MentorCalendarComponent },
  { path: 'mentor/lesson', component: MentorLessonComponent },
  { path: 'mentor/pupils', component: MentorPupilsComponent },
  { path: 'mentor/mail', component: MentorMailComponent },
  { path: 'mentor/news', component: MentorNewsComponent },
  { path: 'mentor/faq', component: MentorFaqComponent },
  { path: 'mentor/exercise', component: MentorExerciseComponent },
  { path: 'mentor/opinions', component: MentorOpinionsComponent },
];
