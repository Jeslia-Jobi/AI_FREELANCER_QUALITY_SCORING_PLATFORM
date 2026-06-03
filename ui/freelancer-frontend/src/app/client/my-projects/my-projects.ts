import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectService } from '../../services/project';
import { RouterLink } from '@angular/router';
import { RecommendationService } from '../../services/recommendation';
@Component({
  selector: 'app-my-projects',
  imports: [CommonModule, RouterLink],
  templateUrl: './my-projects.html',
  styleUrl: './my-projects.css'
})
export class MyProjects implements OnInit {

  projects: any[] = [];
  recommendations: { [key: number]: any[] } = {};

  constructor(
    private projectService: ProjectService,
    private cdr: ChangeDetectorRef,
    private recommendationService: RecommendationService
  ) {}

  ngOnInit(): void {

    this.projectService.getMyProjects()
      .subscribe({
        next: (res: any) => {

          console.log("API RESPONSE:", res);

          this.projects = [...res];

          this.projects.forEach((project: any) => {

            this.loadRecommendations(project.projectId);

          });

          console.log("PROJECTS VARIABLE:", this.projects);

          this.cdr.detectChanges();
        },

        error: (err) => {
          console.log(err);
        }
      });

  }

  loadRecommendations(projectId: number) {

    this.recommendationService
      .getRecommendations(projectId)
      .subscribe({

        next: (data) => {

          console.log("RESPONSE:", data);

          this.recommendations = {
            ...this.recommendations,
            [projectId]: data
          };

          console.log("UPDATED:", this.recommendations);

          this.cdr.detectChanges();

        },

        error: (err) => {

          console.log("ERROR:", err);

        }

      });

  }

}
