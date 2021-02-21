import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';
import { Messages } from './../../../models/Messages/messages';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Students } from '../../../models/Users/students';


@Component({
  selector: 'app-mentor-mail-add',
  templateUrl: './mentor-mail-add.component.html',
  styleUrls: ['./student-mail.component.css'],
})


export class AddStudentMailDialog implements OnInit {

  mailForm: FormGroup;
  mailData: Messages;
  success: any;
  private fileStream: string;
  private _http: HttpClient;
  private _base: string;
  public progress: number;
  public message: string;
  private values: any;
  private description: string[];
  public mailList: Students[];
  selected: Students;
  @Output() public onUploadFinished = new EventEmitter();



  constructor(private https: HttpService, private http: HttpClient, @Inject('BASE_URL') baseUrl: string, @Inject(MAT_DIALOG_DATA) data) {
    this._http = http;
    this._base = baseUrl
    this.mailData = {
      senderFirstName: 'string',
      senderLastName: 'string',
      senderEmail: 'string',
      recipientFirstName: 'string',
      recipientLastName: 'string',
      recipientEmail: '',
      title: '',
      senderId: 'string',
      recipientId: 'string',
      content: 'string',
      isRead: false,
      dateRead: null,
      dateSent: null,
      imgPath: '',

    };
    this.selected = {
      firstName: '',
      lastName: '',
      streetName: '',
      houseNumber: '',
      apartmentNumber: '',
      email: '',
      street: '',
      city: '',
      country: '',
      postcode: 0,
      imgPath: '',
    }
    
      this.description = data.userIdentity,
      this.mailList = data.mailList
   
    }

  ngOnInit() {
    this.initializeForm();
  }

  getIdentityForUser() {
    this._http.get<Students[]>(this._base + 'api/UserBasics?rolename=student').subscribe(result => {
      this.mailList = result;
    });
  }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.http.post('api/upload', formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
          this.fileStream = fileToUpload.name;
        }
      });
  }

  private initializeForm() {
    this.mailForm = new FormGroup({
      'recipientFirstName': new FormControl(null),
      'recipientLastName': new FormControl(null),
      'recipientEmail': new FormControl(null),
      'title': new FormControl(null),
      'content': new FormControl(null),
      'senderFirstName': new FormControl(this.description[2]),
      'senderLastName': new FormControl(this.description[3]),
      'senderEmail': new FormControl(this.description[1]),
    });
  }

  public add = () => {
    const route: string = 'api/Messages';
    this._http.post(route, this.mailData)
      .subscribe((result) => {
        this.success = true;
      });
  }
  onSubmit() {
    this.success = undefined;
    this.mailData = this.mailForm.value;
    //   if (this.fileStream != undefined){
    //     this.mailData.imgPath = this.fileStream;
    //   }
    this.add();
  }
}