import { Component, OnInit } from '@angular/core';
import { TradeDateService } from 'src/app/core/services/trade-date/trade-date.service';
import { TradeDate } from 'src/app/core/services/trade-date/trade-date';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.css']
})
export class CreateAccountComponent implements OnInit {

  constructor(private tradeDateService: TradeDateService) { }

  tradeDates: TradeDate[];
  disabledDates: Date[] = [];

  openDate: Date;
  minOpenDate: Date;
  maxOpenDate: Date;

  ngOnInit() {
    this.tradeDateService.getTradeDates()
        .subscribe(tradeDates => this.handleTradeDatesResponse(tradeDates) );

  }

  handleTradeDatesResponse(tradeDates: TradeDate[]) {
    this.tradeDates = _.orderBy(tradeDates, ['tradeDate'], ['asc']);

    this.minOpenDate = new Date(this.tradeDates[0].tradeDate);
    this.maxOpenDate = new Date(this.tradeDates[this.tradeDates.length - 1].tradeDate);

    console.log(JSON.stringify(this.minOpenDate));
    console.log(JSON.stringify(new Date()));

    this.disabledDates = this.getNonTradingDays(this.minOpenDate, this.maxOpenDate, this.tradeDates);

  }

  getNonTradingDays(startDate: Date, endDate: Date, tradeDates: TradeDate[]): Date[] {
      const nonTradingDays: Date[] = [];

      const tradeDatesMap = _.keyBy(tradeDates, d => d.tradeDate);
      let workingDate = new Date(startDate);


      while (workingDate <= endDate) {
        if ( !tradeDatesMap[moment(workingDate).format('YYYY-MM-DD')] ) {
          nonTradingDays.push(workingDate);
        }
        workingDate = this.addDays(workingDate, 1);
      }

      return nonTradingDays;
  }


  addDays(date: Date, days: number): Date {
    const result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
  }


}
