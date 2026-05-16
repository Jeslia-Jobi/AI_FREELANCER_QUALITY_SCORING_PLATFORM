import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ProjectService } from '../../services/project';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-create-project',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './create-project.html',
  styleUrl: './create-project.css'
})
export class CreateProject {

  projectForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private projectService: ProjectService
  ) {

    this.projectForm = this.fb.group({
      title: [''],
      description: [''],
      budget: [''],
      deadline: [''],
      requirements: ['']
    });

  }

  submitProject() {

    console.log(this.projectForm.value);

    this.projectService
      .createProject(this.projectForm.value)
      .subscribe({
        next: (res) => {
          console.log(res);
          alert("Project Created");
        },
        error: (err) => {
          console.log(err);
        }
      });

  }

}
