import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {

  const router = inject(Router);
  const token = localStorage.getItem('token');

  if (!token) {
    router.navigate(['/login']);
    return false;
  }

  // Decode token
  const payload = JSON.parse(atob(token.split('.')[1]));
  const userRole = payload.role;

  // Get expected role from route
  const expectedRole = route.data?.['role'];

  if (expectedRole && userRole !== expectedRole) {
    // wrong role
    router.navigate(['/login']);
    return false;
  }

  return true;
};