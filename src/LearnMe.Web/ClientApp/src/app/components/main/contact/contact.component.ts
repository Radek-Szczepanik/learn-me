import { Component, HostListener, OnInit } from '@angular/core';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css'],
})
export class ContactComponent implements OnInit {

  public isMobile: boolean;


  constructor() { 
    
  }

  ngOnInit() {
    this.isMobile = window.innerWidth < 1200 ? true : false; 
  }
  
  @HostListener('window:resize')
  onResize() {
    this.isMobile = window.innerWidth < 1200 ? true : false; 
  };
  
}
