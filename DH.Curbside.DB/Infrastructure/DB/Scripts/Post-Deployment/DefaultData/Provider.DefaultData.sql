PRINT 'Insert or update provider records into dbo.Provider'

Merge into [dbo].[Provider] as target
using (
	values 
	('5A77932E-B876-4528-8491-F2FFA5F2FDEF', '80c641e8-e12f-406a-bb31-9557ce7d9f66','FirstName','LastName', '5417543010','Default@test.com','1')
 ) as source (ProviderId
			, TenantId
			, FirstName
			, LastName
			, Phone
			, Email
			, LoginSourceSystem)
on target.ProviderId = source.ProviderId
when not matched by target then
	insert (ProviderId, TenantId, FirstName, LastName, Phone, Email, LoginSourceSystem)
	values (ProviderId, TenantId, FirstName, LastName, Phone, Email, LoginSourceSystem)
when matched then 
	update set target.TenantId = source.TenantId
	, target.FirstName = source.FirstName
	, target.LastName = source.LastName
	, target.Phone = source.Phone
	, target.Email = source.Email
	, target.LoginSourceSystem = source.LoginSourceSystem; 

	