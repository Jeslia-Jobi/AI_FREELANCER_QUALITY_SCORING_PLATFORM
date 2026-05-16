import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectService } from '../../services/project';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-my-projects',
  imports: [CommonModule, RouterLink],
  templateUrl: './my-projects.html',
  styleUrl: './my-projects.css'
})
export class MyProjects implements OnInit {

  projects: any[] = [];

  constructor(
    private projectService: ProjectService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

    this.projectService.getMyProjects()
      .subscribe({
        next: (res: any) => {

          console.log("API RESPONSE:", res);

          this.projects = [...res];

          console.log("PROJECTS VARIABLE:", this.projects);

          this.cdr.detectChanges();
        },

        error: (err) => {
          console.log(err);
        }
      });

  }

}
