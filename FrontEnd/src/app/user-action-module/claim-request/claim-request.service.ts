import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Base } from "../../../environments/base";

@Injectable()
export class ClaimRequestService {

    baseUri = Base.baseUri;
    constructor(private http: HttpClient) {

    }

    claim(requestId: number, reason: string): Observable<Object> {
        const claimRequestUrl = `${this.baseUri}/request/claim/${requestId}`;
        let header = new HttpHeaders({'Content-Type' : 'application/json'});
        return this.http.put(claimRequestUrl, `"${reason}"`, { headers: header });
    }
}