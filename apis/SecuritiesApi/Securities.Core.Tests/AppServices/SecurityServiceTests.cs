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
    public class SecurityServiceTests
    {
        [Fact]
        public void GetSecurity_WhenInvalidTickerStringPassed_FailureResultWithInvalidDataErrorIsReturned()
        {
            // Arrange
            string ticker = "";
            Mock<ISecurityRepository> securityRepository = new Mock<ISecurityRepository>();

            // Act
            SecurityService service = new SecurityService(securityRepository.Object);
            var result = service.GetSecurity(ticker);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().BeOfType<InvalidDataError>();
            result.Value.Should().BeNull();
        }


        [Fact]
        public void GetSecurity_WhenSecurityExistsForTicker_SuccessfulResultReturnedWithSecurity()
        {
            // Arrange
            string ticker = "ZVZZT";
            Security security = new Security(ticker, "SuperMontage Test Security", "STOCK", new DateTime(2018, 1, 1), new DateTime(2018, 12, 31));
            Mock<ISecurityRepository> securityRepository = new Mock<ISecurityRepository>();
            securityRepository.Setup(r => r.GetSecurity(ticker))
                .Returns(Maybe.Create<Security>(security));

            // Act
            SecurityService service = new SecurityService(securityRepository.Object);
            var result = service.GetSecurity(ticker);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Ticker.Should().Be(ticker);
        }

        [Fact]
        public void GetSecurity_WhenNoSecurityExistsForTicker_FailureResultIsReturnedWithTickerNotFoundError()
        {
            // Arrange
            string ticker = "ZVZZT";
            Mock<ISecurityRepository> securityRepository = new Mock<ISecurityRepository>();
            securityRepository.Setup(r => r.GetSecurity(ticker))
                .Returns(Maybe.Create<Security>(null));

            // Act
            SecurityService service = new SecurityService(securityRepository.Object);
            var result = service.GetSecurity(ticker);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().BeOfType<TickerNotFoundError>();
            result.Error.As<TickerNotFoundError>().InvalidTickers.Should().Contain(ticker);
            result.Value.Should().BeNull();
        }



        [Fact]
        public void GetSecurities_WhenSecuritiesExist_AllSecuritiesAreReturned()
        {
            // Arrange
            var securities = new List<Security>()
            {
                new Security("ZVZZT", "SuperMontage Test Security", "STOCK", new DateTime(2018, 1, 1), new DateTime(2018, 12, 31)),
                new Security("NBZZT", "SuperMontage SmallCap Test Security", "STOCK", new DateTime(2017, 4, 1), new DateTime(2018, 12, 31)),
                new Security("SQZZT", "SuperMontage SmallCap Test Security", "STOCK", new DateTime(2016, 7, 1), new DateTime(2018, 12, 31)),
                new Security("PVZZT", "SuperMontage Test Security", "STOCK", new DateTime(2018, 3, 1), new DateTime(2018, 12, 31))
            };
            Mock<ISecurityRepository> securityRepository = new Mock<ISecurityRepository>();
            securityRepository.Setup(r => r.GetSecurities())
                .Returns(securities);

            // Act
            SecurityService service = new SecurityService(securityRepository.Object);
            var result = service.GetSecurities();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Count.Should().Be(securities.Count);
        }

        [Fact]
        public void GetSecurities_WhenNoSecuritiesExist_FailureResultIsReturnedWithApplicationError()
        {
            // Arrange
            var securities = new List<Security>();
            Mock<ISecurityRepository> securityRepository = new Mock<ISecurityRepository>();
            securityRepository.Setup(r => r.GetSecurities())
                .Returns(securities);

            // Act
            SecurityService service = new SecurityService(securityRepository.Object);
            var result = service.GetSecurities();

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().BeOfType<ApplicationError>();
        }

    }
}
