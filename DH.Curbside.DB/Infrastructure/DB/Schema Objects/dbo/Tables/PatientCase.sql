CREATE TABLE [dbo].[PatientCase](
	[PatientCaseId] [int] IDENTITY(1,1) NOT NULL,
	[PatientCaseGuid] [uniqueidentifier] NOT NULL,
	[CaseTitle] [nvarchar](2000) NOT NULL,
	[CaseDescription] [text] NULL,
	[ProviderId] [uniqueidentifier] NOT NULL,
	[CaseStatus] [nvarchar](50) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PatientCase] PRIMARY KEY CLUSTERED 
(
	[PatientCaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[PatientCase]  WITH CHECK ADD  CONSTRAINT [FK_PatientCase_Provider] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Provider] ([ProviderId])
GO

ALTER TABLE [dbo].[PatientCase]  CHECK CONSTRAINT [FK_PatientCase_Provider]
GO
