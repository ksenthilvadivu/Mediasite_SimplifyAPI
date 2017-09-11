PRINT 'Insert or update tenant records into dbo.Tenant'

Merge into dbo.Tenant as target
using (
	values 
	('80c641e8-e12f-406a-bb31-9557ce7d9f66', 'Dignity Health'),
	('4d8d4947-ebe5-4648-801c-d1033331c683', 'BNI')
 ) as source (TenantId
			, TenantName)
on target.TenantId = source.TenantId
when not matched by target then
	insert (TenantId, TenantName)
	values (TenantId, TenantName)
when matched then 
	update set target.TenantName = source.TenantName;
