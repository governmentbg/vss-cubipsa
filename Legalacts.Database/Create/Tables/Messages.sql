CREATE TABLE [dbo].[Messages] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Recipient]         NVARCHAR (500) NOT NULL,
    [Subject]           NVARCHAR (MAX) NULL,
    [Body]		        NVARCHAR (MAX) NOT NULL,
	[IsBodyHtml]		BIT			   NOT NULL,
    [SentDate]          DATETIME       NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([Id] ASC),
);

