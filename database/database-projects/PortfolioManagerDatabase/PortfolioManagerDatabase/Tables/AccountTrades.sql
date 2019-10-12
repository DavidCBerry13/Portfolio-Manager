CREATE TABLE [dbo].[AccountTrades]
(
	AccountTradeId      INT IDENTITY   NOT NULL,
	TradeDate           DATE           NOT NULL,
	AccountNumber       VARCHAR(12)    NOT NULL,
	Ticker              VARCHAR(5)     NOT NULL,
	BuySell             VARCHAR(1)     NOT NULL,
	Shares              DECIMAL(12,4)  NOT NULL,
	Price               DECIMAL(12,4)  NOT NULL,
	TradeAmount         DECIMAL(14,4)  NOT NULL,
	CONSTRAINT PK_AccountTrades
	    PRIMARY KEY (AccountTradeId),
	CONSTRAINT FK_AccountTrades_TradeDate
	    FOREIGN KEY (TradeDate) REFERENCES TradeDates (TradeDate),
	CONSTRAINT FK_AccountTrades_AccountNumber
	    FOREIGN KEY (AccountNumber) REFERENCES InvestmentAccounts (AccountNumber),
	CONSTRAINT FK_AccountTrades_Ticker
	    FOREIGN KEY (Ticker) REFERENCES Securities (Ticker)
)
