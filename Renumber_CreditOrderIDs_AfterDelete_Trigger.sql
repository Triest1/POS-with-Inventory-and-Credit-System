CREATE TRIGGER trg_Renumber_CreditOrderIDs_AfterDelete
ON creditOrders
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Create a temporary table to hold current credit orders without identity
    CREATE TABLE #TempCreditOrders (
        prod_id VARCHAR(MAX) NULL,
        prod_name VARCHAR(MAX) NULL,
        category VARCHAR(MAX) NULL,
        qty INT NULL,
        orig_price FLOAT NULL,
        total_price FLOAT NULL,
        order_date DATE NULL,
        customer_id INT NULL
    );

    -- Insert existing credit orders into the temporary table, excluding the deleted ones
    INSERT INTO #TempCreditOrders (prod_id, prod_name, category, qty, orig_price, total_price, order_date, customer_id)
    SELECT prod_id, prod_name, category, qty, orig_price, total_price, order_date, customer_id
    FROM creditOrders;

    -- Delete all records from the creditOrders table
    TRUNCATE TABLE creditOrders;

    -- Insert the data back with renumbered IDs
    INSERT INTO creditOrders (prod_id, prod_name, category, qty, orig_price, total_price, order_date, customer_id)
    SELECT 
        prod_id, 
        prod_name, 
        category, 
        qty, 
        orig_price, 
        total_price, 
        order_date, 
        customer_id
    FROM #TempCreditOrders;

    -- Clean up the temporary table
    DROP TABLE #TempCreditOrders;
END;