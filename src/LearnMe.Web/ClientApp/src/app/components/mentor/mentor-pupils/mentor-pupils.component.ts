import { HttpClient } from '@angular/common/http';
import { Component, Inject, AfterContentChecked,} from '@angular/core';
import { Students } from '../../../models/Users/students';
import { AfterViewInit, ViewChild} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AddPupilDialog } from "./mentor-pupils.component.add.pupil";
import { DeletePupilDialog } from "./mentor-pupils.component.delete.pupil";
import { UpdatePupilDialog } from "./mentor-pupils.component.update.pupil";
import { CrudService } from "../../../services/crud.service"



@Component({
  selector: 'app-mentor-pupils',
  templateUrl: './mentor-pupils.component.html',
  styleUrls: ['./mentor-pupils.component.css']
})
export class MentorPupilsComponent implements AfterViewInit {

  displayedColumns: string[] = ['imgPath', 'firstName', 'lastName', 'email', 'streetName', 'houseNumber', 'apartmentNumber', 'city', 'postcode', 'country', 'actions'];

  dataSource: MatTableDataSource<Students>;
  _http: HttpClient;
  _baseUrl: string;
  _crud: CrudService;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string, 
    public dialog: MatDialog, 
    crud: CrudService) {
    this._http = http;
    this._baseUrl = baseUrl;
    this._crud = crud;
  }

  ngAfterViewInit() {
   this.getData();
  }

  getData(){
      this._http.get<Students[]>(this._baseUrl + 'api/UserBasics?rolename=student').subscribe(result => {
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

  addPupil() {
    const dialogRef = this.dialog.open(AddPupilDialog);

    dialogRef.afterClosed().subscribe(result => {
      this.ngAfterViewInit();
    });
  }
  deletePupil(email: string) {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.data = {
      id: 1,
      title: email
    };

    const dialogRef = this.dialog.open(DeletePupilDialog, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      this.ngAfterViewInit();
    });
  }

  updatePupil(user: Students) {

    const dialogConfig = new MatDialogConfig();


    dialogConfig.data = {
      id: 1,
      title: user
    };

    const dialogRef = this.dialog.open(UpdatePupilDialog, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
        this.getData();
    });
  }
}

