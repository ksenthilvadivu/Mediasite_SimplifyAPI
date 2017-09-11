
CREATE TABLE [dbo].[WhitelistUser](
	[WhitelistUserId] [int] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[DeactivatedDate] [datetime2](7) NULL,
	CONSTRAINT [PK_WhitelistUser] PRIMARY KEY CLUSTERED ([WhitelistUserId] ASC),
	CONSTRAINT [UK_WhitelistUser_Email] UNIQUE ([Email])
);