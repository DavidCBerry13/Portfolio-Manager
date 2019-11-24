import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClientsRoutingModule } from './clients-routing.module';
import { CreateClientComponent } from './create-client/create-client.component';
import { ListClientsComponent } from './list-clients/list-clients.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  declarations: [CreateClientComponent, ListClientsComponent],
  imports: [
    CommonModule,
    FormsModule,
    ClientsRoutingModule,
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
  ]
})
export class ClientsModule { }
