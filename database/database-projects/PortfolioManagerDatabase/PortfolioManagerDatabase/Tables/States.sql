CREATE TABLE [dbo].[States]
(
	StateCode     VARCHAR(2)      NOT NULL,
	StateName     VARCHAR(30)     NOT NULL,
	CONSTRAINT PK_States
	    PRIMARY KEY (StateCode)
);
