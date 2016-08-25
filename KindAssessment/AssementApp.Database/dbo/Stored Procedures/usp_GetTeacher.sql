
CREATE PROC dbo.usp_GetTeacher (
	@UserName NVARCHAR(256)
)
AS
BEGIN
	SELECT
		t.Id ,
		t.UserId 
	FROM
		dbo.Teacher t JOIN	dbo.AspNetUsers u ON t.UserId = u.Id
	WHERE
		u.UserName = @UserName	
END
GO

