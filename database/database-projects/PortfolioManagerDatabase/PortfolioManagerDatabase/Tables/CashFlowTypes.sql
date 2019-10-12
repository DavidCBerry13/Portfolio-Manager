CREATE TABLE CashFlowTypes
(
	CashFlowTypeCode     VARCHAR(1)     NOT NULL,
	CashFlowTypeName     VARCHAR(40)    NOT NULL,
	ExternalFlow         BIT            NOT NULL,
	CONSTRAINT PK_CashFlowTypes
	    PRIMARY KEY (CashFlowTypeCode)
);
