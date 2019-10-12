CREATE TABLE dbo.SecurityPrices
(
	Ticker        VARCHAR(5)     NOT NULL,
	TradeDate     DATE           NOT NULL,
	OpenPrice     DECIMAL(12,4)  NOT NULL,
	ClosePrice    DECIMAL(12,4)  NOT NULL,
	DailyLow      DECIMAL(12,4)  NULL,
	DailyHigh     DECIMAL(12,4)  NULL,
	Volume        BIGINT         NULL,
	Change        DECIMAL(12,4)  NULL,
	ChangePercent DECIMAL(8,4)   NULL,
	CONSTRAINT PK_SecurityPrices
	    PRIMARY KEY (TradeDate, Ticker),
	CONSTRAINT FK_SecurityPrices_Ticker 
	    FOREIGN KEY (Ticker) REFERENCES Securities (Ticker),
	CONSTRAINT FK_SecurityPrices_TradeDate 
	    FOREIGN KEY (TradeDate) REFERENCES TradeDates (TradeDate)

)
