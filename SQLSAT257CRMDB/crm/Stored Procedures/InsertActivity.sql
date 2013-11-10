CREATE PROCEDURE crm.[InsertActivity] (
		@CustomerName [dbo].[String], 
		@CustomerFirstName [dbo].[String] = NULL, 
		@CustomerTaxCode [dbo].[TaxCode], 
		@CustomerVatCode [dbo].[VatCode] = NULL,
		@DateTime DATETIME, 
		@Duration [dbo].[Hours], 
		@ActivityTypeDescription [dbo].[String],
		@Details dbo.UnlimitedString = NULL, 
		@Username [dbo].[String]
	)
	AS

	DECLARE @customerId int
	DECLARE @activityTypeId int
	DECLARE @userId int

	SELECT @customerId = CustomerId
	FROM crm.Customers 
	WHERE Name=@CustomerName AND TaxCode = @CustomerTaxCode
	
	IF @CustomerID IS NULL
	BEGIN
		INSERT INTO crm.Customers(Name, FirstName, TaxCode,VatCode)
		VALUES (@CustomerName, @CustomerFirstName, @CustomerTaxCode, @CustomerVatCode)

		SELECT @customerId = SCOPE_IDENTITY()
	END 

	SELECT @activityTypeID=ActivityTypeId
	FROM crm.ActivityTypes
	WHERE Description = @ActivityTypeDescription

	SELECT @userId=UserId
	FROM dbo.Users
	WHERE Username = @Username

	INSERT INTO crm.Activities(CustomerId, ActivityTypeId, UserId, DateTime, Duration, Details)
	VALUES (@CustomerId, @ActivityTypeId, @UserId, @DateTime,@Duration, @Details)

	DECLARE @activityId int = SCOPE_IDENTITY()
	SELECT *
	FROM  crm.[ActivityViewModels]
	WHERE ActivityId = @activityId

RETURN
