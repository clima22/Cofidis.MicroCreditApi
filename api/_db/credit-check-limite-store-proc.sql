CREATE PROCEDURE dbo.CreditCheckLimit
	@creditAmount DECIMAL,
	@netMonthlyIncome DECIMAL,
	@@result BIT OUT
AS
BEGIN
	DECLARE @maxAllowed DECIMAL
	
	--Poderia se ter uma tabela com configuração dos intervalos e valor máximo...
	
	IF (@netMonthlyIncome <= 1000) 
		SET @maxAllowed = 1000
	ELSE IF (@netMonthlyIncome <= 2000) 
		SET @maxAllowed = 2000
	ELSE
		SET @maxAllowed = 5000

	IF (@creditAmount > @maxAllowed)
		SET @@result = 0 --NOK
	ELSE
		SET @@result = 1 --OK
END