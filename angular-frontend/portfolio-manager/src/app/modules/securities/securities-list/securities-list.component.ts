import { Component, OnInit } from '@angular/core';
import { SecurityPriceService } from 'src/app/core/services/security-price/security-price.service';
import { SecurityPrice } from 'src/app/core/services/security-price/security-price';
import { TradeDateService } from 'src/app/core/services/trade-date/trade-date.service';
import { TradeDate } from 'src/app/core/services/trade-date/trade-date';
import * as _ from 'lodash';

@Component({
  selector: 'app-securities-list',
  templateUrl: './securities-list.component.html',
  styleUrls: ['./securities-list.component.css']
})
export class SecuritiesListComponent implements OnInit {

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
