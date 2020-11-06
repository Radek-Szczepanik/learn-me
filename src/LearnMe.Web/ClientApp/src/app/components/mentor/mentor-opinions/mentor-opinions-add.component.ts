import { Component, Inject, OnInit, Output, EventEmitter  } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';
import { News } from "../../../models/Home/news";
import { Opinion } from 'src/app/models/Home/opinon';


@Component({
  selector: 'app-mentor-opinions-add',
  templateUrl: 'mentor-opinions-add.component.html',
  styleUrls: ['./mentor-opinions.component.css']
})

export class AddOpinionsDialog implements OnInit {

  opinionForm: FormGroup;
  opinionData: Opinion;
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
    this.opinionData = {
      author: '',
      title: '',
      text: '',
      date: '',
      rating: 0,
      id: 0,
    };
  }

  ngOnInit() {

    this.initializeForm();
    
  }

 private initializeForm() {
    this.opinionForm = new FormGroup({
      'author': new FormControl(null),
      'title': new FormControl('temp'),
      'text': new FormControl(null),
      'date': new FormControl(null),
      'rating': new FormControl(null),
    });
  }

  public add = () => {
    const route: string = 'api/Opinions';
    this.https.post(route, this.opinionData)
      .subscribe((result) => {
        this.succes = true;          
        });
  }
  onSubmit() {
    this.succes = undefined;
    this.opinionData = this.opinionForm.value;
    this.add();
  }
}