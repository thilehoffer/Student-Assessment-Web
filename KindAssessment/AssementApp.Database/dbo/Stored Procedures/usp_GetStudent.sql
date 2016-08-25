
CREATE PROC dbo.usp_GetStudent(
	@Id int
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
		tcs.Id = @Id
END
GO

