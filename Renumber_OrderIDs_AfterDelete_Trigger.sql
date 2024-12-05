CREATE TRIGGER trg_Renumber_OrderIDs_AfterDelete
ON orders
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Create a temporary table to hold current orders without identity
    CREATE TABLE #TempOrders (
        prod_id VARCHAR(MAX) NULL,
        prod_name VARCHAR(MAX) NULL,
        category VARCHAR(MAX) NULL,
        qty INT NULL,
        orig_price FLOAT NULL,
        total_price FLOAT NULL,
        order_date DATE NULL,
        customer_id INT NULL
    );

    -- Insert existing orders into the temporary table, excluding the deleted ones
    INSERT INTO #TempOrders (prod_id, prod_name, category, qty, orig_price, total_price, order_date, customer_id)
    SELECT prod_id, prod_name, category, qty, orig_price, total_price, order_date, customer_id
    FROM orders;

    -- Truncate the original table to reset the identity seed
    TRUNCATE TABLE orders;

    -- Insert the data back with renumbered IDs
    INSERT INTO orders (prod_id, prod_name, category, qty, orig_price, total_price, order_date, customer_id)
    SELECT 
        prod_id, 
        prod_name, 
        category, 
        qty, 
        orig_price, 
        total_price, 
        order_date, 
        customer_id
    FROM #TempOrders;

    -- Clean up the temporary table
    DROP TABLE #TempOrders;
END;

DROP TRIGGER IF EXISTS dbo.trg_Renumber_ProductIDs_AfterDelete