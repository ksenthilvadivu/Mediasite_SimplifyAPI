/****** Object:  Table [dbo].[ClientApplicationTenant]    Script Date: 7/27/2017 10:28:36 AM ******/


CREATE TABLE [dbo].[ClientApplicationTenant](
	[TenantId] [uniqueidentifier] NOT NULL,
	[ClientApplicationId] [int] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ClientApplicationTenant]  WITH CHECK ADD  CONSTRAINT [FK_ClientApplicationOrganization_ClientApplication] FOREIGN KEY([ClientApplicationId])
REFERENCES [dbo].[ClientApplication] ([ClientApplicationId])
GO

ALTER TABLE [dbo].[ClientApplicationTenant] CHECK CONSTRAINT [FK_ClientApplicationOrganization_ClientApplication]
GO

ALTER TABLE [dbo].[ClientApplicationTenant]  WITH CHECK ADD  CONSTRAINT [FK_ClientApplicationTenant_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO

ALTER TABLE [dbo].[ClientApplicationTenant] CHECK CONSTRAINT [FK_ClientApplicationTenant_Tenant]
GO
