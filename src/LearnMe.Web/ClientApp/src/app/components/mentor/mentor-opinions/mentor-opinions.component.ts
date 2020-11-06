import { HttpClient } from '@angular/common/http';
import { Component, Inject, ViewChild, AfterViewInit } from '@angular/core';
import { News } from '../../../models/Home/news';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AddOpinionsDialog } from "./mentor-opinions-add.component";
import { DeleteOpinionsDialog } from "./mentor-opinions-delete.component";
import { UpdateOpinionsDialog } from "./mentor-opinions-update.component";

@Component({
  selector: 'app-mentor-opinions',
  templateUrl: './mentor-opinions.component.html',
  styleUrls: ['./mentor-opinions.component.css']
})
export class MentorOpinionsComponent implements AfterViewInit {

  displayedColumns: string[] = ['author', 'text', 'rating', 'date', 'actions'];

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
    this._http.get<News[]>(this._baseUrl + 'api/Opinions').subscribe(result => {
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

  addOpinion() {
    
    const dialogConfig = new MatDialogConfig();

    // dialogConfig.width = '50%';
    // dialogConfig.height= '50%';

    const dialogRef = this.dialog.open(AddOpinionsDialog, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      this.ngAfterViewInit();
    });
  }
  
  deleteOpinion(id: number) {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      id: 1,
      title: id
    };
    
    const dialogRef = this.dialog.open(DeleteOpinionsDialog, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      this.ngAfterViewInit();
    });   
  }
  
  updateOpinion(name: any) {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      id: 1,
      title: name
    };
    
    // dialogConfig.width = '50%';
    // dialogConfig.height= '50%';

    const dialogRef = this.dialog.open(UpdateOpinionsDialog, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      this.ngAfterViewInit();
    });   
  }
}
