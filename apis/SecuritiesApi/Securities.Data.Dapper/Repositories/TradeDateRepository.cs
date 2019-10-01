using Dapper;
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
                  MonthEndDate AS IsMonthEnd,
                  QuarterEndDate AS IsQuarterEnd,
                  YearEndDate AS IsyearEnd
              FROM TradeDates";

        public static readonly String SINGLE_TRADE_DATE_SQL =
            $@"{BASE_SQL}
               WHERE TradeDate = @TradeDate";


        public static readonly String LATEST_TRADE_DATE_SQL =
            $@"{BASE_SQL}
               ORDER BY TradeDate DESC";


        private readonly String _connectionString;



        public TradeDate GetTradeDate(DateTime date)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<TradeDate>(SINGLE_TRADE_DATE_SQL, new { TradeDate = date.Date });
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



        public TradeDate GetLatestTradeDate()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<TradeDate>(LATEST_TRADE_DATE_SQL);
            }
        }

    }
}
