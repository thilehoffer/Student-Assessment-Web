CREATE TABLE [dbo].[TeacherClassStudent] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [TeacherClassId]     INT            NOT NULL,
    [StudentName]        NVARCHAR (200) NULL,
    [StudentDateOfBirth] DATETIME2 (7)  NOT NULL,
    [Active]             BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_dbo.TeacherClassStudent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TeacherClassStudent_dbo.TeacherClass_TeacherClassId] FOREIGN KEY ([TeacherClassId]) REFERENCES [dbo].[TeacherClass] ([Id]) ON DELETE CASCADE
);

