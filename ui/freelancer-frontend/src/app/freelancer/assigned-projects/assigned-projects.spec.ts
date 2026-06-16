import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignedProjects } from './assigned-projects';

describe('AssignedProjects', () => {
  let component: AssignedProjects;
  let fixture: ComponentFixture<AssignedProjects>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssignedProjects],
    }).compileComponents();

    fixture = TestBed.createComponent(AssignedProjects);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
