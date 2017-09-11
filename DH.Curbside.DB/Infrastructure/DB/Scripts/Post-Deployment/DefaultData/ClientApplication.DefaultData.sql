PRINT 'Insert or update client application records into dbo.ClientApplication'

Merge into dbo.ClientApplication as target
using (
	values 
	('93ae9e3b-fba6-4b89-b722-f3f8bf7738af', 'Submitter App', 1),
	('ddd6afff-f931-4c28-bfb5-e8afab16c76b', 'Reviewer App', 1),
	('66d565b6-f4d3-4705-9443-d7eb4d9fdac6', 'Admin Portal', 0)
 ) as source ([ClientApplicationToken]
			  ,[ClientApplicationName]
			  ,[IsMobile])
on target.ClientApplicationToken = source.ClientApplicationToken
when not matched by target then
	insert ([ClientApplicationToken],[ClientApplicationName],[IsMobile])
	values ([ClientApplicationToken],[ClientApplicationName],[IsMobile])
when matched then 
	update set target.ClientApplicationName = source.ClientApplicationName
				, target.IsMobile = source.IsMobile;
