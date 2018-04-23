import { PermissionModel } from "../../user-action-module/user-requests/permission.model";

export class RequestModel extends PermissionModel {
}

export class ProvisionedPermissionstModel extends RequestModel {
    provisionRequst = new RequestModel();
    constructor(private request: RequestModel) {
        super();
        this.provisionRequst = request;
        this.provisionRequst.status = "Provisioned";
    }
}