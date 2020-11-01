import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { User } from '../../../models/Users/user';
import { AfterViewInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { DialogContentExampleDialog } from "./mentor-pupils.componebt.add.pupil";

@Component({
  selector: 'app-mentor-pupils',
  templateUrl: './mentor-pupils.component.html',
  styleUrls: ['./mentor-pupils.component.css']
})
export class MentorPupilsComponent implements AfterViewInit {

  displayedColumns: string[] = ['firstName', 'lastName', 'email'];
  dataSource: MatTableDataSource<User>;
  _http: HttpClient;
  _baseUrl: string;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, public dialog: MatDialog) {
    this._http = http;
    this._baseUrl = baseUrl;
  }

  ngAfterViewInit() {
    this._http.get<User[]>(this._baseUrl + 'api/UserBasics?rolename=student').subscribe(result => {
      this.dataSource = new MatTableDataSource(result);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openDialog() {
    const dialogRef = this.dialog.open(DialogContentExampleDialog);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
}

