import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import { ProjectService }
from '../../services/project';

@Component({
  selector: 'app-assigned-projects',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './assigned-projects.html',
  styleUrl: './assigned-projects.css'
})
export class AssignedProjects implements OnInit {

  projects: any[] = [];

  constructor(
    private projectService: ProjectService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

    this.loadProjects();

  }

  loadProjects() {

    this.projectService
      .getAssignedProjects()
      .subscribe({

        next: (res: any) => {

          console.log(
            'ASSIGNED PROJECTS:',
            res
          );

          this.projects = [...res];

          console.log(
            'PROJECTS VARIABLE:',
            this.projects
          );

          this.cdr.detectChanges();
        },

        error: (err) => {

          console.log(err);

        }
      });
  }

}