
CREATE PROC dbo.usp_GetTeacherClasses (
	@TeacherId INT
)
AS
BEGIN
	SELECT 
		tc.Id , 
		tc.TeacherId , 
		tc.ClassName , 
		tc.Active
	FROM
		dbo.TeacherClass tc
	WHERE
		tc.TeacherId = @TeacherId

END
GO

