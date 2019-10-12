import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SecuritiesRoutingModule } from './securities-routing.module';
import { RouterModule } from '@angular/router';
import { SecuritiesListComponent } from './securities-list/securities-list.component';

@NgModule({
  declarations: [SecuritiesListComponent],
  imports: [
    CommonModule,
    SecuritiesRoutingModule,
    RouterModule
  ]
})
export class SecuritiesModule { }
