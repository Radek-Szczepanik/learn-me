import { Component } from '@angular/core';
import { Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements  OnInit {

  public ofers: string[] = ['Matura', 'Certyfikaty i inne', 'Konwersacje', 'Sprawdź się'];
  public fotos: string[] = ['krecik.jpg', 'sasiedzi.jpg', 'uszatek.jpg', 'pingu.jpg'];


  constructor() {
    
  }

  
  ngOnInit() {
   
  }

  
}


