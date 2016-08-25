CREATE PROC dbo.usp_StudentIsTeachers (
	@TeacherId INT,
	@StudentId INT
)
AS
BEGIN
	IF ( (SELECT tc.TeacherId FROM dbo.TeacherClass tc JOIN dbo.TeacherClassStudent tcs on tcs.TeacherClassId = tc.Id WHERE tcs.Id = @StudentId) = @TeacherId)
	RETURN 1
	ELSE
	RETURN 0
END
GO

