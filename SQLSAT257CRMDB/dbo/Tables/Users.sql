CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] [dbo].[String] NOT NULL, 
    [DisplayName] [dbo].[String] NOT NULL
)
