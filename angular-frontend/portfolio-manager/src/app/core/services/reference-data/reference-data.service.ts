import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { tap, catchError } from 'rxjs/operators';
import { State } from './state';
import { BaseService } from '../base.service';

@Injectable({
  providedIn: 'root'
})
export class ReferenceDataService extends BaseService {

  private STATES_ENDPOINT = '/api/States';



  getStates(): Observable<State[]> {
    return this.http.get<State[]>(environment.brokerageAccountsUrl + this.STATES_ENDPOINT)
      .pipe(
        tap(_ => this.log('fetched trade dates')),
        catchError(this.handleError<State[]>('getTradeDates', []))
      );
  }


}
