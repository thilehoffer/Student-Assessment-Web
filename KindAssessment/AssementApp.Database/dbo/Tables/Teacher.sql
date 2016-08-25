CREATE TABLE [dbo].[Teacher] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [UserId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.Teacher] PRIMARY KEY CLUSTERED ([Id] ASC)
);

