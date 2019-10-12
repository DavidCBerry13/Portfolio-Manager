MERGE INTO CashFlowTypes AS Target
USING (VALUES
    ('D', 'Deposit', 1),
    ('F', 'Fee', 0),
    ('I', 'Interest Payment', 0),
    ('P', 'Dividend Payment', 0),
    ('W', 'Withdrawal', 1)
)
AS Source (CashFlowTypeCode, CashFlowTypeName, ExternalFlow)
ON Target.CashFlowTypeCode = Source.CashFlowTypeCode
    WHEN MATCHED THEN
        UPDATE
		    SET
			CashFlowTypeName = Source.CashFlowTypeName,
			ExternalFlow = Source.ExternalFlow
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (CashFlowTypeCode, CashFlowTypeName, ExternalFlow)
        VALUES (CashFlowTypeCode, CashFlowTypeName, ExternalFlow);

GO