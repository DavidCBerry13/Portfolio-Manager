import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SecuritiesModule } from './modules/securities/securities.module';
import { HomeModule } from './modules/home/home.module';
import { HttpClientModule, /* other http imports */ } from '@angular/common/http';
import { AdminModule } from './modules/admin/admin.module';
import { ClientsModule } from './modules/clients/clients.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    SecuritiesModule,
    HomeModule,
    AdminModule,
    ClientsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
