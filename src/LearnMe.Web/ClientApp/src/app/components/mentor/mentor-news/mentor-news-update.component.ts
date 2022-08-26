import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import { News } from '../../../models/Home/news';


@Component({
  selector: 'app-mentor-news-update',
  templateUrl: 'mentor-news-update.component.html',
  styleUrls: ['./mentor-news.component.css']
})

export class UpdateNewsDialog implements OnInit {

  newsForm: UntypedFormGroup;
  newsData: News;
  succes: any;
  description: any;
  news: News;
  private fileStream: string;
  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();



  constructor(private https: HttpService,
    @Inject('BASE_URL') baseUrl: string,
    private dialogRef: MatDialogRef<UpdateNewsDialog>,
    @Inject(MAT_DIALOG_DATA) data,
    private http: HttpClient) {   

    this.description = data.title; 
    this.newsData = {
      title: '',
      text: '',
      date: '',
      imgPath: '',
      id: 0,
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
    this.news = this.description;
    this.newsForm = new UntypedFormGroup({
        'id': new UntypedFormControl(this.news.id),
        'title': new UntypedFormControl(this.news.title),
        'text': new UntypedFormControl(this.news.text),
        'date': new UntypedFormControl(this.news.date),
        'imgPath': new UntypedFormControl('anonymusUser.png')
    });
  }

  public update = () => {
    const route: string = 'api/News';
    this.https.put(route, this.newsData)
      .subscribe((result) => {
        this.succes = true;
      },
        (error) => {
        });
  }

  onSubmit() {
    this.succes = undefined;
    this.newsData = this.newsForm.value;
    if (this.fileStream != undefined){
        this.newsData.imgPath = this.fileStream;
      }
    this.update();
  }
}