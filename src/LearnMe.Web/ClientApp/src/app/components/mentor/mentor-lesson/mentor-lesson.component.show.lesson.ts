import { Component, Inject, OnInit, Output, EventEmitter  } from '@angular/core';
import { Register } from '../../../models/Account/register';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';
import { Appointment } from 'src/app/services/calendar/calendar-service-ver-2';
import { Lesson } from '../../../Models/Lesson/lesson';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";

@Component({
  selector: 'app-mentor-lesson-show',
  templateUrl: 'mentor-lesson.component.show.lesson.html',
  styleUrls: ['./mentor-lesson.component.css']
})

export class ShowLessonDialog implements OnInit {

  // pupilForm: FormGroup;
  lessonData: Appointment;
  // succes: any;
  // fileStream: string;
  private _httpClient: HttpClient;
  _base: string;
  // errorFirstName: any;
  // errorLastName: any;
  // errorEmail: any;
  // errorPassword: any;
  // public progress: number;
  // public message: string;
  @Output() public onUploadFinished = new EventEmitter();



  constructor(
    private https: HttpService,
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    @Inject(MAT_DIALOG_DATA) data) {
    this._httpClient = http;
    this._base = baseUrl
    this.lessonData = {
      subject: '',
      startDate: new Date(),
      endDate: new Date(),
      description: '',
      isDone: false,
      isFreeSlot: false,
      calendarId: '',
      lesson: {
        title: '',
        lessonStatus: 0,
        calendarEventId: 0,
        relatedInvoiceId: 0
      } ,
      attendees: []
    };
  }

  ngOnInit() {

    this._httpClient.get<Appointment>this._baseUrl + 'api/calendareventsbydate/' + this.mentorLesson.calendarId).subscribe(result => {

    }
    
  }

  // public uploadFile = (files) => {
  //   if (files.length === 0) {
  //     return;
  //   }
  //   let fileToUpload = <File>files[0];
  //   const formData = new FormData();
  //   formData.append('file', fileToUpload, fileToUpload.name);
  //   this.http.post('api/upload', formData, {reportProgress: true, observe: 'events'})
  //     .subscribe(event => {
  //       if (event.type === HttpEventType.UploadProgress)
  //         this.progress = Math.round(100 * event.loaded / event.total);
  //       else if (event.type === HttpEventType.Response) {
  //         this.message = 'Upload success.';
  //         this.onUploadFinished.emit(event.body);
  //         this.fileStream = fileToUpload.name;
  //       }
  //     });
  // }

  // private initializeForm() {
  //   this.pupilForm = new FormGroup({
  //     'firstName': new FormControl(null),
  //     'lastName': new FormControl(null),
  //     'email': new FormControl(null, Validators.required),
  //     'password': new FormControl('Temp1!'),
  //     'confirmPassword': new FormControl('Temp1!'),
  //     'streetName': new FormControl(null),
  //     'houseNumber': new FormControl(null),
  //     'apartmentNumber': new FormControl(null),
  //     'street': new FormControl(null),
  //     'city': new FormControl(null),
  //     'country': new FormControl(null),
  //     'postcode': new FormControl(null),
  //     'imgPath': new FormControl('anonymusUser.png')
     
  //   });
  // }

  // public add = () => {
  //   const route: string = 'api/RegisterByMentor';
  //   this.https.post(route, this.pupilData)
  //     .subscribe((result) => {
  //       this.succes = true;
  //     },
  //       (error) => {
  //         if (error.status == "400") {
  //           this.errorEmail = error.error.errors.Email;
  //           this.errorLastName = error.error.errors.FirstName;
  //           this.errorFirstName = error.error.errors.LastName;
  //         }
  //         else if (error.status == "401") {
  //           this.errorPassword = error.error.password.errors;
  //         }
  //       });
  // }
  // onSubmit() {
  //   this.errorPassword = undefined;
  //   this.errorEmail = undefined;
  //   this.errorFirstName = undefined;
  //   this.errorLastName = undefined;
  //   this.succes = undefined;
  //   this.pupilData = this.pupilForm.value;
  //   if (this.fileStream != undefined){
  //     this.pupilData.imgPath = this.fileStream;
  //   }
  //   this.add();
  // }
}