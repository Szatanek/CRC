import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RequestsComponent } from './requests/requests.component';
import { RequestsService } from './requests/requests.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [RequestsComponent],
  exports:[RequestsComponent],
  providers:[RequestsService]
})
export class ApproverActionModuleModule { }
