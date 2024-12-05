CREATE FUNCTION dbo.GetOrderTotalPrice (@CustomerID INT)
RETURNS FLOAT
AS
BEGIN
    DECLARE @TotalPrice FLOAT;

    SELECT @TotalPrice = SUM(total_price)
    FROM orders
    WHERE customer_id = @CustomerID;

    RETURN ISNULL(@TotalPrice, 0);  -- Return 0 if there are no orders
END;

