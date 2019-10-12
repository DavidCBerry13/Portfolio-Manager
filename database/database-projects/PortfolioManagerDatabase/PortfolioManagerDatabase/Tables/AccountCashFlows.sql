CREATE TABLE dbo.AccountCashFlows
(
	AccountCashFlowId     INT IDENTITY      NOT NULL,
	AccountNumber         VARCHAR(12)       NOT NULL,
	TradeDate             DATE              NOT NULL,
	CashFlowTypeCode      VARCHAR(1)        NOT NULL,
	Amount                DECIMAL(12,4)     NOT NULL,
	Description           VARCHAR(200)      NOT NULL,
	DividendTicker        VARCHAR(5)        NULL,
	DividendShares        DECIMAL(12,4)     NULL,
	DividendPerShare      DECIMAL(12,4)     NULL,
	InterestPrincipal     DECIMAL(14,4)     NULL,
	InterestRate          DECIMAL(8,4)      NULL,
	CONSTRAINT PK_AccountCashFlows
	    PRIMARY KEY (AccountCashFlowId),
	CONSTRAINT FK_AccountCashFlows_TradeDate
	    FOREIGN KEY (TradeDate) REFERENCES TradeDates (TradeDate),
	CONSTRAINT FK_AccountCashFlows_AccountNumber
	    FOREIGN KEY (AccountNumber) REFERENCES InvestmentAccounts (AccountNumber),
	CONSTRAINT FK_AccountCashFlows
	    FOREIGN KEY (CashFlowTypeCode) REFERENCES CashFlowTypes (CashFlowTypeCode),
	CONSTRAINT FK_AccountCashFlows_DividendTicker
	    FOREIGN KEY (DividendTicker) REFERENCES Securities (Ticker)
);
