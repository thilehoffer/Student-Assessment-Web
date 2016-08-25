CREATE TABLE [dbo].[AssessmentType]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [AssessmentName] NVARCHAR(200) NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 1
)
