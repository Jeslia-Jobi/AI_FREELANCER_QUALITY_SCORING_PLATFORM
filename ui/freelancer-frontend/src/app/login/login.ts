import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  constructor(private http: HttpClient, private router: Router) {}

  user = {
    username: '',
    password: ''
  };

  login() {
      if (!this.user.username || !this.user.password) {
        alert("Enter username and password");
        return;
      }

  this.http.post<any>(
    'http://localhost:5029/api/auth/login',
    this.user
  ).subscribe({
    next: (res) => {

      localStorage.setItem('token', res.token);     
      const payload = JSON.parse(atob(res.token.split('.')[1]));
      //console.log("PAYLOAD:", payload);

      const role =
        payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ||
        payload["role"] ||
        payload["Role"];

      //console.log("ROLE:", role);

      if (role === 'Admin') {
        this.router.navigate(['/admin']);
      } else if (role === 'Client') {
        this.router.navigate(['/client']);
      } else if (role === 'Freelancer') {
        this.router.navigate(['/freelancer']);

      } else {
        alert("Invalid role");
      }
    },
    error: (err) => {
      console.error(err);
      alert("Invalid username or password");
    }
  });
}
}