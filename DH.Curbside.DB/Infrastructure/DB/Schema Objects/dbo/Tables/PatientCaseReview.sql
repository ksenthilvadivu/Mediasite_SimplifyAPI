/****** Object:  Table [dbo].[CaseReview]    Script Date: 7/27/2017 10:27:30 AM ******/


CREATE TABLE [dbo].[PatientCaseReview](
	[PatientCaseReviewId] [int] IDENTITY(1,1) NOT NULL,
	[PatientCaseId] [int] NOT NULL,
	[ReviewerProviderId] [int] NOT NULL,
	[ReviewNotes] [text] NOT NULL,
	[DiagnosisCategory] [nvarchar](100) NOT NULL,
	[ReviewStatus] [nvarchar](50) NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PatientCaseReview] PRIMARY KEY CLUSTERED 
(
	[PatientCaseReviewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[PatientCaseReview]  WITH CHECK ADD  CONSTRAINT [FK_PatientCaseReview_Case] FOREIGN KEY([PatientCaseId])
REFERENCES [dbo].[PatientCase] ([PatientCaseId])
GO

ALTER TABLE [dbo].[PatientCaseReview] CHECK CONSTRAINT [FK_PatientCaseReview_Case]
GO


