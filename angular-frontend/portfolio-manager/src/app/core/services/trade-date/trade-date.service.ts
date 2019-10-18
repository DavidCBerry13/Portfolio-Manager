import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { TradeDate } from './trade-date';
import { environment } from 'src/environments/environment';
import { BaseService } from '../base.service';


@Injectable({
  providedIn: 'root'
})
export class TradeDateService extends BaseService {

  private TRADE_DATES_ENDPOINT = '/api/TradeDates';

  getTradeDates(): Observable<TradeDate[]> {
    return this.http.get<TradeDate[]>(environment.stockDataUrl + this.TRADE_DATES_ENDPOINT)
      .pipe(
        tap(_ => this.log('fetched trade dates')),
        catchError(this.handleError<TradeDate[]>('getTradeDates', []))
      );
  }


}
