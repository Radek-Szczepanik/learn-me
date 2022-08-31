import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';
import { Slider } from '../../models/Home/slider'
import { HttpClient } from '@angular/common/http';
import { Opinion } from '../../models/Home/opinon';
import { MatDialog } from '@angular/material/dialog';

import { Component, Inject, HostListener } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [NgbCarouselConfig]
})

export class HomeComponent {

  displayedColumns: string[] = ['author', 'text', 'rating', 'date', 'actions'];

  dataSource: MatTableDataSource<Opinion>;
  _http: HttpClient;
  _baseUrl: string;

  sliderList: Slider[] = new Array();
  slider: Slider;
  public innerWidth: string;
  public innerHeader: string;
  public isMobile: boolean;
  public colspanVal: number;
  public colsVal: number;
  opinions = [];

  constructor(config: NgbCarouselConfig, http: HttpClient, @Inject('BASE_URL') baseUrl: string, public dialog: MatDialog) {
    config.interval = 10000;
    config.keyboard = true;
    config.pauseOnHover = true;
    config.showNavigationArrows = false;
    this._http = http;
    this._baseUrl = baseUrl;
  }

  public getResponsiveLayout() {
    var textWidth = 1;
    var headerWidth = 1;

    if (window.innerWidth === 412) {
      textWidth  = window.innerWidth / 283
      headerWidth  = window.innerWidth / 120;
    } else  if (window.innerWidth === 360) {
      textWidth  = window.innerWidth / 290
      headerWidth  = window.innerWidth / 100;
    } else if (window.innerWidth >= 1680) {
        textWidth  = window.innerWidth / 700
        headerWidth  = window.innerWidth / 550;
    } else if (window.innerWidth >= 1330) {
      textWidth  = window.innerWidth / 550
      headerWidth  = window.innerWidth / 500;
    } else if (window.innerWidth >= 1200) {
      textWidth  = window.innerWidth / 600
      headerWidth  = window.innerWidth / 450;
    } else if (window.innerWidth >= 1000) {
      textWidth = window.innerWidth / 340
      headerWidth  = window.innerWidth / 270;
    } else if (window.innerWidth >= 800) {
      textWidth = window.innerWidth / 295
      headerWidth  = window.innerWidth / 270;
    } else if (window.innerWidth >= 700) {
        textWidth = window.innerWidth / 275
        headerWidth  = window.innerWidth / 250;
    }  else if (window.innerWidth >= 600) {
      textWidth  = window.innerWidth / 275;
      headerWidth  = window.innerWidth / 220;
    } else if (window.innerWidth >= 500) {
      textWidth  = window.innerWidth / 255;
      headerWidth  = window.innerWidth / 220;
    }  else if (window.innerWidth >= 400) {
      textWidth  = window.innerWidth / 250;
      headerWidth  = window.innerWidth / 180;
    } else if (window.innerWidth >= 300) {
      textWidth  = window.innerWidth / 260;
      headerWidth = window.innerWidth / 140;
    } else if (window.innerWidth >= 200) {
      textWidth = window.innerWidth / 250;
      headerWidth  = window.innerWidth / 125;
    } 

    this.innerWidth = `font-size: calc(0.5em * ${textWidth .toString()}); 
    line-height: calc(0.5em * ${headerWidth .toString()});
    font-family: "Poppins", sans-serif;
    color: white;
    `;

    this.innerHeader = `font-size: calc(0.6em * ${textWidth .toString()}); 
    line-height: calc(0.5em * ${headerWidth .toString()});
    font-weight: bold;
    text-align: center;
    margin-bottom: 1rem;
    `;

    if (window.innerWidth < 1200) this.isMobile = true;
    else this.isMobile = false;
    this.colsVal = window.innerWidth < 1200 ? 1 : 2;

  }

  public async ngOnInit() {
    await this._http.get<Opinion[]>(this._baseUrl + 'api/Opinions').subscribe(result => {
      this.getSlider(result);
    });
    this.getResponsiveLayout();
    this.colspanVal = 1;
  }

  public getSlider(opinions: any) {
    opinions.forEach(element => {
      this.slider = {
        author: '',
        rating: '',
        text: '',
        date: '',
      }
      this.slider.author = element.author;
      this.slider.rating = element.rating;
      this.slider.text = element.text;
      this.slider.date = element.date;
      this.sliderList.push(this.slider);
    });
  }


  @HostListener('window:resize')
  onResize() {
    this.getResponsiveLayout();
  };
}


