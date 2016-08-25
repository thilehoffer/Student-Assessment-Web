CREATE PROCEDURE [dbo].[usp_GetActiveAssessments](
	@TeacherId INT
)
AS
BEGIN
	Select 
		Id ,
		AssessmentName AS [TEXT]
	FROM 
		dbo.AssessmentType 
	WHERE
		Active = 1
	ORDER BY [TEXT] ASC
END
