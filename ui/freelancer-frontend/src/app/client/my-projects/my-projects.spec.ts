import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { MyProjects } from './my-projects';

describe('MyProjects', () => {
  let component: MyProjects;
  let fixture: ComponentFixture<MyProjects>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MyProjects],
      providers: [
      provideRouter([])
    ]
    }).compileComponents();

    fixture = TestBed.createComponent(MyProjects);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
