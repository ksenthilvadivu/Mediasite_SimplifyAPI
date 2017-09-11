CREATE TABLE [dbo].[PatientCaseAttachment](
	[PatientCaseAttachmentId] [int] IDENTITY(1,1) NOT NULL,
	[PatientCaseId] [int] NOT NULL,
	[MediaAttachmentId] [int] NOT NULL,
 CONSTRAINT [PK_PatientCaseAttachment] PRIMARY KEY CLUSTERED 
(
	[PatientCaseAttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PatientCaseAttachment]  WITH CHECK ADD  CONSTRAINT [FK_PatientCaseAttachment_MediaAttachment] FOREIGN KEY([MediaAttachmentId])
REFERENCES [dbo].[MediaAttachment] ([MediaAttachmentId])
GO

ALTER TABLE [dbo].[PatientCaseAttachment] CHECK CONSTRAINT [FK_PatientCaseAttachment_MediaAttachment]
GO

ALTER TABLE [dbo].[PatientCaseAttachment]  WITH CHECK ADD  CONSTRAINT [FK_MedicalCaseAttachnment_MedicalCase] FOREIGN KEY([PatientCaseId])
REFERENCES [dbo].[PatientCase] ([PatientCaseId])
GO

ALTER TABLE [dbo].[MedicalCaseAttachnment] CHECK CONSTRAINT [FK_MedicalCaseAttachnment_MedicalCase]
GO
