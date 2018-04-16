import { Component, OnInit } from '@angular/core';
import { RequestsService } from './requests.service';

@Component({
  selector: 'app-user-permissions',
  templateUrl: './user-permissions.component.html',
  styleUrls: ['./user-permissions.component.scss']
})
export class UserPermissionsComponent implements OnInit {
  permissions = []
  constructor(private requestsService: RequestsService) { }

  ngOnInit() {
    this.requestsService.getPermissions().subscribe((permissions) => {
      this.permissions = permissions;
    })

  }

}
