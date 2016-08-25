 
CREATE PROC dbo.usp_GetTeacherClass (
	@Id int
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
		tc.Id = @Id
END
GO
