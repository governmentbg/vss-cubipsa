SET IDENTITY_INSERT [dbo].[ActionLogTypes] ON

INSERT INTO [dbo].[ActionLogTypes] ([ActionLogTypeId], [Name], [IsActive]) VALUES (1,N'Редактиране',1);
INSERT INTO [dbo].[ActionLogTypes] ([ActionLogTypeId], [Name], [IsActive]) VALUES (2,N'Добавяне',1);
INSERT INTO [dbo].[ActionLogTypes] ([ActionLogTypeId], [Name], [IsActive]) VALUES (3,N'Изтриване',1);

SET IDENTITY_INSERT [dbo].[ActionLogTypes] OFF
GO
