import { Component } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { ResolveStart, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  model: LoginRequest;

  constructor(private authService: AuthService, private cookieService: CookieService,
            private router: Router){
    this.model = {
      email: '',
      password: ''
    }
  }

  onFormSubmit():void{
    this.authService.login(this.model)
    .subscribe({
      next: (response) => {
        //Set auth cookie 
        this.cookieService.set('Authorization', `Bearer ${response.token}`, undefined, '/', undefined, true, 'Strict');

        //Set user

        this.authService.setUser({
          email: response.email,
          roles: response.roles
        });

        //Redirect to home page
        this.router.navigateByUrl('/');
      }
    });
  }
}
