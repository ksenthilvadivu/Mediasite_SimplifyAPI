/****** Object:  Table [dbo].[ProviderAcceptedTOS]    Script Date: 7/27/2017 10:48:45 AM ******/

CREATE TABLE [dbo].[ProviderTOS](
	[TermsOfServiceId] [int] NOT NULL,
	[ProviderId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProviderTOS] PRIMARY KEY CLUSTERED 
(
	[TermsOfServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProviderTOS]  WITH CHECK ADD  CONSTRAINT [FK_ProviderTOS_Provider] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Provider] ([ProviderId])
GO

ALTER TABLE [dbo].[ProviderTOS] CHECK CONSTRAINT [FK_ProviderTOS_Provider]
GO

ALTER TABLE [dbo].[ProviderTOS]  WITH CHECK ADD  CONSTRAINT [FK_ProviderTOS_TermsOfService] FOREIGN KEY([TermsOfServiceId])
REFERENCES [dbo].[TermsOfService] ([TermsOfServiceId])
GO

ALTER TABLE [dbo].[ProviderTOS] CHECK CONSTRAINT [FK_ProviderTOS_TermsOfService]
GO


