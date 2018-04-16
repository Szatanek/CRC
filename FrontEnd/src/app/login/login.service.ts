import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { of } from "rxjs/observable/of";
import { HttpClient } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { UserLoginModel } from "./user-login.model";
import { Subject } from "rxjs/Subject";

import { Base } from "../../environments/base";
import { RoleModel } from "../base-form-module/user-form/role.model";
@Injectable()
export class LoginService {
    baseUri = Base.baseUri;
    private currentLoginUser: UserLoginModel;
    constructor(private http: HttpClient) {
    }

    logInUser(login: string, password: string): Observable<number>{
        return this.http.get<any>(`${this.baseUri}/user/LogInUser/${login}/${password}`);
    }

    getUserBy(id: string): Observable<UserLoginModel> {
        return this.http.get<UserLoginModel>(`${this.baseUri}/user/GetUserById/${id}`);
    }

    changeLoging(user: UserLoginModel, isLogin: boolean) {
        user.isLogin = isLogin;
        return this.http.get<any>(`${this.baseUri}/user/UserLoginChange/${user.id}`);
    }

    logOut(login: string): Observable<UserLoginModel> {
        return this.http.get<any>(`${this.baseUri}/user/logOutUser/${login}`);
    }
    isUserLogIn(id: string): Observable<UserLoginModel> {
        return this.http.get<any>(`${this.baseUri}/user/UserLogged/${id}`);
    }

    setCurrentLoginUser(user: UserLoginModel): void {
        this.currentLoginUser = user;
    }

    getCurrentLoginUser(): UserLoginModel {
        return this.currentLoginUser;
    }

    getUserRoles(login: string): Observable<RoleModel> {
        return this.http.get<RoleModel>(`${this.baseUri}/user/GetUserRoles/${login}`);
    }

}