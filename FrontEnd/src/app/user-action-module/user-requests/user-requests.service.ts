import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { HttpClient, HttpParams } from '@angular/common/http';
import { PermissionModel } from "./permission.model";
import { WrapperRequestService } from "../../wrapper.request.service";
import { Base } from "../../../environments/base";

@Injectable()
export class UserRequestService {
    baseUri = Base.baseUri;
    constructor(private http: HttpClient, private wrapperRequestService: WrapperRequestService) { }

    getCurrentRequests(): Observable<Array<PermissionModel>> {
        let currentLogInUser = this.wrapperRequestService.getCurrentUser();
        return this.http.get<Array<PermissionModel>>(`${this.baseUri}/request/getMyRequests/${currentLogInUser.id}`);
    }

    remove(id: string) {
        return this.http.delete<string>(`${this.baseUri}/request/delete/${id}`);
    }
}