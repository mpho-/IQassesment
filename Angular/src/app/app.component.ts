import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthService } from './core/services/AuthService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {
  title = 'ACME-Industries';
  isSignedIn: boolean = false;

  constructor(private router: Router, private authService: AuthService) {
    
  }

  ngOnInit() {
    this.router.events.subscribe(event => {
      this.authService.signedIn().subscribe(res => {
          if(res) {
            this.isSignedIn = true;
          }
          else
          {
            this.isSignedIn = false;
          }
      });
    })
  }
}
