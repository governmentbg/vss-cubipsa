SET IDENTITY_INSERT [dbo].[SendToDocumentKinds] ON

INSERT INTO [dbo].[SendToDocumentKinds] ([SendToDocumentKindId], [Name], [IsActive]) VALUES (7001,N'Писмо',1);
INSERT INTO [dbo].[SendToDocumentKinds] ([SendToDocumentKindId], [Name], [IsActive]) VALUES (7002,N'Писмо - молба за опр. срок при бавност',1);
INSERT INTO [dbo].[SendToDocumentKinds] ([SendToDocumentKindId], [Name], [IsActive]) VALUES (7003,N'Писмо - предложение за възобновяване',1);
INSERT INTO [dbo].[SendToDocumentKinds] ([SendToDocumentKindId], [Name], [IsActive]) VALUES (7004,N'Писмо за изпращане по подсъдност',1);
INSERT INTO [dbo].[SendToDocumentKinds] ([SendToDocumentKindId], [Name], [IsActive]) VALUES (7005,N'Писмо - искане за опр. на компетентен съд',1);

SET IDENTITY_INSERT [dbo].[SendToDocumentKinds] OFF
GO