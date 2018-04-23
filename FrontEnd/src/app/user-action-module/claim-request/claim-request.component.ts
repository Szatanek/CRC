import { Component, Output, Input } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { ClaimRequestService } from './claim-request.service';

@Component({
  selector: 'app-claim-request',
  templateUrl: './claim-request.component.html',
  styleUrls: ['./claim-request.component.scss']
})
export class ClaimRequestComponent {

  @Output() requestClaimed = new EventEmitter<string>();
  @Input() requestId: number;

  public reason: string;

  constructor(private claimRequestService: ClaimRequestService) {
    this.reason = '';
   }

  claim() {
    this.claimRequestService
      .claim(this.requestId, this.reason)
      .subscribe((response) => {
        this.requestClaimed.next("Request claimed");
        this.reason = '';
      }, error => {
        this.requestClaimed.next(error);
    });
  }

}
