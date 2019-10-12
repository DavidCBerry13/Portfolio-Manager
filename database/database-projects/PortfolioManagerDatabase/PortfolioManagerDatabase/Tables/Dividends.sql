CREATE TABLE dbo.Dividends
(
	DividendId         INT            NOT NULL,
	Ticker             VARCHAR(5)     NOT NULL,
	DeclaredDate       DATE           NOT NULL,
	ExDividendDate     DATE           NOT NULL,
	RecordDate         DATE           NOT NULL,
	PaymentDate        DATE           NOT NULL,
	DividendType       VARCHAR(20)    NOT NULL,
	DividendPerShare   DECIMAL(12,4)  NOT NULL,
	CONSTRAINT PK_Dividends
	    PRIMARY KEY (DividendId),
	CONSTRAINT FK_Dividends_Ticker
	    FOREIGN KEY (Ticker) REFERENCES Securities (Ticker)
);
