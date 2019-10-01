import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SecuritiesRoutingModule } from './securities-routing.module';
import { IndexComponent } from './pages/index/index.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [IndexComponent],
  imports: [
    CommonModule,
    SecuritiesRoutingModule,
    RouterModule
    
  ]
})
export class SecuritiesModule { }
