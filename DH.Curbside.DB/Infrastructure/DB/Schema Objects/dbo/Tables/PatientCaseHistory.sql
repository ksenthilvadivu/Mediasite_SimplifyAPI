/****** Object:  Table [dbo].[CaseHistory]    Script Date: 7/27/2017 10:21:25 AM ******/


CREATE TABLE [dbo].[PatientCaseHistory](
	[PatientCaseHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[PatientCaseId] [int] NOT NULL,
	[CaseStatusId] [int] NOT NULL,
	[CreatedOn] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_PatientCaseHistory] PRIMARY KEY CLUSTERED 
(
	[PatientCaseHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PatientCaseHistory]  WITH CHECK ADD  CONSTRAINT [FK_PatientCaseHistory_Case] FOREIGN KEY([PatientCaseId])
REFERENCES [dbo].[PatientCase] ([PatientCaseId])
GO

ALTER TABLE [dbo].[PatientCaseHistory] CHECK CONSTRAINT [FK_PatientCaseHistory_Case]
GO

ALTER TABLE [dbo].[PatientCaseHistory]  WITH CHECK ADD  CONSTRAINT [FK_PatientCaseHistory_Status] FOREIGN KEY([CaseStatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO

ALTER TABLE [dbo].[PatientCaseHistory] CHECK CONSTRAINT [FK_PatientCaseHistory_Status]
GO


