﻿CREATE TABLE [dbo].[InvestmentAccounts]
(
	AccountNumber      VARCHAR(12)   NOT NULL,
	ClientId           INT           NOT NULL,
	AccountName        VARCHAR(40)   NOT NULL,
	AccountStatusCode  VARCHAR(1)    NOT NULL,
	OpenDate           DATE          NOT NULL,
	CloseDate          DATE          NULL,
	CONSTRAINT PK_InvestmentAccounts 
	    PRIMARY KEY (AccountNumber),
	CONSTRAINT FK_InvestmentAccounts_AccountNumber
	    FOREIGN KEY (ClientId) REFERENCES Clients (ClientId),
	CONSTRAINT FK_InvestmentAccounts_AccountStatusCode
	    FOREIGN KEY (AccountStatusCode) REFERENCES AccountStatusCodes (AccountStatusCode)
);
