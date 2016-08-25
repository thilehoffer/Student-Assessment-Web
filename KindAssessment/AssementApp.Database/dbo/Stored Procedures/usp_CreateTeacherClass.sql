

CREATE PROC dbo.usp_CreateTeacherClass (
	@TeacherId int, 
	@ClassName NVARCHAR(128), 
	@Active BIT
)
AS
BEGIN
	INSERT into.TeacherClass (
		TeacherId, 
		ClassName, 
		Active
	)
	SELECT
		@Teacherid ,
		@ClassName ,
		@Active

	RETURN SCOPE_IDENTITY()
END
GO

