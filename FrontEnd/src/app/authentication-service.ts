import { LoginService } from "./login/login.service";
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
@Injectable()
export class Login {
    constructor(private loginService: LoginService, private router: Router){}
    
    whenUserIsNotLogin(id: string) {
        return this.loginService.isUserLogIn(id).map(user => {
            if (user.isLogin) {
                this.loginService.setCurrentLoginUser(user);
                return true;
            }
            else {
                this.router.navigate(['/login']);
                return false;
            }
        });
    }
}

@Injectable()
export class CanActivateUser implements CanActivate {
    constructor(private login: Login) { }

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot,

    ): Observable<boolean> | Promise<boolean> | boolean {
        return this.login.whenUserIsNotLogin(route.params.userId);
    }
}