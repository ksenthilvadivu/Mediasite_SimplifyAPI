CREATE TABLE [dbo].[WhitelistUserStatus](
	[WhiteListUserStatusId] [int] NOT NULL,
	[StatusName] [nvarchar](2000) NULL,
	[StatusDescription] [nvarchar](2000) NULL,
 CONSTRAINT [PK_WhitelistUserStatus] PRIMARY KEY CLUSTERED 
(
	[WhiteListUserStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
