CREATE PROCEDURE [dbo].[usp_GetActiveClasses]
	@TeacherId INT
AS
BEGIN
	SELECT
		tc.Id ,
		tc.ClassName  AS [Text]
	FROM 
		dbo.TeacherClass tc 
	WHERE 
		tc.Active	= 1 AND
		tc.TeacherId = @TeacherId
	Order By
		[Text] ASC
END
	 