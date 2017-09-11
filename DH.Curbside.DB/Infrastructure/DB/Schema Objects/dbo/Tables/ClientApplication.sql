/****** Object:  Table [dbo].[ClientApplication]    Script Date: 7/27/2017 10:28:04 AM ******/

CREATE TABLE [dbo].[ClientApplication](
	[ClientApplicationId] [int] IDENTITY(1,1) NOT NULL,
	[ClientApplicationToken] [uniqueidentifier] NOT NULL,
	[ClientApplicationName] [nvarchar](100) NOT NULL,
	[IsMobile] [bit] NOT NULL,
	[MinimumVersion] [nvarchar](10) NULL,
	[LatestSupportedVersion] [nvarchar](10) NULL,
	[UpgradeUrl] [nvarchar](500) NULL,
 CONSTRAINT [PK_ClientApplication] PRIMARY KEY CLUSTERED 
(
	[ClientApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ClientApplication] ADD  CONSTRAINT [DF_ClientApplication_IsMobile]  DEFAULT ((0)) FOR [IsMobile]
GO