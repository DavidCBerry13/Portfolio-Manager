import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SecurityPrice } from './security-price';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class SecurityPriceService {

  private PRICES_ENDPOINT = '/api/Prices';


  constructor(private http: HttpClient) { }


  getSecurities (): Observable<SecurityPrice[]> {
    return this.http.get<SecurityPrice[]>(environment.stockDataUrl + this.PRICES_ENDPOINT)
      .pipe(
        tap(_ => this.log('fetched securities')),
        catchError(this.handleError<SecurityPrice[]>('getSecurities()', []))
      );
  }




  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T> (operation = 'operation', result?: T) {
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
    //this.messageService.add(`HeroService: ${message}`);
  }

}
