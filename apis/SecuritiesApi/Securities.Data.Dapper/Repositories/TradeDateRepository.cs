using Dapper;
using DavidBerry.Framework.Functional;
using Securities.Core.DataAccess;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Securities.Data.Dapper.Repositories
{
    public class TradeDateRepository : ITradeDateRepository
    {

        public TradeDateRepository(String connectionString)
        {
            _connectionString = connectionString;
        }

        public static readonly String BASE_SQL =
            @"SELECT
                  TradeDate AS Date,
                  IsMonthEnd,
                  IsQuarterEnd,
                  IsyearEnd
              FROM TradeDates";

        public static readonly String SINGLE_TRADE_DATE_SQL =
            $@"{BASE_SQL}
               WHERE TradeDate = @TradeDate";


        public static readonly String LATEST_TRADE_DATE_SQL =
            $@"{BASE_SQL}
               ORDER BY TradeDate DESC";


        private readonly String _connectionString;



        public Maybe<TradeDate> GetTradeDate(DateTime date)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tradeDate = connection.QueryFirstOrDefault<TradeDate>(SINGLE_TRADE_DATE_SQL, new { TradeDate = date.Date });
                return Maybe.Create<TradeDate>(tradeDate);
            }
        }

        public List<TradeDate> GetTradeDates()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<TradeDate>(BASE_SQL).ToList();
            }
        }



        public Maybe<TradeDate> GetLatestTradeDate()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tradeDate = connection.QueryFirstOrDefault<TradeDate>(LATEST_TRADE_DATE_SQL);
                return Maybe.Create<TradeDate>(tradeDate);
            }
        }

    }
}
