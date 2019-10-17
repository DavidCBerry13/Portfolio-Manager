MERGE INTO AccountStatusCodes AS Target
USING (VALUES
    ('O', 'Open'  , 1),
    ('C', 'Closed', 0)
)
AS Source (AccountStatusCode, AccountStatusName, IsOpen)
ON Target.AccountStatusCode = Source.AccountStatusCode
    WHEN MATCHED THEN
        UPDATE
		    SET
			AccountStatusName = Source.AccountStatusName,
			IsOpen = Source.IsOpen
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (AccountStatusCode, AccountStatusName, IsOpen)
        VALUES (AccountStatusCode, AccountStatusName, IsOpen);

GO