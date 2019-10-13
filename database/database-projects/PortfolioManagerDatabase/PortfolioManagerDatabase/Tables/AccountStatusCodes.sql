CREATE TABLE [dbo].[AccountStatusCodes]
(
	AccountStatusCode   VARCHAR(1)   NOT NULL,
	AccountStatusName   VARCHAR(20)  NOT NULL,
	IsOpen              BIT          NOT NULL,
	CONSTRAINT PK_AcountStatusCodes
	    PRIMARY KEY (AccountStatusCode)
);
