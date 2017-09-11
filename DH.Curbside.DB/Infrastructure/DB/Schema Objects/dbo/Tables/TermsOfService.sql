/****** Object:  Table [dbo].[TermsOfService]    Script Date: 7/27/2017 11:03:36 AM ******/


CREATE TABLE [dbo].[TermsOfService](
	[TermsOfServiceId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [text] NOT NULL,
 CONSTRAINT [PK_TermsOfService] PRIMARY KEY CLUSTERED 
(
	[TermsOfServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


