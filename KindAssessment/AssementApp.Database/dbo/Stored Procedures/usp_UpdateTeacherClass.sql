
CREATE PROC dbo.usp_UpdateTeacherClass (
	@Id int,
	@TeacherId int, 
	@ClassName NVARCHAR(128), 
	@Active BIT
)

AS
BEGIN
	UPDATE dbo.TeacherClass
	SET
		TeacherId = @TeacherId ,
		ClassName = @ClassName ,
		Active = @Active
	WHERE
		Id = @Id
END
GO

