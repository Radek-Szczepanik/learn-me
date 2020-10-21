import { HttpClient } from '@angular/common/http';
import { Component, Inject, AfterViewInit } from '@angular/core';
import { User } from '../../../Models/Users/user';

@Component({
  selector: 'app-mentor-pupils',
  templateUrl: './mentor-pupils.component.html',
  styleUrls: ['./mentor-pupils.component.css']
})
export class MentorPupilsComponent {

  users: User[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<User[]>(baseUrl + 'api/UserBasics').subscribe(result => {
      this.users = result;
    }, error => console.log(error));
  }
}
