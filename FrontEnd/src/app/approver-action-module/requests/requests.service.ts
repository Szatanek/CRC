import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { HttpClient, HttpParams } from '@angular/common/http';

import { WrapperRequestService } from "../../wrapper.request.service";
import { PermissionModel } from "../../user-action-module/user-requests/permission.model";
import { RequestModel, ProvisionedPermissionstModel } from "./request.model";
import { Base } from "../../../environments/base";

@Injectable()
export class RequestsService {
    baseUri = Base.baseUri;
    constructor(private http: HttpClient, private wrapperRequestService: WrapperRequestService) { }

    getRequests(): Observable<Array<RequestModel>> {
        return this.http.get<Array<RequestModel>>(`${this.baseUri}/request/getAllRequests`);
    }

    approve(requestedPermission: RequestModel) {
        let request = this.wrapperRequestService.approve(requestedPermission);
        return this.http.put<string>(`${this.baseUri}/request/approve/${request.id}`, requestedPermission);
    }

    reject(requestedPermission: RequestModel) {
        let request = this.wrapperRequestService.reject(requestedPermission);
        return this.http.put<string>(`${this.baseUri}/request/reject/${request.id}`, requestedPermission);
    }

    provisionRequest(request: RequestModel) {
        let provisionRequest = new ProvisionedPermissionstModel(request);
        return this.http.post<any>(`${this.baseUri}/provisioned/`, provisionRequest.provisionRequst);
    }
}