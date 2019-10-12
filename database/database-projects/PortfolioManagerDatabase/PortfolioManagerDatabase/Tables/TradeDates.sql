CREATE TABLE dbo.TradeDates
(
	TradeDate         DATE                  NOT NULL,
	IsMonthEnd        BIT       DEFAULT 0   NOT NULL,
	IsQuarterEnd      BIT       DEFAULT 0   NOT NULL,
	IsYearEnd         BIT       DEFAULT 0   NOT NULL
	CONSTRAINT PK_TradeDates
	    PRIMARY KEY (TradeDate)
);
