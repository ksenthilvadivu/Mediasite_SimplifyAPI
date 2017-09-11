/****** Object:  Table [dbo].[PhoneValidation]    Script Date: 7/27/2017 10:35:48 AM ******/

CREATE TABLE [dbo].[PhoneValidation](
	[PhoneValidationId] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[SMSCode] [nvarchar](10) NOT NULL,
	[SMSValidationDate] [datetime2](7) NULL,
	[IssueDate] [datetime2](7) NOT NULL,
	[ProviderId] [uniqueidentifier] NOT NULL,
	[EmailValidationId] [int] NOT NULL,
 CONSTRAINT [PK_PhoneValidation] PRIMARY KEY CLUSTERED 
(
	[PhoneValidationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PhoneValidation]  WITH CHECK ADD  CONSTRAINT [FK_PhoneValidation_Provider] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Provider] ([ProviderId])
GO

ALTER TABLE [dbo].[PhoneValidation] CHECK CONSTRAINT [FK_PhoneValidation_Provider]
GO


