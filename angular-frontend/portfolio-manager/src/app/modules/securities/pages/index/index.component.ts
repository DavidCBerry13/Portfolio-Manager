import { Component, OnInit } from '@angular/core';
import { SecurityPriceService } from 'src/app/core/services/security-price/security-price.service';
import { SecurityPrice } from 'src/app/core/services/security-price/security-price';
import { TradeDateService } from 'src/app/core/services/trade-date/trade-date.service';
import { TradeDate } from 'src/app/core/services/trade-date/trade-date';
import * as _ from 'lodash';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  tradeDates: TradeDate[];
  securities: SecurityPrice[];


  constructor(private tradeDateService: TradeDateService, private securitiesService: SecurityPriceService) { }

  ngOnInit() {

    this.tradeDateService.getTradeDates()
        .subscribe(tradeDates => this.tradeDates = _.orderBy(tradeDates, ['tradeDate'], ['desc']) );

    this.securitiesService.getSecurities()
        .subscribe(securities => this.securities = securities);
  }



  onTradeDateChanged(newTradeDate: string) {
      console.log('The new trade date is ' + newTradeDate);
  }

}
