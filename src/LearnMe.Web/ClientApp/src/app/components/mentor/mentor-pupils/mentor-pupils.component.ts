import { HttpClient } from '@angular/common/http';
import { Component, Inject} from '@angular/core';
import { User } from '../../../models/Users/user';
import {AfterViewInit, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'app-mentor-pupils',
  templateUrl: './mentor-pupils.component.html',
  styleUrls: ['./mentor-pupils.component.css']
})
export class MentorPupilsComponent  {

  displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  dataSource: MatTableDataSource<User>;
  users: User[];
  a: any;

  // @ViewChild(MatPaginator) paginator: MatPaginator;
  // @ViewChild(MatSort) sort: MatSort;


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<User[]>(baseUrl + 'api/UserBasics?rolename=student').subscribe(result => {
      this.dataSource = new MatTableDataSource(result);;
    }, error => console.log(error));
  }
    
  
  // /** Builds and returns a new User. */
  // function createNewUser(id: number): UserData {
  //   const name = NAMES[Math.round(Math.random() * (NAMES.length - 1))] + ' ' +
  //       NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) + '.';
  
  //   return {
  //     id: id.toString(),
  //     name: name,
  //     progress: Math.round(Math.random() * 100).toString(),
  //     color: COLORS[Math.round(Math.random() * (COLORS.length - 1))]
  //}
 }
  
