import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { HttpClient } from '@angular/common/http';
import { LoginService } from "./login/login.service";
import { PermissionModel } from "./user-action-module/user-requests/permission.model";
import { Subject } from "rxjs";
import { RequestModel } from "./approver-action-module/requests/request.model";

@Injectable()
export class WrapperRequestService {
    constructor(private http: HttpClient, private loginService: LoginService) {

    }

    permission(request: PermissionModel): PermissionModel {
      //  request.login = this.loginService.getCurrentLoginUser().login;
      request.userId = this.loginService.getCurrentLoginUser().id;
        request.status = "In progress"
        return request;
    }

    approve(request: RequestModel): RequestModel {
        let updatedRequest = request;
        updatedRequest.status = "Approve";
        return updatedRequest;
    }

    reject(request: RequestModel): RequestModel {
        let updatedRequest = request;
        updatedRequest.status = "Reject";
        return updatedRequest;
    }

    getCurrentUser(){
        return this.loginService.getCurrentLoginUser();
    }

}