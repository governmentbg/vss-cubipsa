
ALTER TABLE [dbo].[Indexes] WITH CHECK ADD CONSTRAINT [FK_Indexes_Routes] FOREIGN KEY([RouteId])
REFERENCES [dbo].[Routes] ([RouteId])
GO
ALTER TABLE [dbo].[Indexes] CHECK CONSTRAINT [FK_Indexes_Routes]
GO

ALTER TABLE [dbo].[Sitemaps] WITH CHECK ADD CONSTRAINT [FK_Sitemaps_Indexes] FOREIGN KEY([IndexId])
REFERENCES [dbo].[Indexes] ([IndexId])
GO
ALTER TABLE [dbo].[Sitemaps] CHECK CONSTRAINT [FK_Sitemaps_Indexes]
GO

CREATE NONCLUSTERED INDEX [IX_Date]
ON [dbo].[Routes] ([Date]);
GO