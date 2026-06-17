import { Login } from './login/login';
import { Register } from './register/register';
import { Admin } from './admin/admin';
import { Client } from './client/client';
import { Freelancer } from './freelancer/freelancer';
import { authGuard } from './auth-guard';
import { MyProjects } from './client/my-projects/my-projects';
import { CreateProject } from './client/create-project/create-project';
import { Rankings } from './rankings/rankings';
export const routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' as const },

  { path: 'login', component: Login },
  { path: 'register', component: Register },

  {
    path: 'admin',
    component: Admin,
    canActivate: [authGuard],
    data: { role: 'Admin' }
  },
  {
    path: 'client',
    component: Client,
    canActivate: [authGuard],
    data: { role: 'Client' }
  },
  {
    path: 'freelancer',
    component: Freelancer,
    canActivate: [authGuard],
    data: { role: 'Freelancer' }
  },
  {
  path: 'create-project',
  component: CreateProject
  },
  {
    path: 'my-projects',
    component: MyProjects
  },
  {
    path: 'proposals',
    loadComponent: () =>
      import('./client/proposals/proposals')
        .then(m => m.Proposals)
  },
  {
    path: 'assigned-projects',
    loadComponent: () =>
      import('./freelancer/assigned-projects/assigned-projects')
        .then(m => m.AssignedProjects)
  },
  {
    path: 'review-project/:id',
    loadComponent: () =>
      import(
        './client/review-project/review-project'
      )
      .then(m => m.ReviewProject)
  },
  {
    path: 'rankings',
    component: Rankings
  },

  { path: '**', redirectTo: 'login' }
];