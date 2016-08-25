
CREATE PROC dbo.usp_CreateTeacher (
	@UserId NVARCHAR(128)
)
AS
BEGIN
	INSERT INTO dbo.Teacher (
		UserId
	)
	SELECT 
		@UserId
	
	RETURN SCOPE_IDENTITY()
END
GO
