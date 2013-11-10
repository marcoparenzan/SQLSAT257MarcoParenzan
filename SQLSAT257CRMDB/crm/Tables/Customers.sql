CREATE TABLE [crm].[Customers]
(
	[CustomerId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] [dbo].[String] NOT NULL, 
    [FirstName] [dbo].[String] NULL, 
    [TaxCode] [dbo].[TaxCode] NOT NULL, 
    [VatCode] [dbo].[VatCode] NULL 
)
