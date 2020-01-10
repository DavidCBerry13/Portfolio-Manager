using DavidBerry.Framework.Functional;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Securities.Core.DataAccess
{
    public interface ITradeDateRepository
    {

        List<TradeDate> GetTradeDates();

        Maybe<TradeDate> GetTradeDate(DateTime date);

        Maybe<TradeDate> GetLatestTradeDate();

    }
}
