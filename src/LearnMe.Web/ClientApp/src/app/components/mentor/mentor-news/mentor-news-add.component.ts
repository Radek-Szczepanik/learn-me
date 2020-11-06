import { Component, Inject, OnInit, Output, EventEmitter  } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';
import { News } from "../../../models/Home/news";


@Component({
  selector: 'app-mentor-news-add',
  templateUrl: 'mentor-news-add.component.html',
  styleUrls: ['./mentor-news.component.css']
})

export class AddNewsDialog implements OnInit {

  newsForm: FormGroup;
  newsData: News;
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
    this.newsForm = new FormGroup({
      'title': new FormControl(null),
      'text': new FormControl(null),
      'date': new FormControl(null),
      'imgPath': new FormControl('anonymusUser.png')
    });
  }

  public add = () => {
    const route: string = 'api/News';
    this.https.post(route, this.newsData)
      .subscribe((result) => {
        this.succes = true;          
        });
  }
  onSubmit() {
    this.succes = undefined;
    this.newsData = this.newsForm.value;
    if (this.fileStream != undefined){
      this.newsData.imgPath = this.fileStream;
    }
    this.add();
  }
}