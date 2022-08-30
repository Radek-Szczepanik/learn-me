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

  public kolp() {
    var tt = 1;
    var kol = 1;

    if (window.innerWidth >= 1680) {
      kol = window.innerWidth / 700
      tt = window.innerWidth / 550;
    } else if (window.innerWidth >= 1330) {
      kol = window.innerWidth / 550
      tt = window.innerWidth / 500;
    } else if (window.innerWidth >= 1200) {
      kol = window.innerWidth / 600
      tt = window.innerWidth / 450;
    } else if (window.innerWidth >= 1000) {
      kol = window.innerWidth / 340
      tt = window.innerWidth / 270;
    } else if (window.innerWidth >= 800) {
      kol = window.innerWidth / 310
      tt = window.innerWidth / 270;
    }  else if (window.innerWidth >= 600) {
      kol = window.innerWidth / 270;
      tt = window.innerWidth / 250;
    } else if (window.innerWidth >= 500) {
      kol = window.innerWidth / 255;
      tt = window.innerWidth / 250;
    }  else if (window.innerWidth >= 400) {
      kol = window.innerWidth / 235;
      tt = window.innerWidth / 220;
    } else if (window.innerWidth >= 300) {
      kol = window.innerWidth / 240;
      tt = window.innerWidth / 150;
    } else if (window.innerWidth >= 200) {
      kol = window.innerWidth / 220;
      tt = window.innerWidth / 140;
    } 

    console.log(window.innerWidth);

    this.innerWidth = `font-size: calc(0.5em * ${kol.toString()}); 
    line-height: calc(0.5em * ${tt.toString()});
    font-family: "Poppins", sans-serif;
    color: white;
    `;

    this.innerHeader = `font-size: calc(0.6em * ${kol.toString()}); 
    line-height: calc(0.5em * ${tt.toString()});
    font-weight: bold;
    text-align: center;
    margin-bottom: 1rem;
    `;

    if (window.innerWidth < 1200) this.isMobile = true;
    else this.isMobile = false;
    this.colspanVal = window.innerWidth < 1200 ? 1 : 1;
    this.colsVal = window.innerWidth < 1200 ? 1 : 2;

  }

  ngOnInit() {
    this._http.get<Opinion[]>(this._baseUrl + 'api/Opinions').subscribe(result => {
      this.getSlider(result);
    });
    this.kolp();
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
    this.kolp();
  };
}


