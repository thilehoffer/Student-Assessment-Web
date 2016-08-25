CREATE PROCEDURE dbo.usp_GetStudentAssessment (@Id int)
AS
BEGIN
	SELECT
		sa.StudentId ,
		sa.AssessmentTypeId ,
		tcs.StudentName ,
		at.AssessmentName as AssessmentTypeName
	FROM
		dbo.StudentAssessment sa 
		JOIN dbo.TeacherClassStudent tcs on sa.StudentId = tcs.Id 
		JOIN dbo.AssessmentType at on sa.AssessmentTypeId = at.Id
	WHERE
	 sa.Id = @Id
END
