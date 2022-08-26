import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { UntypedFormBuilder, UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';
import { Questions } from "../../../models/Home/questions";


@Component({
  selector: 'app-mentor-faq-add',
  templateUrl: 'mentor-faq-add.component.html',
  styleUrls: ['./mentor-faq.component.css']
})

export class AddFaqDialog implements OnInit {

  questionsForm: UntypedFormGroup;
  questionsData: Questions;
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

  isValidInput(fieldName): boolean {
    return this.questionsForm.controls[fieldName].invalid &&
      (this.questionsForm.controls[fieldName].dirty || this.questionsForm.controls[fieldName].touched);
  }

  constructor(private https: HttpService, private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private fb: UntypedFormBuilder) {
    this._httpClient = http;
    this._base = baseUrl
    this.questionsData = {
      questionText: '',
      answerText: '',
      rating: 0,
      id: 0,
    };
  }

  ngOnInit() {

    this.initializeForm();

  }

  private initializeForm() {
    this.questionsForm = new UntypedFormGroup({
      'questionText': new UntypedFormControl(null, { validators: [Validators.required] }),
      'answerText': new UntypedFormControl(null, { validators: [Validators.required] }),
      'rating': new UntypedFormControl(null),
    });
  }

  public add = () => {
    const route: string = 'api/Questions';
    this.https.post(route, this.questionsData)
      .subscribe((result) => {
        this.succes = true;
      });
  }
  onSubmit() {
    this.succes = undefined;
    this.questionsData = this.questionsForm.value;
    this.add();
  }
}