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
  images = ['pyt1.png', 'pyt1_1.png', 'pyt2.png', 'pyt2_1.png', 'pyt3.png', 'pyt4.png', 'pyt5.png', 'pyt6.png', 'pyt7.png', 'pyt8.png' ].map((n) => `questions/${n}`);
  links = ['mentor/payment', 'mentor/payment', 'pyt2.png', 'pyt2_1.png', 'pyt3.png', 'pyt4.png', 'pyt5.png', 'pyt6.png', 'pyt7.png', 'pyt8.png' ]
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
      console.log(result);
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
        console.log(this.sliderList);
      });
      this.breakpoint = (window.innerWidth <= 400) ? 6 : 12

    }
  
  // onResize(event) {
  //   this.breakpoint = (event.target.innerWidth <= 400) ? 1 : 6;
  //}
  
}


