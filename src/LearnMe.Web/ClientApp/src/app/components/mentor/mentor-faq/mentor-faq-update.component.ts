import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { FormBuilder, UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { HttpService } from '../../../services/http.service';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { Questions } from "../../../models/Home/questions";

@Component({
  selector: 'app-mentor-faq-update',
  templateUrl: './mentor-faq-update.component.html',
  styleUrls: ['./mentor-faq.component.css']
})

export class UpdateFaqDialog implements OnInit {

  public faqForm: UntypedFormGroup;
  public faqData: Questions;
  public succes: any;
  private description: any;
  private faq: Questions;
  private progress: number;
  private message: string;
  private fileStream: string;
  @Output() public onUploadFinished = new EventEmitter();


  constructor(private https: HttpService,
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private dialogRef: MatDialogRef<UpdateFaqDialog>,
    @Inject(MAT_DIALOG_DATA) data) {

    this.description = data.title;
    this.faqData = {
        questionText: '',
        answerText: '',
        rating: 0,
        id: 0,
    };
  }

  isValidInput(fieldName): boolean {
    return this.faqForm.controls[fieldName].invalid &&
      (this.faqForm.controls[fieldName].dirty || this.faqForm.controls[fieldName].touched);
  }

  ngOnInit() {
    this.initializeForm();
  }

  private initializeForm() {
    this.faq = this.description;
    this.faqForm = new UntypedFormGroup({
        'questionText': new UntypedFormControl(this.faq.questionText, { validators: [Validators.required] }),
        'answerText': new UntypedFormControl(this.faq.answerText, { validators: [Validators.required] }),
        'rating': new UntypedFormControl(this.faq.rating),
        'id': new UntypedFormControl(this.faq.id)
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
    const route: string = 'api/Questions';
    this.https.put(route, this.faqData)
      .subscribe((result) => {
        this.succes = true;
      },
        (error) => {
        });
  }

  onSubmit() {
    this.succes = undefined;
    this.faqData = this.faqForm.value;
    this.update();
  }
}