import { AfterViewInit, Component,ViewChild, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Questions } from '../../../models/Home/questions';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AddFaqDialog } from "./mentor-faq-add.component";
// import { DeleteOpinionsDialog } from "./mentor-opinions-delete.component";
import { UpdateFaqDialog } from "./mentor-faq-update.component";

@Component({
  selector: 'app-mentor-faq',
  templateUrl: './mentor-faq.component.html',
  styleUrls: ['./mentor-faq.component.css']
})
export class MentorFaqComponent implements AfterViewInit {

  displayedColumns: string[] = ['question', 'answer', 'rating', 'actions'];

  dataSource: MatTableDataSource<Questions>;
  _http: HttpClient;
  _baseUrl: string;
 
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, public dialog: MatDialog) {
    this._http = http;
    this._baseUrl = baseUrl;
  }

  ngAfterViewInit() {
    this._http.get<Questions[]>(this._baseUrl + 'api/Questions').subscribe(result => {
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

  addFaq() {
    
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '50%';
    const dialogRef = this.dialog.open(AddFaqDialog, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      this.ngAfterViewInit();
    });
  }
  
  // deleteOpinion(id: number) {

  //   const dialogConfig = new MatDialogConfig();

  //   dialogConfig.data = {
  //     id: 1,
  //     title: id
  //   };
    
  //   const dialogRef = this.dialog.open(DeleteOpinionsDialog, dialogConfig);

  //   dialogRef.afterClosed().subscribe(result => {
  //     this.ngAfterViewInit();
  //   });   
  // }
  
  updateFaq(name: any) {

    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '50%';
    dialogConfig.data = {
      id: 1,
      title: name
    };
    const dialogRef = this.dialog.open(UpdateFaqDialog, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      this.ngAfterViewInit();
    });   
  }
}