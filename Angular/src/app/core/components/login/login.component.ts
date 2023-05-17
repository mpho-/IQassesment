import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/AuthService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less']
})
export class LoginComponent {
  model = {
    emailAddress: '',
    password: ''
  };

  constructor(private authService: AuthService, private router: Router) { 
    this.authService.signedIn()
    .subscribe(result => {
      if (result) {
        this.router.navigate(['/profile']);
      }
    });
  }

  onSubmit() {
    this.authService.login(this.model.emailAddress, this.model.password)
      .subscribe(result => {
        if (result) {
          this.router.navigate(['/profile']);
        }
      });
  }
}
