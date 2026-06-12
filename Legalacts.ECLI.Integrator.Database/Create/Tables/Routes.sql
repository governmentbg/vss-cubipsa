
CREATE TABLE [dbo].[Routes](
	[RouteId] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL
 CONSTRAINT [PK_Routes] PRIMARY KEY ([RouteId])
)
GO