import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SecuritiesModule } from './modules/securities/securities.module';
import { HomeModule } from './modules/home/home.module';
import { HttpClientModule, HTTP_INTERCEPTORS, /* other http imports */ } from '@angular/common/http';
import { AdminModule } from './modules/admin/admin.module';
import { ClientsModule } from './modules/clients/clients.module';
import { RequestCache, RequestCacheWithMap } from './core/shared/caching/request-cache.service';
import { CachingInterceptor } from './core/shared/caching/caching-interceptor';
import { environment } from 'src/environments/environment';

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
  providers: [
    { provide: RequestCache, useClass: RequestCacheWithMap },
    { provide: HTTP_INTERCEPTORS, useClass: CachingInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
