import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { News } from '../../../models/Home/news';
import { AfterViewInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AddNewsDialog } from "./mentor-news-add.component";
import { DeleteNewsDialog } from "./mentor-news-delete.components";
import { UpdateNewsDialog } from "./mentor-news-update.component";

@Component({
  selector: 'app-mentor-news',
  templateUrl: './mentor-news.component.html',
  styleUrls: ['./mentor-news.component.css']
})
export class MentorNewsComponent {

  displayedColumns: string[] = ['imgPath', 'title', 'text', 'data', 'actions'];

  dataSource: MatTableDataSource<News>;
  _http: HttpClient;
  _baseUrl: string;
 


  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, public dialog: MatDialog) {
    this._http = http;
    this._baseUrl = baseUrl;
  }

  ngAfterViewInit() {
    this._http.get<News[]>(this._baseUrl + 'api/News').subscribe(result => {
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

  addNews() {
    
    const dialogConfig = new MatDialogConfig();

    dialogConfig.width = '50%';
    dialogConfig.height= '50%';

    const dialogRef = this.dialog.open(AddNewsDialog, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      this.ngAfterViewInit();
    });
  }
  deleteNews(id: number) {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      id: 1,
      title: id
    };
    
  
    const dialogRef = this.dialog.open(DeleteNewsDialog, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      this.ngAfterViewInit();
    });   
  }
  updateNews(name: any) {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      id: 1,
      title: name
    };
    
    dialogConfig.width = '50%';
    dialogConfig.height= '50%';

    const dialogRef = this.dialog.open(UpdateNewsDialog, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      this.ngAfterViewInit();
    });   
  }
}

