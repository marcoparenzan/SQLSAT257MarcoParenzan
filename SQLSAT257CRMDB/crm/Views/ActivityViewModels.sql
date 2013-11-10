CREATE VIEW crm.[ActivityViewModels]
	AS 	
	SELECT 
		a.ActivityId
		, c.Name as CustomerName
		, c.FirstName as CustomerFirstName
		, c.TaxCode as CustomerTaxCode
		, c.VatCode as CustomerVatCode
		, a.DateTime
		, a.Duration
		, at.Description as ActivityTypeDescription
		, a.Details
		, u.Username
	FROM crm.Activities a
	JOIN crm.Customers c on a.CustomerId = c.CustomerId
	JOIN crm.ActivityTypes at on a.ActivityTypeId = at.ActivityTypeId
	JOIN dbo.Users u on a.UserId = u.UserId
