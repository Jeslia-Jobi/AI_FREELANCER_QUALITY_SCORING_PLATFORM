import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { CreateProject } from './create-project';

describe('CreateProject', () => {
  let component: CreateProject;
  let fixture: ComponentFixture<CreateProject>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateProject],
      providers: [
        provideRouter([])
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(CreateProject);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
