CREATE TABLE dbo.SecurityTypes
(
	SecurityTypeCode          VARCHAR(5)        NOT NULL,
	SecurityTypeName          VARCHAR(30)       NOT NULL,
	CONSTRAINT PK_SecurityTypes PRIMARY KEY (SecurityTypeCode)
);
