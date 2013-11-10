CREATE TABLE [dbo].[AddressSearchs]
(
	[SearchId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Address] NVARCHAR(128) NOT NULL, 
    [Timestamp] DATETIME NOT NULL, 
    [Provider] NVARCHAR(64) NOT NULL, 
    [Response] NVARCHAR(4000) NOT NULL, 
    [Coordinates] [sys].[geography] NOT NULL 
)
