import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserPermissionsComponent } from './user-permissions/user-permissions.component';
import { UserRequestsComponent } from './user-requests/user-requests.component';
import { UserRequestService } from './user-requests/user-requests.service';
import { AddPermissionComponent } from './add-permission/add-permission.component';
import { AddPermissionService } from './add-permission/permission-request.service';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { RequestsService } from './user-permissions/requests.service';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
  ],
  declarations: [UserPermissionsComponent, UserRequestsComponent, AddPermissionComponent],
  exports: [UserPermissionsComponent, UserRequestsComponent],
  providers: [UserRequestService, AddPermissionService, RequestsService]
})
export class UserActionModuleModule { }
