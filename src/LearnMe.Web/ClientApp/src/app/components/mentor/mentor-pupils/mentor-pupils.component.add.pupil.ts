import { Component, Inject, OnInit, Output, EventEmitter  } from '@angular/core';
import { Register } from '../../../models/Account/register';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';


@Component({
  selector: 'app-mentor-pupils-add',
  templateUrl: 'mentor-pupils.component.add.pupil.html',
  styleUrls: ['./mentor-pupils.component.css']
})

export class AddPupilDialog implements OnInit {

  pupilForm: FormGroup;
  pupilData: Register;
  succes: any;
  private fileStream: string;
  private _httpClient: HttpClient;
  private _base: string;
  private errorFirstName: any;
  private errorLastName: any;
  private errorEmail: any;
  private errorPassword: any;
  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();



  constructor(private https: HttpService, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._httpClient = http;
    this._base = baseUrl
    this.pupilData = {
      firstName: '',
      lastName: '',
      streetName: '',
      houseNumber: '',
      apartmentNumber: '',
      email: '',
      password: '',
      street: '',
      city: '',
      country: '',
      postcode: 0,
      confirmPassword: '',
      imgPath: '',
    };
  }

  ngOnInit() {

    this.initializeForm();
    
  }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.http.post('api/upload', formData, {reportProgress: true, observe: 'events'})
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
    this.pupilForm = new FormGroup({
      'firstName': new FormControl(null),
      'lastName': new FormControl(null),
      'email': new FormControl(null, Validators.required),
      'password': new FormControl('Temp1!'),
      'confirmPassword': new FormControl('Temp1!'),
      'streetName': new FormControl(null),
      'houseNumber': new FormControl(null),
      'apartmentNumber': new FormControl(null),
      'street': new FormControl(null),
      'city': new FormControl(null),
      'country': new FormControl(null),
      'postcode': new FormControl(null),
      'imgPath': new FormControl('anonymusUser.png')
     
    });
  }

  public add = () => {
    const route: string = 'api/RegisterByMentor';
    this.https.post(route, this.pupilData)
      .subscribe((result) => {
        this.succes = true;
      },
        (error) => {
          if (error.status == "400") {
            this.errorEmail = error.error.errors.Email;
            this.errorLastName = error.error.errors.FirstName;
            this.errorFirstName = error.error.errors.LastName;
          }
          else if (error.status == "401") {
            this.errorPassword = error.error.password.errors;
          }
        });
  }
  onSubmit() {
    this.errorPassword = undefined;
    this.errorEmail = undefined;
    this.errorFirstName = undefined;
    this.errorLastName = undefined;
    this.succes = undefined;
    this.pupilData = this.pupilForm.value;
    if (this.fileStream != undefined){
      this.pupilData.imgPath = this.fileStream;
    }
    this.add();
  }
}