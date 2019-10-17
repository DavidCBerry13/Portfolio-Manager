import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Client } from './client';
import { environment } from 'src/environments/environment';
import { tap, catchError } from 'rxjs/operators';
import { BaseService } from '../base.service';


@Injectable({
  providedIn: 'root'
})
export class ClientService extends BaseService {

  private CLIENTS_ENDPOINT = '/api/clients';


  getClients(): Observable<Client[]> {
    return this.http.get<Client[]>(environment.brokerageAccountsUrl + this.CLIENTS_ENDPOINT)
      .pipe(
        tap(_ => this.log('fetched trade dates')),
        catchError(this.handleError<Client[]>('getClients', []))
      );
  }


}
