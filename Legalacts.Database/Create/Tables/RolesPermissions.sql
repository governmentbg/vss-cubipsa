CREATE TABLE [dbo].[RolesPermissions](
	[RoleId] [uniqueidentifier] NOT NULL,
	[PermissionId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RolesPermissions] PRIMARY KEY ([RoleId], [PermissionId])
)
GO