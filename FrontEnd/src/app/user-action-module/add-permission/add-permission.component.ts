import { Component, OnInit, Output } from '@angular/core';
import { AddPermissionService } from './permission-request.service';
import { PermissionModel } from '../user-requests/permission.model';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-add-permission',
  templateUrl: './add-permission.component.html',
  styleUrls: ['./add-permission.component.scss']
})
export class AddPermissionComponent implements OnInit {

  @Output() addedPermission = new EventEmitter<string>();
  servers: Array<string> = [];
  permissions: Array<string> = [];
  newPermission = new PermissionModel();
  constructor(private addPermissionService: AddPermissionService) { }

  ngOnInit() {
    this.addPermissionService.getServers().subscribe((servers) => {
      this.servers = servers;
    });
    this.addPermissionService.getAvailablePermissions().subscribe((permissions) => {
      this.permissions = permissions;
    })
  }

  add() {
   
    this.addPermissionService.add(this.newPermission).subscribe((servers) => {
    this.addedPermission.next("Added server" );
      this.newPermission = new PermissionModel();
    }, error => {
      this.addedPermission.next(error);
    });
  }

}
