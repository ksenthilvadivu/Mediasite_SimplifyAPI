/****** Object:  Table [dbo].[EmailValidation]    Script Date: 7/27/2017 10:29:13 AM ******/


CREATE TABLE [dbo].[EmailValidation](
	[EmailValidationId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[InvitedBy] [nvarchar](50) NULL,
	[IssedDate] [datetime2](7) NOT NULL,
	[ProviderId] UNIQUEIDENTIFIER NOT NULL,
	[EmailValidationDate] [datetime2](7) NULL,
 CONSTRAINT [PK_EmailValidation] PRIMARY KEY CLUSTERED 
(
	[EmailValidationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EmailValidation]  WITH CHECK ADD  CONSTRAINT [FK_EmailValidation_Provider] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Provider] ([ProviderId])
GO

ALTER TABLE [dbo].[EmailValidation] CHECK CONSTRAINT [FK_EmailValidation_Provider]
GO


