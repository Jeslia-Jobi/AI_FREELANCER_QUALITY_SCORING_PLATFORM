import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ProjectService } from '../services/project';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-client',
  imports: [CommonModule, RouterLink],
  templateUrl: './client.html',
  styleUrl: './client.css'
})
export class Client implements OnInit {

  projects: any[] = [];

  totalProjects = 0;
  openProjects = 0;
  completedProjects = 0;

  username = '';

  constructor(private projectService: ProjectService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {

    const token = localStorage.getItem('token');

    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));
      this.username = payload.unique_name;
    }

    this.loadProjects();
  }

  loadProjects() {

    this.projectService
      .getMyProjects()
      .subscribe({
        next: (res: any) => {

          console.log("CLIENT DASHBOARD PROJECTS:", res);

          this.projects = [...res];

          this.totalProjects = this.projects.length;

          this.openProjects = this.projects.filter(
            p => p.status === 'Open'
          ).length;

          this.completedProjects = this.projects.filter(
            p => p.status === 'Completed'
          ).length;
          
          this.cdr.detectChanges();
        },

        error: (err) => {
          console.log(err);
        }
      });
  }
}