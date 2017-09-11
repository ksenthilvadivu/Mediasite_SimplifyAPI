/****** Object:  Table [dbo].[ProviderRole]    Script Date: 7/27/2017 10:50:12 AM ******/

CREATE TABLE [dbo].[ProviderRole](
	[ProviderId] [uniqueidentifier] NOT NULL,
	[RoleId] [int] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProviderRole]  WITH CHECK ADD  CONSTRAINT [FK_ProviderRole_Provider] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Provider] ([ProviderId])
GO

ALTER TABLE [dbo].[ProviderRole] CHECK CONSTRAINT [FK_ProviderRole_Provider]
GO

ALTER TABLE [dbo].[ProviderRole]  WITH CHECK ADD  CONSTRAINT [FK_ProviderRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO

ALTER TABLE [dbo].[ProviderRole] CHECK CONSTRAINT [FK_ProviderRole_Role]
GO


