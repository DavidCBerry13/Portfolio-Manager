CREATE TABLE dbo.Securities
(
	Ticker              VARCHAR(5)        NOT NULL,
	SecurityTypeCode    VARCHAR(5)        NOT NULL,
	SecurityName        VARCHAR(80)       NOT NULL,
	Description         VARCHAR(2048)     NOT NULL,
	CONSTRAINT PK_Securities 
	    PRIMARY KEY (Ticker),
	CONSTRAINT FK_Securities_SecurityTypeCode
	    FOREIGN KEY (SecurityTypeCode) REFERENCES SecurityTypes (SecurityTypeCode)
);
