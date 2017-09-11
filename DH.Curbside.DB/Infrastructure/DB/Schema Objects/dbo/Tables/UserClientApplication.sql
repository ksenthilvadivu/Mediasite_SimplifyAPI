--Updated on : 05-Aug-2017

CREATE TABLE [dbo].[UserClientApplication](
	[UserClientApplicationId] [int] IDENTITY(1,1) NOT NULL,
	[DeProvisionedDate] [datetime2](7) NULL,
	[WhitelistedDate] [datetime2](7) NULL,	
	[UserId] [int] NOT NULL,
	[ClientApplicationId] [int] NULL,
	[InvitationDate]	[datetime2](7) NULL,
	[ReinviteDate ]	[datetime2](7) NULL,
	[UpdatedBy] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserClientApplication] PRIMARY KEY CLUSTERED 
(
	[UserClientApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserClientApplication]  WITH CHECK ADD  CONSTRAINT [FK_UserClientApplication_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO

ALTER TABLE [dbo].[UserClientApplication] CHECK CONSTRAINT [FK_UserClientApplication_User]
GO

ALTER TABLE [dbo].[UserClientApplication] WITH CHECK
ADD CONSTRAINT UserClientApplication_ClientApplication FOREIGN KEY ( ClientApplicationId ) REFERENCES ClientApplication(ClientApplicationId)
GO