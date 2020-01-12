using DavidBerry.Framework.Functional;
using FluentAssertions;
using Moq;
using Securities.Core.AppServices;
using Securities.Core.DataAccess;
using Securities.Core.Domain;
using Securities.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Securities.Core.Tests.AppServices
{
    public class SecurityPriceServiceTests
    {

        private static DateTime DATE_DEC_28_2016 => new DateTime(2016, 12, 28);
        private static DateTime DATE_DEC_29_2016 => new DateTime(2016, 12, 29);
        private static DateTime DATE_DEC_30_2016 => new DateTime(2016, 12, 30);

        private static DateTime DATE_DEC_31_2016 => new DateTime(2016, 12, 31);    // Not a trade date (Saturday)


        private static TradeDate TRADE_DATE_DEC_28_2016 => new TradeDate(DATE_DEC_28_2016, true, true, true);
        private static TradeDate TRADE_DATE_DEC_29_2016 => new TradeDate(DATE_DEC_29_2016, true, true, true);
        private static TradeDate TRADE_DATE_DEC_30_2016 => new TradeDate(DATE_DEC_30_2016, true, true, true);

        private static SecurityPrice PRICE_MSFT_DEC_29 => new SecurityPrice()
        {
            Ticker = "MSFT",
            Name = "Microsoft Corporation",
            SecurityType = "STOCK",
            TradeDate = DATE_DEC_29_2016,
            OpeningPrice = 60.7019m,
            ClosingPrice = 60.7405m,
            DailyLow = 60.5763m,
            DailyHigh = 61.0302m,
            Volume = 10250582,
            Change = -0.0869m,
            ChangePercent = -0.1430m
        };

        private static SecurityPrice PRICE_INTC_DEC_29 => new SecurityPrice()
        {
            Ticker = "INTC",
            Name = "Intel Corporation",
            SecurityType = "STOCK",
            TradeDate = DATE_DEC_29_2016,
            OpeningPrice = 34.8528m,
            ClosingPrice = 34.9960m,
            DailyLow = 34.7764m,
            DailyHigh = 35.0532m,
            Volume = 8447998,
            Change = 0.0286m,
            ChangePercent = 0.0820m
        };

        private static SecurityPrice PRICE_JPM_DEC_29 => new SecurityPrice()
        {
            Ticker = "JPM",
            Name = "JP Morgan Chase",
            SecurityType = "STOCK",
            TradeDate = DATE_DEC_29_2016,
            OpeningPrice = 83.3321m,
            ClosingPrice = 82.6680m,
            DailyLow = 81.8499m,
            DailyHigh = 83.4188m,
            Volume = 14689042,
            Change = -0.5871m,
            ChangePercent = -0.7050m
        };




        private static SecurityPrice PRICE_MSFT_DEC_30 => new SecurityPrice()
        {
            Ticker = "MSFT",
            Name = "Microsoft Corporation",
            SecurityType = "STOCK",
            TradeDate = DATE_DEC_30_2016,
            OpeningPrice = 60.7984m,
            ClosingPrice = 60.0066m,
            DailyLow = 59.9003m,
            DailyHigh = 60.8274m,
            Volume = 25579908,
            Change = -0.7339m,
            ChangePercent = -1.2080m
        };

        private static SecurityPrice PRICE_INTC_DEC_30 => new SecurityPrice()
        {
            Ticker = "INTC",
            Name = "Intel Corporation",
            SecurityType = "STOCK",
            TradeDate = DATE_DEC_30_2016,
            OpeningPrice = 35.1201m,
            ClosingPrice = 34.6237m,
            DailyLow = 34.5568m,
            DailyHigh = 35.1296m,
            Volume = 17467984,
            Change = -0.3723m,
            ChangePercent = -1.0640m
        };

        private static SecurityPrice PRICE_JPM_DEC_30 => new SecurityPrice()
        {
            Ticker = "JPM",
            Name = "JP Morgan Chase",
            SecurityType = "STOCK",
            TradeDate = DATE_DEC_30_2016,
            OpeningPrice = 82.8702m,
            ClosingPrice = 83.0530m,
            DailyLow = 82.4467m,
            DailyHigh = 83.1781m,
            Volume = 13617776,
            Change = 0.3850m,
            ChangePercent = 0.4660m
        };

        [Fact]
        public void GetSecurityPrices_WhenNoDateProvided_PricesForLatestPricingDateAreReturned()
        {
            // Arrange
            var tradeDateRepository = new Mock<ITradeDateRepository>(MockBehavior.Strict);
            tradeDateRepository.Setup(r => r.GetLatestTradeDate())
                .Returns(Maybe.Create<TradeDate>(TRADE_DATE_DEC_30_2016));
            var securityRepository = new Mock<ISecurityRepository>(MockBehavior.Strict);
            var securityPriceRepository = new Mock<ISecurityPriceRepository>(MockBehavior.Strict);
            securityPriceRepository.Setup(r => r.GetSecurityPrices(TRADE_DATE_DEC_30_2016))
                .Returns(new List<SecurityPrice>() { PRICE_MSFT_DEC_30, PRICE_INTC_DEC_30, PRICE_JPM_DEC_30 });

            // Act
            SecurityPriceService service = new SecurityPriceService(tradeDateRepository.Object,
                securityRepository.Object, securityPriceRepository.Object);
            var result = service.GetSecurityPrices((DateTime?)null);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Count.Should().Be(3);
            result.Value.Any(s => s.Ticker == "MSFT" && s.TradeDate == DATE_DEC_30_2016).Should().BeTrue();
            result.Value.Any(s => s.Ticker == "INTC" && s.TradeDate == DATE_DEC_30_2016).Should().BeTrue();
            result.Value.Any(s => s.Ticker == "JPM" && s.TradeDate == DATE_DEC_30_2016).Should().BeTrue();

            tradeDateRepository.Verify(r => r.GetLatestTradeDate(), Times.Once);
            tradeDateRepository.Verify(r => r.GetTradeDate(It.IsAny<DateTime>()), Times.Never);
            tradeDateRepository.Verify(r => r.GetTradeDates(), Times.Never);
            securityPriceRepository.Verify(r => r.GetSecurityPrices(TRADE_DATE_DEC_30_2016), Times.Once);
        }


        [Fact]
        public void GetSecurityPrices_WhenDateProvided_PricesForThatPricingDateAreReturned()
        {
            var tradeDateRepository = new Mock<ITradeDateRepository>(MockBehavior.Strict);
            tradeDateRepository.Setup(r => r.GetTradeDate(DATE_DEC_29_2016))
                .Returns(Maybe.Create<TradeDate>(TRADE_DATE_DEC_29_2016));
            var securityRepository = new Mock<ISecurityRepository>(MockBehavior.Strict);
            var securityPriceRepository = new Mock<ISecurityPriceRepository>(MockBehavior.Strict);
            securityPriceRepository.Setup(r => r.GetSecurityPrices(TRADE_DATE_DEC_29_2016))
                .Returns(new List<SecurityPrice>() { PRICE_MSFT_DEC_29, PRICE_INTC_DEC_29, PRICE_JPM_DEC_29 });

            // Act
            SecurityPriceService service = new SecurityPriceService(tradeDateRepository.Object,
                securityRepository.Object, securityPriceRepository.Object);
            var result = service.GetSecurityPrices(DATE_DEC_29_2016);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Count.Should().Be(3);
            result.Value.Any(s => s.Ticker == "MSFT" && s.TradeDate == DATE_DEC_29_2016).Should().BeTrue();
            result.Value.Any(s => s.Ticker == "INTC" && s.TradeDate == DATE_DEC_29_2016).Should().BeTrue();
            result.Value.Any(s => s.Ticker == "JPM" && s.TradeDate == DATE_DEC_29_2016).Should().BeTrue();

            tradeDateRepository.Verify(r => r.GetTradeDate(DATE_DEC_29_2016), Times.Once);
            tradeDateRepository.Verify(r => r.GetLatestTradeDate(), Times.Never);
            tradeDateRepository.Verify(r => r.GetTradeDates(), Times.Never);
            securityPriceRepository.Verify(r => r.GetSecurityPrices(TRADE_DATE_DEC_29_2016), Times.Once);
        }


        [Fact]
        public void GetSecurityPrices_WhenDateProvidedIsNotTradeDate_FailureResultReturnedWithInvalidTradeDateError()
        {
            var tradeDateRepository = new Mock<ITradeDateRepository>(MockBehavior.Strict);
            tradeDateRepository.Setup(r => r.GetTradeDate(DATE_DEC_31_2016))
                .Returns(Maybe.Create<TradeDate>(null));
            var securityRepository = new Mock<ISecurityRepository>(MockBehavior.Strict);
            var securityPriceRepository = new Mock<ISecurityPriceRepository>(MockBehavior.Strict);

            // Act
            SecurityPriceService service = new SecurityPriceService(tradeDateRepository.Object,
                securityRepository.Object, securityPriceRepository.Object);
            var result = service.GetSecurityPrices(DATE_DEC_31_2016);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().BeOfType<InvalidTradeDateError>();
            result.Error.As<InvalidTradeDateError>().Date.Should().Be(DATE_DEC_31_2016);

            tradeDateRepository.Verify(r => r.GetTradeDate(DATE_DEC_31_2016), Times.Once);
            tradeDateRepository.Verify(r => r.GetLatestTradeDate(), Times.Never);
            tradeDateRepository.Verify(r => r.GetTradeDates(), Times.Never);
            securityPriceRepository.Verify(r => r.GetSecurityPrices(It.IsAny<TradeDate>()), Times.Never);
        }


        [Fact]
        public void GetSecurityPrices_WhenValidDateProvidedButNoPricingData_FailureResultReturnedWithApplicationError()
        {
            var tradeDateRepository = new Mock<ITradeDateRepository>(MockBehavior.Strict);
            tradeDateRepository.Setup(r => r.GetTradeDate(DATE_DEC_29_2016))
                .Returns(Maybe.Create<TradeDate>(TRADE_DATE_DEC_29_2016));
            var securityRepository = new Mock<ISecurityRepository>(MockBehavior.Strict);
            var securityPriceRepository = new Mock<ISecurityPriceRepository>(MockBehavior.Strict);
            securityPriceRepository.Setup(r => r.GetSecurityPrices(TRADE_DATE_DEC_29_2016))
                .Returns(new List<SecurityPrice>());   // Empty list of prices = no pricing data

            // Act
            SecurityPriceService service = new SecurityPriceService(tradeDateRepository.Object,
                securityRepository.Object, securityPriceRepository.Object);
            var result = service.GetSecurityPrices(DATE_DEC_29_2016);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().BeOfType<ApplicationError>();
            result.Value.Should().BeNull();

            tradeDateRepository.Verify(r => r.GetTradeDate(DATE_DEC_29_2016), Times.Once);
            tradeDateRepository.Verify(r => r.GetLatestTradeDate(), Times.Never);
            tradeDateRepository.Verify(r => r.GetTradeDates(), Times.Never);
            securityPriceRepository.Verify(r => r.GetSecurityPrices(TRADE_DATE_DEC_29_2016), Times.Once);
        }
    }
}
