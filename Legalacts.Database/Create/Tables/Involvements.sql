CREATE TABLE [dbo].[Involvements](
	[InvolvementId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Involvements] PRIMARY KEY ([InvolvementId])
)
GO