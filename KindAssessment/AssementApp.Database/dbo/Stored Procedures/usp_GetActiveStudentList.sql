CREATE PROCEDURE dbo.usp_GetActiveStudentList (
	@TeacherId int
)
	
AS
BEGIN
	SELECT
		tcs.Id ,
		tcs.StudentName + ' - ' + convert(NVARCHAR(30), tcs.StudentDateOfBirth, 101 ) AS [Text]
	FROM 
	dbo.TeacherClassStudent tcs JOIN dbo.TeacherClass tc on tcs.TeacherClassId = tc.Id
	WHERE 
		tc.Active	= 1 AND
		tcs.Active	= 1 AND
		tc.TeacherId = @TeacherId
	Order By
		[Text] ASC
END