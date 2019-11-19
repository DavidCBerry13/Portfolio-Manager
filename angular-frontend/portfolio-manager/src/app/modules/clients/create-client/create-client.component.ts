import { Component, OnInit } from '@angular/core';
import { CreateClient } from 'src/app/core/services/client/create-client';
import { ReferenceDataService } from 'src/app/core/services/reference-data/reference-data.service';
import { State } from 'src/app/core/services/reference-data/state';
import * as _ from 'lodash';
import { NgForm } from '@angular/forms';
import { ClientService } from 'src/app/core/services/client/client.service';

@Component({
  selector: 'app-create-client',
  templateUrl: './create-client.component.html',
  styleUrls: ['./create-client.component.css']
})
export class CreateClientComponent implements OnInit {

  constructor(private referenceDataService: ReferenceDataService, private clientService: ClientService) {
    this.client = new CreateClient();
  }

  states: State[];
  client: CreateClient;

  ngOnInit() {
    this.referenceDataService.getStates()
        .subscribe(states => this.states = _.orderBy(states, ['stateName'], ['asc']) );
  }


  onSubmit(form: NgForm) {
      console.log('submitting form');
      console.log(this.client);
      this.clientService.addClient(this.client)
      .subscribe(
          result => console.log('success', result),
          error => console.log('error', error)
      );
  }


}
