CREATE PROCEDURE dbo.usp_GetRecentAssessments (
	@TeacherId INT
)
AS
BEGIN
	SELECT TOP 10 
		sa.Id ,
		ISNULL(sa.UpdatedOn, sa.CreatedOn) Updated ,
		tcs.StudentName ,
		at.AssessmentName AS Assessment
	FROM
			dbo.StudentAssessment sa 
	JOIN	dbo.TeacherClassStudent tcs on sa.StudentId = tcs.Id  
	JOIN	dbo.TeacherClass tc on tcs.TeacherClassId = tc.Id  
	JOIN	dbo.AssessmentType at on sa.AssessmentTypeId = at.Id  
	WHERE
		tc.TeacherId = @TeacherId
	ORDER BY Updated DESC
END
