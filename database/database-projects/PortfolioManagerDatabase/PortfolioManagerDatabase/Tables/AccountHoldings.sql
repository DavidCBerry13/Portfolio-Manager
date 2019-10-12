CREATE TABLE [dbo].[AccountHoldings]
(
    TradeDate          DATE             NOT NULL,
	AccountNumber      VARCHAR(12)      NOT NULL,
	Ticker             VARCHAR(5)       NOT NULL,
	Shares             DECIMAL(12,4)    NOT NULL,
	Price              DECIMAL(12,4)    NOT NULL,
	MarketValue        DECIMAL(14,4)    NOT NULL,
	CONSTRAINT PK_AccountHoldings
	    PRIMARY KEY (TradeDate, AccountNumber, Ticker),
	CONSTRAINT FK_AccountHoldings_TradeDate
	    FOREIGN KEY (TradeDate) REFERENCES TradeDates (TradeDate),
	CONSTRAINT FK_AccountHoldings_AccountNumber
	    FOREIGN KEY (AccountNumber) REFERENCES InvestmentAccounts (AccountNumber),
	CONSTRAINT FK_AccountHoldings_Ticker
	    FOREIGN KEY (Ticker) REFERENCES Securities (Ticker)
);
