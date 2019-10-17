import { Component, OnInit } from '@angular/core';
import { ClientService } from 'src/app/core/services/client/client.service';
import { Client } from 'src/app/core/services/client/client';
import * as _ from 'lodash';

@Component({
  selector: 'app-list-clients',
  templateUrl: './list-clients.component.html',
  styleUrls: ['./list-clients.component.css']
})
export class ListClientsComponent implements OnInit {


  clients: Client[];
  lodash: any;

  constructor(private clientService: ClientService) {
    this.lodash = _;
  }

  ngOnInit() {
    this.clientService.getClients()
    .subscribe(clients => this.clients = _.orderBy(clients, ['lastName', 'firstName'], ['desc', 'desc']) );
  }

}
