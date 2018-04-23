import { Component, OnInit } from '@angular/core';
import { UserRequestService } from './user-requests.service';
import { PermissionModel } from './permission.model';

@Component({
  selector: 'app-user-requests',
  templateUrl: './user-requests.component.html',
  styleUrls: ['./user-requests.component.scss']
})
export class UserRequestsComponent implements OnInit {

  currentRequests: Array<PermissionModel> = [];
  userId: string;
  constructor(private userRequestService: UserRequestService) { }

  ngOnInit() {
      this.getUserPermissionRequests();
  }

  getUserPermissionRequests(){
      this.userRequestService.getCurrentRequests().subscribe((permissions) => {
        this.currentRequests = permissions;
      });
  }

  remove(id){
    this.userRequestService.remove(id).subscribe(()=>{
      this.getUserPermissionRequests();
    });
  }

  addedPermissionListener(info){
    console.log(info);
    this.getUserPermissionRequests();
  }

  requestClaimedListener(info){
    console.log(info);
    this.getUserPermissionRequests();
  }
}
