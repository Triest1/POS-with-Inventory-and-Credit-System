CREATE FUNCTION GetProductDetails
(
    @ProdID VARCHAR(255),
    @Status VARCHAR(MAX)
)
RETURNS TABLE
AS
RETURN
(
    SELECT prod_name, category, price
    FROM products
    WHERE prod_id = @ProdID AND status = @Status
);