import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListClientsComponent } from './list-clients/list-clients.component';
import { CreateClientComponent } from './create-client/create-client.component';

const routes: Routes = [
  { path: 'clients', component: ListClientsComponent },
  { path: 'clients/create', component: CreateClientComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientsRoutingModule { }
