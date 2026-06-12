SET IDENTITY_INSERT [dbo].[Statuses] ON

INSERT INTO [dbo].[Statuses] ([StatusId],[Name],[IsActive]) VALUES (1,N'Влязъл в сила',1);
INSERT INTO [dbo].[Statuses] ([StatusId],[Name],[IsActive]) VALUES (2,N'Не е влязъл в сила',1);
INSERT INTO [dbo].[Statuses] ([StatusId],[Name],[IsActive]) VALUES (3,N'Отменен',1);
INSERT INTO [dbo].[Statuses] ([StatusId],[Name],[IsActive]) VALUES (4,N'Изменен',1);
INSERT INTO [dbo].[Statuses] ([StatusId],[Name],[IsActive]) VALUES (5,N'Потвърден',1);

SET IDENTITY_INSERT [dbo].[Statuses] OFF
GO