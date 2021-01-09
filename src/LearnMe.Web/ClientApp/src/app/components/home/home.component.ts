import { Component } from '@angular/core';
import { Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgbCarouselConfig } from '@ng-bootstrap/ng-bootstrap';
import { Slider } from '../../models/Home/slider'


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  providers: [NgbCarouselConfig] 
})

export class HomeComponent  {
 
  sliderList: Slider[] = new Array();
  slider: Slider;
  images = ['pyt1.png', 'pyt1_1.png', 'pyt2.png', 'pyt2_1.png', 'pyt3.png', 'pyt4.png', 'pyt5.png', 'pyt6.png', 'pyt7.png', 'pyt8.png' ].map((n) => `questions/${n}`);
  links = ['mentor/payment', 'mentor/payment', 'pyt2.png', 'pyt2_1.png', 'pyt3.png', 'pyt4.png', 'pyt5.png', 'pyt6.png', 'pyt7.png', 'pyt8.png' ]
  breakpoint: number;

  
  constructor(config: NgbCarouselConfig) {
    config.interval = 3000;
    config.keyboard = true;
    config.pauseOnHover = true;
  }
  
  ngOnInit() {
    for (let index = 0; index < this.images.length; index++) {
      this.slider = {
        images: '',
        links: '',
      }
      this.slider.images = this.images[index];
      this.slider.links = this.links[index];
      this.sliderList.push(this.slider);
    }
    this.breakpoint = (window.innerWidth <= 400) ? 6 : 12;
  }
  
  // onResize(event) {
  //   this.breakpoint = (event.target.innerWidth <= 400) ? 1 : 6;
  //}
  
}


