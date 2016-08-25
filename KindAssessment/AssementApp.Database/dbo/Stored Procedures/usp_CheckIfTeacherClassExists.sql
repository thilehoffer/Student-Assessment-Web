
CREATE PROC usp_CheckIfTeacherClassExists (
	@TeacherId INT ,
	@ClassName NVARCHAR(128)
)
AS
BEGIN
	IF EXISTS (Select Id FROM dbo.TeacherClass WHERE TeacherId = @TeacherId AND CLassName = @ClassName)
		RETURN 1
	ELSE
		RETURN 0
END
GO
