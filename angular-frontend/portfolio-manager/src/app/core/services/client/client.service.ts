import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Client } from './client';
import { CreateClient } from './create-client';
import { environment } from 'src/environments/environment';
import { tap, catchError } from 'rxjs/operators';
import { BaseService } from '../base.service';


@Injectable({
  providedIn: 'root'
})
export class ClientService extends BaseService {

  private CLIENTS_ENDPOINT = '/api/clients';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };


  getClients(): Observable<Client[]> {
    return this.http.get<Client[]>(environment.brokerageAccountsUrl + this.CLIENTS_ENDPOINT)
      .pipe(
        tap(_ => this.log('fetched trade dates')),
        catchError(this.handleError<Client[]>('getClients', []))
      );
  }


  getClient(id: number): Observable<Client> {
    const url = environment.brokerageAccountsUrl + this.CLIENTS_ENDPOINT + '/' + id;
    return this.http.get<Client>(url)
      .pipe(
        tap(_ => this.log('fetched client ' + id)),
        catchError(this.handleError<Client>('getClient', null))
      );
  }


  addClient(createClient: CreateClient): Observable<Client> {
    const url = environment.brokerageAccountsUrl + this.CLIENTS_ENDPOINT;

    return this.http.post<Client>(url, createClient, this.httpOptions).pipe(
      tap((newClient: Client) => this.log(`added new client w/ id=${newClient.clientId}`)),
      catchError(this.handleError<Client>('addClient'))
    );
  }


}
