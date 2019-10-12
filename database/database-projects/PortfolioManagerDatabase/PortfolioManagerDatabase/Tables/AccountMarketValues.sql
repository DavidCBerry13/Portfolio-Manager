CREATE TABLE [dbo].[AccountMarketValues]
(
	TradeDate       DATE          NOT NULL,
	AccountNumber   VARCHAR(12)   NOT NULL,
	MarketValue     DECIMAL(14,4) NOT NULL,
	CONSTRAINT PK_AccountMarketValues
	    PRIMARY KEY (TradeDate, AccountNumber),
	CONSTRAINT FK_AccountMarketValues_TradeDate
	    FOREIGN KEY (TradeDate) REFERENCES TradeDates (TradeDate),
	CONSTRAINT FK_AccountMarketValues_AccountNumber
	    FOREIGN KEY (AccountNumber) REFERENCES InvestmentAccounts (AccountNumber)
);
