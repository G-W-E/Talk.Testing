Create PROCEDURE ValidateUser
    @UserName NVARCHAR(100),
    @PasswordHash NVARCHAR(64)
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the user exists with the provided username and password hash
    IF EXISTS (
        SELECT 1 
        FROM [User]
        WHERE UserName = @UserName AND Password = @PasswordHash
    )
    BEGIN
        -- Return success
        SELECT 1 AS IsValid;
    END
    ELSE
    BEGIN
        -- Return failure
        SELECT 0 AS IsValid;
    END
END;

