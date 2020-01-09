using DavidBerry.Framework.Functional;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.AppInterfaces
{
    public interface ITradeDateService
    {

        Result<List<TradeDate>> GetTradeDates();

        Result<TradeDate> GetTradeDate(DateTime date);

        Result<TradeDate> GetLatestTradeDate();


    }
}
