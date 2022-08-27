import { Router } from '@angular/router';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';
import { Slider } from '../../models/Home/slider'
import { HttpClient } from '@angular/common/http';
import { Opinion } from '../../models/Home/opinon';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';

import { Component, Inject, ViewChild, AfterViewInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [NgbCarouselConfig] 
})

export class HomeComponent  {

  displayedColumns: string[] = ['author', 'text', 'rating', 'date', 'actions'];

  dataSource: MatTableDataSource<Opinion>;
  _http: HttpClient;
  _baseUrl: string;
 
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  sliderList: Slider[] = new Array();
  slider: Slider;
  breakpoint: number;
  opinions = [];
  
  constructor(config: NgbCarouselConfig, http: HttpClient, @Inject('BASE_URL') baseUrl: string, public dialog: MatDialog) {
    config.interval = 10000;
    config.keyboard = true;
    config.pauseOnHover = true;
    this._http = http;
    this._baseUrl = baseUrl;
  }
  

    ngOnInit() {
    this._http.get<Opinion[]>(this._baseUrl + 'api/Opinions').subscribe(result => {
      this.getSlider(result) ;
      });
    }
    
    public getSlider(opinions: any) {
      opinions.forEach(element => {
        this.slider = {
          author: '',
          rating: '',
          text: '',
          date:'',
        }
        this.slider.author = element.author;
        this.slider.rating = element.rating;
        this.slider.text = element.text;
        this.slider.date = element.date;
  
        this.sliderList.push(this.slider);
      });
      this.breakpoint = (window.innerWidth <= 800) ? 6 : 12

    }
  
  // onResize(event) {
  //   this.breakpoint = (event.target.innerWidth <= 400) ? 1 : 6;
  //}
  
}


