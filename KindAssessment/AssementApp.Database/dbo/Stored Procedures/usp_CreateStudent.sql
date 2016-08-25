
CREATE PROC dbo.usp_CreateStudent (
	@TeacherClassId INT ,
	@StudentName NVARCHAR(200) ,
	@StudentDateOfBirth DATETIME2 ,
	@Active BIT
)
AS
BEGIN
	INSERT INTO dbo.TeacherClassStudent (
		TeacherClassId, 
		StudentName, 
		StudentDateOfBirth, 
		Active
	)
	SELECT
		@TeacherClassId  ,
		@StudentName  ,
		@StudentDateOfBirth  ,
		@Active  

	RETURN SCOPE_IDENTITY()
END
GO
