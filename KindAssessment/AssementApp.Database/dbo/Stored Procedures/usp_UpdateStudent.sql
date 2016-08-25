 
 CREATE PROC dbo.usp_UpdateStudent (
	@Id INT, 
	@TeacherClassId INT, 
	@StudentName NVARCHAR(200) , 
	@StudentDateOfBirth DATETIME2, 
	@Active BIT
)
AS
BEGIN
	UPDATE dbo.TeacherClassStudent
	SET
		TeacherClassId = @TeacherClassId ,
		StudentName = @StudentName ,
		StudentDateOfBirth = @StudentDateOfBirth ,
		Active = @Active
	WHERE
		Id = @Id
END
GO

