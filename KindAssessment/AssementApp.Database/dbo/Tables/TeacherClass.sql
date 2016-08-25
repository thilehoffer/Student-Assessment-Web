CREATE TABLE [dbo].[TeacherClass] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [TeacherId] INT            NOT NULL,
    [ClassName] NVARCHAR (128) NOT NULL,
    [Active]    BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_dbo.TeacherClass] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.TeacherClass_dbo.Teacher_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Teacher] ([Id]) ON DELETE CASCADE
);

