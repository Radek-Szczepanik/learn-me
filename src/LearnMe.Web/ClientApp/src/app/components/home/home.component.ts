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
  colspanVal: number;
  rowspanSecond: number;
  rowspanLogo: number;
  rowspanAbout: number;

  temp: boolean;
  temp2: boolean;

  opinions = [];
  
  constructor(config: NgbCarouselConfig, http: HttpClient, @Inject('BASE_URL') baseUrl: string, public dialog: MatDialog) {
    config.interval = 10000;
    config.keyboard = true;
    config.pauseOnHover = true;
    config.showNavigationArrows = false;
    this._http = http;
    this._baseUrl = baseUrl;
  }
  

    ngOnInit() {
    this._http.get<Opinion[]>(this._baseUrl + 'api/Opinions').subscribe(result => {
      this.getSlider(result) ;
      });
      
      this.getAbout();

      this.colspanVal = (window.innerWidth <= 1200) ? 10 : 5
      this.rowspanSecond = (window.innerWidth <= 1200) ? 10 : 5
      this.temp = (window.innerWidth <= 1200) ? false : true
      this.temp2 = (window.innerWidth <= 1200) ? true : false
      this.rowspanLogo = (window.innerWidth <= 1200) ? 7 : 5 
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
    }

    public getAbout() {
      if (window.screen.width <= 300)  this.rowspanAbout=28;
      else if (window.innerWidth <= 400)  this.rowspanAbout=20;
      else if (window.innerWidth <= 500)  this.rowspanAbout=16;
      else if (window.innerWidth <= 600)  this.rowspanAbout=12;
      else if (window.innerWidth <= 700)  this.rowspanAbout=10;
      else if (window.innerWidth <= 800)  this.rowspanAbout=9;
      else if (window.innerWidth <= 900)  this.rowspanAbout=8;
      else if (window.innerWidth >= 1000)  this.rowspanAbout=5;
    }
  
  onResize(event) {
    this.getAbout();

    if (event.target.innerWidth <= 1200)
    {
      this.colspanVal = 10
      this.rowspanSecond = 10
      this.temp = false 
      this.temp2 = true
      this.rowspanLogo = 7

    } else {
      this.colspanVal =  5
      this.rowspanSecond = 5
      this.temp = true
      this.temp2 = false
      this.rowspanLogo = 5
    }
  }


  
  
}


