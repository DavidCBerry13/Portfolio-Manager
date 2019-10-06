import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClientsRoutingModule } from './clients-routing.module';
import { CreateClientComponent } from './create-client/create-client.component';
import { ListClientsComponent } from './list-clients/list-clients.component';

@NgModule({
  declarations: [CreateClientComponent, ListClientsComponent],
  imports: [
    CommonModule,
    FormsModule,
    ClientsRoutingModule
  ]
})
export class ClientsModule { }
