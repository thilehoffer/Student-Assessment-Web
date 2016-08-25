CREATE PROCEDURE dbo.usp_CreateStudentAssessment	(
	@StudentId int ,
	@AssessmentTypeId int 
)

AS
BEGIN
	INSERT INTO dbo.StudentAssessment (
		StudentId ,
		AssessmentTypeId ,
		CreatedOn
	)

	SELECT
		@StudentId ,
		@AssessmentTypeId ,
		GETDATE()

	RETURN SCOPE_IDENTITY()
END
