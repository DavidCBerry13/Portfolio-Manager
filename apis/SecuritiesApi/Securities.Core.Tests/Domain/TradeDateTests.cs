using FluentAssertions;
using Securities.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Securities.Core.Tests.Domain
{
    public class TradeDateTests
    {


        [Fact]
        public void GetHashCode_ReturnsDifferentHashCode_ForTwoDifferentTradeDates()
        {
            TradeDate tradeDateOne = new TradeDate(new DateTime(2018, 12, 30), false, false, false);
            TradeDate tradeDateTwo = new TradeDate(new DateTime(2018, 12, 31), true, true, true);

            var hashCodeOne = tradeDateOne.GetHashCode();
            var hashCodeTwo = tradeDateTwo.GetHashCode();

            hashCodeOne.Should().NotBe(hashCodeTwo);
        }

        [Fact]
        public void GetHashCode_ReturnsSameHasCode_ForSameTradeDates()
        {
            TradeDate tradeDateOne = new TradeDate(new DateTime(2018, 12, 31), true, true, true);
            TradeDate tradeDateTwo = new TradeDate(new DateTime(2018, 12, 31), true, true, true);

            var hashCodeOne = tradeDateOne.GetHashCode();
            var hashCodeTwo = tradeDateTwo.GetHashCode();

            hashCodeTwo.Should().Be(hashCodeOne);
        }


        [Fact]
        public void AsString_ReturnsDateString_InYYYYMMDDFormat()
        {
            TradeDate tradeDateOne = new TradeDate(new DateTime(2018, 12, 31), true, true, true);

            var dateString = tradeDateOne.AsString();

            dateString.Should().Be("2018-12-31");
        }

        [Fact]
        public void AsString_CorrectlyHandlesSingleDigitDaysAndMonths()
        {
            TradeDate tradeDateOne = new TradeDate(new DateTime(2018, 4, 6), false, false, false);

            var dateString = tradeDateOne.AsString();

            dateString.Should().Be("2018-04-06");
        }


        [Fact]
        public void Equals_ReturnsTrue_WhenComparingTwoTradeDatesOfSameDate()
        {
            TradeDate tradeDateOne = new TradeDate(new DateTime(2018, 12, 31), true, true, true);
            TradeDate tradeDateTwo = new TradeDate(new DateTime(2018, 12, 31), true, true, true);

            var result = tradeDateOne.Equals(tradeDateTwo);

            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenComparingTwoTradeDatesOfDifferentDates()
        {
            TradeDate tradeDateOne = new TradeDate(new DateTime(2018, 12, 30), false, false, false);
            TradeDate tradeDateTwo = new TradeDate(new DateTime(2018, 12, 31), true, true, true);

            var result = tradeDateOne.Equals(tradeDateTwo);

            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenComparingTradeDateToNull()
        {
            TradeDate tradeDateOne = new TradeDate(new DateTime(2018, 12, 30), false, false, false);

            var result = tradeDateOne.Equals(null);

            result.Should().BeFalse();
        }



    }
}
