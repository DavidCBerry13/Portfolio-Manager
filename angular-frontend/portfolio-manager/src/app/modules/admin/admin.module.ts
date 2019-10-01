import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { CreateAccountComponent } from './pages/create-account/create-account.component';
import { FormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  declarations: [CreateAccountComponent],
  imports: [
    CommonModule,
    FormsModule,
    AdminRoutingModule,
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot()
  ]
})
export class AdminModule { }
