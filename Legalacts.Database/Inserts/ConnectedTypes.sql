SET IDENTITY_INSERT [dbo].[ConnectedTypes] ON

INSERT INTO [dbo].[ConnectedTypes] ([ConnectedTypeId], [Name], [IsActive]) VALUES (3001,N'Дело по подсъдност',1);
INSERT INTO [dbo].[ConnectedTypes] ([ConnectedTypeId], [Name], [IsActive]) VALUES (3002,N'Първоинстанционно дело',1);
INSERT INTO [dbo].[ConnectedTypes] ([ConnectedTypeId], [Name], [IsActive]) VALUES (3003,N'Въззивно дело',1);
INSERT INTO [dbo].[ConnectedTypes] ([ConnectedTypeId], [Name], [IsActive]) VALUES (3004,N'Дело на висша инстанция',1);
INSERT INTO [dbo].[ConnectedTypes] ([ConnectedTypeId], [Name], [IsActive]) VALUES (3005,N'Дело - конфликт на интереси',1);
INSERT INTO [dbo].[ConnectedTypes] ([ConnectedTypeId], [Name], [IsActive]) VALUES (3006,N'Дело – препирня по подсъдност',1);

SET IDENTITY_INSERT [dbo].[ConnectedTypes] OFF
GO