CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Prefix] [nvarchar](10) NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](200) NOT NULL,
	[EmailAddress] [nvarchar](200) NOT NULL,
	[TenantId] [uniqueidentifier] NULL,
	[CreatedOn]           DATETIME2 (7)  NOT NULL,
    [CreatedBy]           NVARCHAR (50)  NOT NULL,
CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Tenant]
GO


