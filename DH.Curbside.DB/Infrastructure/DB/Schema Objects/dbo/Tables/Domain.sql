

CREATE TABLE [dbo].[Domain](
	[DomainId] [int] IDENTITY(1,1) NOT NULL,
	[DomainName] [nvarchar](200) NOT NULL,
	[TenantId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Domain] PRIMARY KEY CLUSTERED 
(
	[DomainId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Domain]  WITH CHECK ADD  CONSTRAINT [FK_Domain_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO

ALTER TABLE [dbo].[Domain] CHECK CONSTRAINT [FK_Domain_Tenant]
GO


