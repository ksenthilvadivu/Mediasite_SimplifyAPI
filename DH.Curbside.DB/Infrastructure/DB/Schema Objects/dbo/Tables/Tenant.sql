/****** Object:  Table [dbo].[Tenant]    Script Date: 7/27/2017 10:59:45 AM ******/


CREATE TABLE [dbo].[Tenant](
	[TenantId] [uniqueidentifier] NOT NULL,
	[TenantName] [nvarchar](100) NOT NULL,

 CONSTRAINT [PK_Tenant] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


