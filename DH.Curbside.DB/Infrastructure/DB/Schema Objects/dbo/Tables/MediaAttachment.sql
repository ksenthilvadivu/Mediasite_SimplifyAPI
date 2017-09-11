CREATE TABLE [dbo].[MediaAttachment](
	[MediaAttachmentId] [int] IDENTITY(1,1) NOT NULL,
	[AttachmentThumbnail] [image] NULL,
	[AttachmentSourceId] [nvarchar](50) NULL,
	[AttachmentSourceSystem] [nvarchar](50) NULL,
	[AttachmentURL] [nvarchar](500) NULL,
 CONSTRAINT [PK_MediaAttachment] PRIMARY KEY CLUSTERED 
(
	[MediaAttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


