import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { map, Observable } from "rxjs";
import { AuthService } from "../services/AuthService";

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) { }
    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.isSignedIn();
    }
    isSignedIn(): Observable<boolean> {
        return this.authService.signedIn().pipe(
            map((isSignedIn) => {
                if (!isSignedIn) {
                    this.router.navigate(['']);
                    return false;
                }
                return true;
            }));
    }
}