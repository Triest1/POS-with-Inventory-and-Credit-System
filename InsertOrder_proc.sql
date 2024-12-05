CREATE PROCEDURE InsertOrder
    @customer_id INT,
    @prod_id VARCHAR(MAX),
    @prod_name VARCHAR(MAX),
    @category VARCHAR(MAX),
    @qty INT,
    @orig_price FLOAT,
    @total_price FLOAT,
    @order_date DATE
AS
BEGIN
    INSERT INTO orders (customer_id, prod_id, prod_name, category, qty, orig_price, total_price, order_date)
    VALUES (@customer_id, @prod_id, @prod_name, @category, @qty, @orig_price, @total_price, @order_date);
END