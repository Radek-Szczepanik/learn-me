import { Injectable, ChangeDetectorRef } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Login } from '../models/Account/login';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { analyzeAndValidateNgModules } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class CrudService {

  constructor(public dialog: MatDialog,
    ) { }

  succes: any;

//   public addData(route: string)
//   {
//     return this.httpService.get(route);
//   }

//   public removeData (route: string, body: any)
//   {
//     return this.httpService.post(route, body);
//   }

  public updateData(name: any, dialogClassName: any)
  {
    const dialogConfig = new MatDialogConfig();


    dialogConfig.data = {
      id: 1,
      title: name
    };

    const dialogRef = this.dialog.open(dialogClassName, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
        
    });

  }
}
