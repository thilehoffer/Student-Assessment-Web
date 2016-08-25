
CREATE PROC dbo.usp_GetStudentsInClass (
	@ClassId INT
)
AS
BEGIN
	SELECT 
		tcs.Id ,
        tcs.TeacherClassId ,
        tc.ClassName ,
        tcs.StudentDateOfBirth ,
        tcs.StudentName ,
        tcs.Active
	FROM
		dbo.TeacherClass tc join dbo.TeacherClassStudent tcs on tcs.TeacherClassId = tc.Id
	WHERE
		tcs.TeacherClassId = @ClassId
END
GO

