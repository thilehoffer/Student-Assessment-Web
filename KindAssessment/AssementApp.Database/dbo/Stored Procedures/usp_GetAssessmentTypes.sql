CREATE PROCEDURE [dbo].[usp_GetAssessmentTypes]
AS
BEGIN
	SELECT 
		t.Id ,
		t.AssessmentName
	FROM 
		dbo.AssessmentType t
	WHERE
		t.Active = 1
END