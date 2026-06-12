CREATE TABLE [dbo].[Permissions](
	[Id] [uniqueidentifier] NOT NULL,
	[ResourceName] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
)
GO