import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { HttpClient } from '@angular/common/http';
import { PermissionModel } from "../user-requests/permission.model";
import { LoginService } from "../../login/login.service";
import { WrapperRequestService } from "../../wrapper.request.service";
import { Base } from "../../../environments/base";

@Injectable()
export class AddPermissionService {

    baseUri = Base.baseUri;
    constructor(private http: HttpClient, private wrapperService: WrapperRequestService) {

    }

    getServers(): Observable<Array<string>> {
        return this.http.get<Array<string>>(`${this.baseUri}/permission/getServerTypes`);
    }

    getAvailablePermissions(): Observable<Array<string>> {
        return this.http.get<Array<string>>(`${this.baseUri}/permission/getPermissionsTypes`);
    }

    add(request: PermissionModel) {
        let wrappedRequest = this.wrapperService.permission(request);
        return this.http.post<PermissionModel>(`${this.baseUri}/request/`, wrappedRequest);
    }


}