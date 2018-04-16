

import { NgModule, Injectable } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserFormComponent } from './user-form/user-form.component';
import { RouterModule, Routes} from '@angular/router';
import { Observable } from 'rxjs/Observable';
@NgModule({
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    UserFormComponent
  ],
  declarations: [UserFormComponent],
  providers:[]
})
export class BaseFormModule { }
