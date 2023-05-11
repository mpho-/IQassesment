import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { AuthService } from '../services/AuthService';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const allowedRoles = next.data['roles'] as string[];
    const currentUserRole = localStorage.getItem("role");
    const hasRole = allowedRoles.some(role => role == currentUserRole);
    if (hasRole) {
      return true;
    } else {
      this.router.navigate(['/']);
      return false;
    }
  }
}