/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

If not exists (Select * from dbo.AssessmentType t where t.AssessmentName = 'Letter Recognition - Upper Case')
BEGIN
	Set Identity_Insert dbo.AssessmentType ON 
	Insert into dbo.AssessmentType (Id, AssessmentName) VALUES (1, 'Letter Recognition - Upper Case')
	Set Identity_Insert dbo.AssessmentType OFF
END

If not exists (Select * from dbo.AssessmentType t where t.AssessmentName = 'Letter Recognition - Lower Case')
BEGIN
	Set Identity_Insert dbo.AssessmentType ON 
	Insert into dbo.AssessmentType (Id, AssessmentName) VALUES (2, 'Letter Recognition - Lower Case')
	Set Identity_Insert dbo.AssessmentType OFF
END