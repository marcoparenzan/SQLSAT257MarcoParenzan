CREATE TABLE [crm].[Activities]
(
	[ActivityId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [DateTime] DATETIME NOT NULL, 
    [Duration] DECIMAL(18, 1) NOT NULL, 
    [ActivityTypeId] INT NOT NULL, 
    [Details] [dbo].[UnlimitedString] NULL, 
    [UserId] INT NOT NULL, 
    CONSTRAINT [FK_Activities_Customers] FOREIGN KEY (CustomerId) REFERENCES crm.Customers(CustomerId), 
    CONSTRAINT [FK_Activities_Users] FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId),
    CONSTRAINT [FK_Activities_ActivityTypes] FOREIGN KEY (ActivityTypeId) REFERENCES crm.ActivityTypes(ActivityTypeId)
)
