CREATE TABLE users
(
	id int PRIMARY KEY IDENTITY (1,1),
	username VARCHAR(MAX) NULL,
	password VARCHAR(MAX) NULL,
	role VARCHAR(MAX) NULL,
	status VARCHAR(MAX) NULL,
	date DATE NULL
)

select * from users

INSERT INTO users (username, password, role, status) VALUES('tesing1', 'admin123', 'Cashier', 'Active')

INSERT INTO users(username, password, role, status, date) VALUES('admin', 'admin123', 'Admin', 'Active', '2024-09-12')

SELECT * FROM products

DBCC CHECKIDENT ('products', RESEED, 3);

CREATE TABLE products
(
	id INT PRIMARY KEY IDENTITY(1,1),
	prod_id VARCHAR(255) NULL UNIQUE,
	prod_name VARCHAR(MAX) NULL,
	category VARCHAR(MAX) NULL,
	price FLOAT NULL,
	stock INT NULL,
	image_path VARCHAR(MAX) NULL,
	status VARCHAR(MAX) NULL,
	date_insert DATE NULL
)
ALTER TABLE products
ADD CONSTRAINT UQ_prod_id UNIQUE (prod_id);

CREATE TABLE stock
(
    stock_id INT PRIMARY KEY IDENTITY(1,1),
    prod_id VARCHAR(255) NOT NULL,       -- Foreign key reference to products table
    quantity INT NOT NULL DEFAULT 0,    -- Amount of stock added or removed
    stock_date DATE NOT NULL DEFAULT GETDATE(),-- Date of the stock change
    FOREIGN KEY (prod_id) REFERENCES products(prod_id)
);
select * from orders

CREATE TABLE orders
(
	id int PRIMARY KEY IDENTITY(1,1),
	prod_id VARCHAR(MAX) NULL,
	prod_name VARCHAR(MAX) NULL,
	category VARCHAR(MAX) NULL,
	qty INT NULL,
	orig_price FLOAT NULL,
	total_price FLOAT NULL,
	order_date DATE NULL,
	customer_id INT NULL
)

CREATE TABLE customers
(
	id INT PRIMARY KEY IDENTITY(1,1),
	customer_id INT NULL,
	total_price FLOAT NULL,
	amount FLOAT NULL,
	change FLOAT NULL,
	order_date DATE NULL
)

select * from creditOrders
------------------CREDIT PART-----------------------------


CREATE TABLE creditCustomer
(
    id INT PRIMARY KEY IDENTITY (1,1),       
    customer_id INT NULL,                    
    total_price FLOAT NULL,                   
    amount FLOAT NULL,                        
    change FLOAT NULL,                        
    order_date DATE NULL,
	customer_name VARCHAR(255) NULL
)

CREATE TABLE creditOrders 
(
	id int PRIMARY KEY IDENTITY(1,1),
	prod_id VARCHAR(MAX) NULL,
	prod_name VARCHAR(MAX) NULL,
	category VARCHAR(MAX) NULL,
	qty INT NULL,
	orig_price FLOAT NULL,
	total_price FLOAT NULL,
	order_date DATE NULL,
	customer_id INT NULL
)

SELECT * FROM creditOrders

Select * from creditPayments

CREATE TABLE creditPayments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),  -- Unique identifier for each payment
    CustomerID INT NULL,                   -- Reference to the customer making the payment
    TotalPrice FLOAT NULL,       -- Total price for the credit transaction
    AmountPaid FLOAT NULL,-- Amount paid in this transaction
    Amount FLOAT NULL,           -- The amount that remains to be paid (if applicable)
    Change FLOAT NULL,           -- Change returned to the customer
    PaymentDate DATE NULL, -- Date of the payment
	customer_name VARCHAR(255) NULL,
)

SELECT * FROM creditPayments
