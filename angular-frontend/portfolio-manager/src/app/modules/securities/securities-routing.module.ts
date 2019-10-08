import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SecuritiesListComponent } from './securities-list/securities-list.component';

const routes: Routes = [
  { path: 'securities', component: SecuritiesListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SecuritiesRoutingModule { }
