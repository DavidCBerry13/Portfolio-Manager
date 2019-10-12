MERGE INTO SecurityTypes AS Target
USING (VALUES
    ('CASH', 'Cash'),
    ('ETF', 'Exchange Traded Fund'), 
    ('FUND', 'Mutual Fund'),
    ('STOCK', 'Stock')
)
AS Source (SecurityTypeCode, SecurityTypeName)
ON Target.SecurityTypeCode = Source.SecurityTypeCode
    WHEN MATCHED THEN
        UPDATE
		    SET
			SecurityTypeName = Source.SecurityTypeName
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (SecurityTypeCode, SecurityTypeName)
        VALUES (SecurityTypeCode, SecurityTypeName);

GO


