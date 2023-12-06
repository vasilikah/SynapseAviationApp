CREATE TABLE [users].[Users]
(
    [Id]                     UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_User_Id] DEFAULT newsequentialid(),
    [FirstName]              NVARCHAR(150)    NOT NULL,
    [LastName]               NVARCHAR(150)    NOT NULL,
    [Age]                    INT              NOT NULL
  
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);