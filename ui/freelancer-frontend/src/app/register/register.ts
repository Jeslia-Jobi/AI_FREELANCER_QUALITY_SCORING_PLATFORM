import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  constructor(private http: HttpClient, private router: Router) {}

  user = {
    username: '',
    email: '',
    passwordHash: '',
    role: 'Freelancer'
  };

  register() {
    this.http.post(
      'http://localhost:5029/api/auth/register',
      this.user,
      { responseType: 'text' }
    ).subscribe({
      next: (res) => {
        console.log(res);
        alert("Registered successfully");
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error("Error:", err);
        alert(err.error || "Registration failed");
      }
    });
  }
}