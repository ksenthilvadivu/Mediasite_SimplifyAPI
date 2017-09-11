/****** Object:  Table [dbo].[Provider]    Script Date: 7/27/2017 10:40:39 AM ******/


CREATE TABLE [dbo].[Provider](
	[ProviderId] [uniqueidentifier] NOT NULL,
	[TenantId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[BIO] [nvarchar](2000) NULL,
	[Speciality] [nvarchar](500) NULL,
	[Picture] [image] NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[LoginUserName] [nvarchar](50) NULL,
	[LoginSourceId] [nvarchar](50) NULL,
	[LoginSourceSystem] [nvarchar](50) NOT NULL,
	[LoginDeactivatedDate] [datetime2](7) NULL,
	[CreatedOn] [datetime2](7) NULL,
	[UpdatedOn] [datetime2](7) NULL,
	
 CONSTRAINT [PK_Provider_1] PRIMARY KEY CLUSTERED 
(
	[ProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Provider]  WITH CHECK ADD  CONSTRAINT [FK_Provider_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO

ALTER TABLE [dbo].[Provider] CHECK CONSTRAINT [FK_Provider_Tenant]
GO


