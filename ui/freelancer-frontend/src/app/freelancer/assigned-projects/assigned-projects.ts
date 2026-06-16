import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import { ProjectService } from '../../services/project';
import { ReviewService } from '../../services/review';


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
  completedProjects: any[] = [];
  reviews: any[] = [];

  constructor(
    private projectService: ProjectService,
    private reviewService: ReviewService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

    this.loadProjects();
    this.loadCompletedProjects();
    this.loadReviews();

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

  loadCompletedProjects() {

    this.projectService
      .getCompletedProjects()
      .subscribe({

        next: (res: any) => {
          this.completedProjects = [...res];

          this.cdr.detectChanges();
        },

        error: (err) => {
          console.log(err);
        }

      });

  }

  requestCompletion(
    projectId: number
  ) {

    this.projectService
      .requestCompletion(projectId)
      .subscribe({

        next: () => {

          alert(
            'Completion request sent'
          );

          this.loadProjects();
        },

        error: (err) => {

          console.log(err);

        }
      });

  }

  loadReviews() {

    this.reviewService
      .getMyReviews()
      .subscribe({

        next: (res: any) => {
          this.reviews = res;
        },

        error: (err) => {
          console.log(err);
        }

      });

  }

}