CREATE TABLE [dbo].[StudentAssessment]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	[StudentId] INT NOT NULL,
    [AssessmentTypeId] INT NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL, 
    [UpdatedOn] DATETIME2 NULL, 
    [CompletedOn] DATETIME2 NULL, 
    [AssessmentData] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_StudentAssessment_TeacherClassStudent] FOREIGN KEY (StudentId) REFERENCES dbo.TeacherClassStudent(Id),
	CONSTRAINT [FK_StudentAssessment_AssessmentType] FOREIGN KEY (AssessmentTypeId) REFERENCES dbo.AssessmentType(Id)
)
