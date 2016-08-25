CREATE PROCEDURE dbo.usp_GetAssessmentData	(
	@Id INT
)
AS
BEGIN
	SELECT
		st.AssessmentData
	FROM
		dbo.StudentAssessment st
	WHERE
		st.Id = @Id
END