CREATE TABLE dbo.Clients
(
	ClientId        INT IDENTITY(1,1)  NOT NULL,
	FirstName       VARCHAR(30)        NOT NULL,
	LastName        VARCHAR(30)        NOT NULL,
	StreetAddress   VARCHAR(40)        NOT NULL,
	City            VARCHAR(30)        NOT NULL,
	StateCode       VARCHAR(2)         NOT NULL,
	ZipCode         VARCHAR(5)         NOT NULL,
	Email           VARCHAR(60)        NOT NULL,
	Phone           VARCHAR(60)        NOT NULL,
	DateOfBirth     DATE               NOT NULL,
	CONSTRAINT PK_Clients
	    PRIMARY KEY (ClientId)
);
