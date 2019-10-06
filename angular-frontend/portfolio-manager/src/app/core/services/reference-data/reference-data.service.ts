import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { tap, catchError } from 'rxjs/operators';
import { State } from './state';

@Injectable({
  providedIn: 'root'
})
export class ReferenceDataService {

  private STATES_ENDPOINT = '/api/States';

  constructor(private http: HttpClient) { }


  getStates(): Observable<State[]> {
    return this.http.get<State[]>(environment.brokerageAccountsUrl + this.STATES_ENDPOINT)
      .pipe(
        tap(_ => this.log('fetched trade dates')),
        catchError(this.handleError<State[]>('getTradeDates', []))
      );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a HeroService message with the MessageService */
  private log(message: string) {
    // this.messageService.add(`HeroService: ${message}`);
  }
}
