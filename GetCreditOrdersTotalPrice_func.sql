CREATE FUNCTION dbo.GetCreditOrderTotalPrice (@CustomerID INT)
RETURNS FLOAT
AS
BEGIN
    DECLARE @TotalPrice FLOAT;

    SELECT @TotalPrice = SUM(total_price)
    FROM creditOrders
    WHERE customer_id = @CustomerID;

    RETURN ISNULL(@TotalPrice, 0);  -- Return 0 if there are no orders
END;

