import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SecurityPrice } from './security-price';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BaseService } from '../base.service';


@Injectable({
  providedIn: 'root'
})
export class SecurityPriceService extends BaseService {

  private PRICES_ENDPOINT = '/api/Prices';


  getSecurities(): Observable<SecurityPrice[]> {
    return this.http.get<SecurityPrice[]>(environment.stockDataUrl + this.PRICES_ENDPOINT)
      .pipe(
        tap(_ => this.log('fetched securities')),
        catchError(this.handleError<SecurityPrice[]>('getSecurities()', []))
      );
  }


}
