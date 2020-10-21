"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.appRouting = void 0;
var home_component_1 = require("./components/home/home.component");
var registration_component_1 = require("./Components/account/registration/registration.component");
var login_component_1 = require("./Components/account/login/login.component");
var calendar_view_component_1 = require("./components/calendar/calendar-view/calendar-view.component");
var faq_component_1 = require("./components/main/faq/faq.component");
var news_component_1 = require("./components/main/news/news.component");
var translations_component_1 = require("./components/main/translations/translations.component");
var exercise_component_1 = require("./components/main/exercise/exercise.component");
var private_lessons_component_1 = require("./components/main/private-lessons/private-lessons.component");
var shop_component_1 = require("./components/main/shop/shop.component");
var contact_component_1 = require("./components/main/contact/contact.component");
var student_mail_component_1 = require("./components/student/student-mail/student-mail.component");
var student_lesson_component_1 = require("./components/student/student-lesson/student-lesson.component");
var student_calendar_component_1 = require("./components/student/student-calendar/student-calendar.component");
var student_payment_component_1 = require("./components/student/student-payment/student-payment.component");
var mentor_payment_component_1 = require("./components/mentor/mentor-payment/mentor-payment.component");
var mentor_calendar_component_1 = require("./components/mentor/mentor-calendar/mentor-calendar.component");
var mentor_lesson_component_1 = require("./components/mentor/mentor-lesson/mentor-lesson.component");
var mentor_pupils_component_1 = require("./components/mentor/mentor-pupils/mentor-pupils.component");
var mentor_mail_component_1 = require("./components/mentor/mentor-mail/mentor-mail.component");
var mentor_news_component_1 = require("./components/mentor/mentor-news/mentor-news.component");
var mentor_faq_component_1 = require("./components/mentor/mentor-faq/mentor-faq.component");
var mentor_exercise_component_1 = require("./components/mentor/mentor-exercise/mentor-exercise.component");
var mentor_opinions_component_1 = require("./components/mentor/mentor-opinions/mentor-opinions.component");
exports.appRouting = [
    { path: '', component: home_component_1.HomeComponent, pathMatch: 'full' },
    { path: 'registration', component: registration_component_1.RegistrationComponent },
    { path: 'login', component: login_component_1.LoginComponent },
    { path: '**', redirectTo: 'home', pathMatch: 'full' },
    { path: 'calendar-view', component: calendar_view_component_1.CalendarViewComponent },
    { path: 'news', component: news_component_1.NewsComponent },
    { path: 'translation', component: translations_component_1.TranslationsComponent },
    { path: 'exercise', component: exercise_component_1.ExerciseComponent },
    { path: 'lessons', component: private_lessons_component_1.PrivateLessonsComponent },
    { path: 'shop', component: shop_component_1.ShopComponent },
    { path: 'faq', component: faq_component_1.FaqComponent },
    { path: 'contact', component: contact_component_1.ContactComponent },
    { path: 'student/mail', component: student_mail_component_1.StudentMailComponent },
    { path: 'student/lesson', component: student_lesson_component_1.StudentLessonComponent },
    { path: 'student/calendar', component: student_calendar_component_1.StudentCalendarComponent },
    { path: 'student/payment', component: student_payment_component_1.StudentPaymentComponent },
    { path: 'mentor/payment', component: mentor_payment_component_1.MentorPaymentComponent },
    { path: 'mentor/calendar', component: mentor_calendar_component_1.MentorCalendarComponent },
    { path: 'mentor/lesson', component: mentor_lesson_component_1.MentorLessonComponent },
    { path: 'mentor/pupils', component: mentor_pupils_component_1.MentorPupilsComponent },
    { path: 'mentor/mail', component: mentor_mail_component_1.MentorMailComponent },
    { path: 'mentor/news', component: mentor_news_component_1.MentorNewsComponent },
    { path: 'mentor/faq', component: mentor_faq_component_1.MentorFaqComponent },
    { path: 'mentor/exercise', component: mentor_exercise_component_1.MentorExerciseComponent },
    { path: 'mentor/opinions', component: mentor_opinions_component_1.MentorOpinionsComponent },
];
//# sourceMappingURL=routing.js.map