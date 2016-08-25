CREATE PROCEDURE dbo.usp_SaveAssessmentData	(
	@Id int = 0,
	@Data NVARCHAR(MAX)
)
AS
BEGIN
	UPDATE dbo.StudentAssessment
	SET 
		AssessmentData = @Data ,
		UpdatedOn = GETDATE()
	WHERE
		Id = @Id
END
