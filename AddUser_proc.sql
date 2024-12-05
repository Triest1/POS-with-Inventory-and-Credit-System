CREATE PROCEDURE AddUser
    @username NVARCHAR(MAX),
    @password NVARCHAR(MAX),
    @role NVARCHAR(MAX),
    @status NVARCHAR(MAX),
    @date DATE
AS
BEGIN
    INSERT INTO users (username, password, role, status, date)
    VALUES (@username, @password, @role, @status, @date);
END;