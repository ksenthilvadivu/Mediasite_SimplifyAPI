
CREATE TABLE [dbo].[DomainClientApplication](
	[DomainClientApplicationId] [int] IDENTITY(1,1) NOT NULL,
	[DomainId] [int] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[DeProvisionedDate] [datetime2](7) NULL,
	[ClientApplicationId] [int] NULL,
 CONSTRAINT [PK_DomainClientApplication] PRIMARY KEY CLUSTERED 
(
	[DomainClientApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DomainClientApplication]  WITH CHECK ADD  CONSTRAINT [FK_DomainClientApplication_Domain] FOREIGN KEY([DomainId])
REFERENCES [dbo].[Domain] ([DomainId])
GO

ALTER TABLE [dbo].[DomainClientApplication] CHECK CONSTRAINT [FK_DomainClientApplication_Domain]
GO

ALTER TABLE [dbo].[DomainClientApplication] WITH CHECK
ADD CONSTRAINT DomainClientApplication_ClientApplication FOREIGN KEY ( ClientApplicationId ) REFERENCES ClientApplication(ClientApplicationId)
GO
