CREATE PROCEDURE crm.[DeleteActivity] (
		@ActivityId int, 
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

	SELECT @activityTypeID=ActivityTypeId
	FROM crm.ActivityTypes
	WHERE Description = @ActivityTypeDescription

	SELECT @userId=UserId
	FROM dbo.Users
	WHERE Username = @Username

	DELETE FROM crm.Activities
	WHERE 
		ActivityID = @ActivityId
		AND CustomerId =@customerId
		AND ActivityTypeID = @ActivityTypeId
		AND UserId = @UserId
		AND [DateTime] = @DateTime
		AND [Duration] = @Duration
		AND Details = @Details

RETURN
