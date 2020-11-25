import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { Students } from '../../../models/Users/students';



@Component({
  selector: 'app-mentor-pupils-update',
  templateUrl: 'mentor-pupils.component.update.pupil.html',
  styleUrls: ['./mentor-pupils.component.css']
})

export class UpdatePupilDialog implements OnInit {

  pupilForm: FormGroup;
  pupilData: Students;
  succes: any;
  description: any;
  pupil: Students;
  public progress: number;
  public message: string;
  private fileStream: string;
  @Output() public onUploadFinished = new EventEmitter();


  constructor(private https: HttpService,
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private dialogRef: MatDialogRef<UpdatePupilDialog>,
    @Inject(MAT_DIALOG_DATA) data) {

    this.description = data.title;
    this.pupilData = {
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


    };
  }

  ngOnInit() {

    this.initializeForm();
  }

  private initializeForm() {
    this.pupil = this.description;
    this.pupilForm = new FormGroup({
      'firstName': new FormControl(this.pupil.firstName),
      'lastName': new FormControl(this.pupil.lastName),
      'email': new FormControl(this.pupil.email),
      'streetName': new FormControl(this.pupil.streetName),
      'houseNumber': new FormControl(this.pupil.houseNumber),
      'apartmentNumber': new FormControl(this.pupil.apartmentNumber),
      'street': new FormControl(this.pupil.street),
      'city': new FormControl(this.pupil.city),
      'country': new FormControl(this.pupil.country),
      'postcode': new FormControl(this.pupil.postcode),
      'imgPath': new FormControl(this.pupil.imgPath)

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

  public update = () => {
    const route: string = 'api/UserBasics';
    this.https.put(route, this.pupilData)
      .subscribe((result) => {
        this.succes = true;
      },
        (error) => {
        });
  }

  onSubmit() {
    this.succes = undefined;
    this.pupilData = this.pupilForm.value;
    if (this.fileStream != undefined) {
      this.pupilData.imgPath = this.fileStream;
    }
    this.update();
  }
}