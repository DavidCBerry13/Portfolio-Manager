using DavidBerry.Framework.Functional;
using FluentAssertions;
using Moq;
using Securities.Core.AppServices;
using Securities.Core.DataAccess;
using Securities.Core.Domain;
using Securities.Core.Errors;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Securities.Core.Tests.AppServices
{
    public class TradeDateServiceTests
    {

        [Fact]
        public void GetLatestTradeDate_WhenRepositoryReturnsLatestTradeDate_ItIsReturned()
        {
            // Arrange
            Mock<ITradeDateRepository> tradeDateRepository = new Mock<ITradeDateRepository>();
            TradeDate tradeDate = new TradeDate(new DateTime(2018, 12, 31), true, true, true);
            tradeDateRepository.Setup(r => r.GetLatestTradeDate())
                .Returns(Maybe.Create<TradeDate>(tradeDate));

            // Act
            TradeDateService service = new TradeDateService(tradeDateRepository.Object);
            var result = service.GetLatestTradeDate();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(tradeDate);
        }


        [Fact]
        public void GetLatestTradeDate_WhenRepositoryHasNoTradeDates_ApplicationErrorFailureIsReturned()
        {
            // Arrange
            Mock<ITradeDateRepository> tradeDateRepository = new Mock<ITradeDateRepository>();
            tradeDateRepository.Setup(r => r.GetLatestTradeDate())
                .Returns(Maybe.Create<TradeDate>(null));

            // Act
            TradeDateService service = new TradeDateService(tradeDateRepository.Object);
            var result = service.GetLatestTradeDate();

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Error.Should().BeOfType<ApplicationError>();
        }

        [Fact]
        public void GetTradeDate_WhenTradeDateExists_ReturnsTradeDate()
        {
            // Arrange
            DateTime date = new DateTime(2018, 12, 31);

            Mock<ITradeDateRepository> tradeDateRepository = new Mock<ITradeDateRepository>();
            TradeDate tradeDate = new TradeDate(new DateTime(2018, 12, 31), true, true, true);
            tradeDateRepository.Setup(r => r.GetTradeDate(date))
                .Returns(Maybe.Create<TradeDate>(tradeDate));

            // Act
            TradeDateService service = new TradeDateService(tradeDateRepository.Object);
            var result = service.GetTradeDate(date);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(tradeDate);
        }


        [Fact]
        public void GetTradeDate_WhenTradeDateDoesNotExist_ReturnsInvalidTradeDateErrorResult()
        {
            // Arrange
            DateTime date = new DateTime(2018, 12, 31);

            Mock<ITradeDateRepository> tradeDateRepository = new Mock<ITradeDateRepository>();
            tradeDateRepository.Setup(r => r.GetTradeDate(date))
                .Returns(Maybe.Create<TradeDate>(null));

            // Act
            TradeDateService service = new TradeDateService(tradeDateRepository.Object);
            var result = service.GetTradeDate(date);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().BeOfType<InvalidTradeDateError>();
            ((InvalidTradeDateError)result.Error).Date.Should().Be(date);
            result.Value.Should().BeNull();
        }

        [Fact]
        public void GetTradeDates_WhenTradeDatesExist_ReturnsListOfTradeDatesInSuccessfulResult()
        {
            // Arrange
            Mock<ITradeDateRepository> tradeDateRepository = new Mock<ITradeDateRepository>();
            List<TradeDate> tradeDates = new List<TradeDate>()
            {
                new TradeDate(new DateTime(2018, 12, 27), false, false, false),
                new TradeDate(new DateTime(2018, 12, 28), false, false, false),
                new TradeDate(new DateTime(2018, 12, 31), true, true, true)
            };

            tradeDateRepository.Setup(r => r.GetTradeDates())
                .Returns(tradeDates);

            // Act
            TradeDateService service = new TradeDateService(tradeDateRepository.Object);
            var result = service.GetTradeDates();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Count.Should().Be(tradeDates.Count);
        }


        [Fact]
        public void GetTradeDates_WhenNoTradeDatesExist_ReturnsFailureResultWithApplicationError()
        {
            // Arrange
            Mock<ITradeDateRepository> tradeDateRepository = new Mock<ITradeDateRepository>();
            List<TradeDate> tradeDates = new List<TradeDate>();

            tradeDateRepository.Setup(r => r.GetTradeDates())
                .Returns(tradeDates);

            // Act
            TradeDateService service = new TradeDateService(tradeDateRepository.Object);
            var result = service.GetTradeDates();

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Error.Should().BeOfType<ApplicationError>();
        }
    }
}
