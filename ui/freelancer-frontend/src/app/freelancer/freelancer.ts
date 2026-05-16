import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ProjectService } from '../services/project';
import { FreelancerProfileService } from '../services/freelancer-profile';

@Component({
  selector: 'app-freelancer',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './freelancer.html',
  styleUrl: './freelancer.css'
})
export class Freelancer implements OnInit {

  projects: any[] = [];

  showEditForm = false;

  profile: any = {
    bio: '',
    skills: '',
    experience: '',
    overallScore: 50,
    completedProjects: 0,
    rating: 0
  };

  constructor(
    private projectService: ProjectService,
    private profileService: FreelancerProfileService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

    this.loadProjects();

    this.loadProfile();
  }

  loadProjects() {

    this.projectService.getAllProjects()
      .subscribe({

        next: (res: any) => {

          this.projects = [...res];

          this.cdr.detectChanges();
        },

        error: (err) => {
          console.log(err);
        }
      });
  }

  loadProfile() {

    this.profileService.getProfile()
      .subscribe({

        next: (res: any) => {

          if (res) {

            this.profile = {
              bio: res.bio || '',
              skills: res.skills || '',
              experience: res.experience || '',
              overallScore: res.overallScore || 50,
              completedProjects: res.completedProjects || 0,
              rating: res.rating || 0
            };

            this.cdr.detectChanges();
          }

          console.log(this.profile);
        },

        error: (err) => {
          console.log(err);
        }
      });
  }

  saveProfile() {

    this.profileService.createProfile(this.profile)
      .subscribe({

        next: (res) => {

          console.log(res);

          alert('Profile Saved');

          this.showEditForm = false;
        },

        error: (err) => {
          console.log(err);
        }
      });
  }

}