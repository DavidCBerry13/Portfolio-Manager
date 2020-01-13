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
    public class SecurityRepository : ISecurityRepository
    {

        public SecurityRepository(String connectionString)
        {
            _connectionString = connectionString;
        }


        public static readonly String BASE_SQL =
            @"WITH SecurityTradeDates AS
              (
                  SELECT
                      Ticker,
	                  FirstTradeDate = MIN(TradeDate),
	                  LastTradeDate = MAX(TradeDate)
                  FROM SecurityPrices
                  GROUP BY Ticker
              )
              SELECT s.Ticker,
                  s.SecurityName AS Name,
                  s.SecurityTypeCode AS SecurityType,
	              sd.FirstTradeDate,
	              sd.LastTradeDate
              FROM Securities s
	          INNER JOIN SecurityTradeDates sd
	              ON s.Ticker = sd.Ticker";

        private readonly String _connectionString;




        public List<Security> GetSecurities()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Security>(BASE_SQL).ToList();
            }
        }

        public Maybe<Security> GetSecurity(string ticker)
        {
            String sql = $@"{BASE_SQL}
                WHERE
                    s.Ticker = @Ticker";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var security = connection.QueryFirstOrDefault<Security>(sql, new { Ticker = ticker });
                return Maybe.Create<Security>(security);
            }
        }

        public List<Security> GetSecurities(IEnumerable<string> tickers)
        {
            String sql = $@"{BASE_SQL}
                WHERE
                    s.Ticker IN @Tickers";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Security>(sql, new { Tickers = tickers }).ToList();
            }
        }
    }
}
