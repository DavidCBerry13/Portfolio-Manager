using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.AppInterfaces
{
    public interface ITradeDateService
    {

        List<TradeDate> GetTradeDates();

        TradeDate GetTradeDate(DateTime date);

        TradeDate GetLatestTradeDate();


    }
}
