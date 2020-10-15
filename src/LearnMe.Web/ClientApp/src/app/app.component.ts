import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  private _opened: boolean = false;

  private _toggleSidebar() {
    this._opened = !this._opened;
  }
}




