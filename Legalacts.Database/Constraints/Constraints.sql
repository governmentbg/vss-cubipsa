ALTER TABLE [dbo].[ActionLogTypes] ADD  CONSTRAINT [DF_ActionLogTypes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ActKinds] ADD  CONSTRAINT [DF__ActKinds__IsActi__30F848ED]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AppealKinds] ADD  CONSTRAINT [DF__AppealKin__IsAct__33D4B598]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CaseKinds] ADD  CONSTRAINT [DF__CaseKinds__IsAct__36B12243]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ConnectedKinds] ADD  CONSTRAINT [DF__ConnectKi__IsAct__398D8EEE]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ConnectedTypes] ADD  CONSTRAINT [DF__ConnectTy__IsAct__3C69FB99]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Courts] ADD  CONSTRAINT [DF__Courts__IsActive__3F466844]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[IndocKinds] ADD  CONSTRAINT [DF__IndocKind__IsAct__4222D4EF]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Involvements] ADD  CONSTRAINT [DF__Involveme__IsAct__44FF419A]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Links] ADD  CONSTRAINT [DF__Links__IsActive__47DBAE45]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ResultsOfAppeals] ADD  CONSTRAINT [DF__ResultsOf__IsAct__5070F446]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SendToDocumentKinds] ADD  CONSTRAINT [DF__SendToKin__IsAct__4AB81AF0]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Statuses] ADD  CONSTRAINT [DF__Statuses__IsActi__4D94879B]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_Acts_ActDocuments] FOREIGN KEY([ActDocumentId])
REFERENCES [dbo].[ActDocuments] ([ActDocumentId])
GO
ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_Acts_ActDocuments]
GO
ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_Acts_ActKinds] FOREIGN KEY([ActKindId])
REFERENCES [dbo].[ActKinds] ([ActKindId])
GO
ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_Acts_ActKinds]
GO
ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_Acts_CaseKinds] FOREIGN KEY([CaseKindId])
REFERENCES [dbo].[CaseKinds] ([CaseKindId])
GO
ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_Acts_CaseKinds]
GO
ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_Acts_Courts] FOREIGN KEY([CourtId])
REFERENCES [dbo].[Courts] ([CourtId])
GO
ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_Acts_Courts]
GO
ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_Acts_HigherCourts] FOREIGN KEY([HigherCourtId])
REFERENCES [dbo].[HigherCourts] ([HigherCourtId])
GO
ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_Acts_HigherCourts]
GO
ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_Acts_MotiveDocuments] FOREIGN KEY([MotiveDocumentId])
REFERENCES [dbo].[MotiveDocuments] ([MotiveDocumentId])
GO
ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_Acts_MotiveDocuments]
GO
ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_Acts_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([StatusId])
GO
ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_Acts_Statuses]
GO
ALTER TABLE [dbo].[ConnectedActs]  WITH CHECK ADD  CONSTRAINT [FK_ConnectedActs_Acts2] FOREIGN KEY([ActId])
REFERENCES [dbo].[Acts] ([ActId])
GO
ALTER TABLE [dbo].[ConnectedActs] CHECK CONSTRAINT [FK_ConnectedActs_Acts2]
GO
ALTER TABLE [dbo].[ConnectedActs]  WITH CHECK ADD  CONSTRAINT [FK_ConnectedActs_Acts3] FOREIGN KEY([ConnectedActId])
REFERENCES [dbo].[Acts] ([ActId])
GO
ALTER TABLE [dbo].[ConnectedActs] CHECK CONSTRAINT [FK_ConnectedActs_Acts3]
GO
ALTER TABLE [dbo].[ConnectedCases]  WITH CHECK ADD  CONSTRAINT [FK_ConnectedCases_ActKinds] FOREIGN KEY([AppealActKindId])
REFERENCES [dbo].[ActKinds] ([ActKindId])
GO
ALTER TABLE [dbo].[ConnectedCases] CHECK CONSTRAINT [FK_ConnectedCases_ActKinds]
GO
ALTER TABLE [dbo].[ConnectedCases]  WITH CHECK ADD  CONSTRAINT [FK_ConnectedCases_Acts] FOREIGN KEY([ActId])
REFERENCES [dbo].[Acts] ([ActId])
GO
ALTER TABLE [dbo].[ConnectedCases] CHECK CONSTRAINT [FK_ConnectedCases_Acts]
GO
ALTER TABLE [dbo].[ConnectedCases]  WITH CHECK ADD  CONSTRAINT [FK_ConnectedCases_ConnectedKinds] FOREIGN KEY([ConnectedKindId])
REFERENCES [dbo].[ConnectedKinds] ([ConnectedKindId])
GO
ALTER TABLE [dbo].[ConnectedCases] CHECK CONSTRAINT [FK_ConnectedCases_ConnectedKinds]
GO
ALTER TABLE [dbo].[ConnectedCases]  WITH CHECK ADD  CONSTRAINT [FK_ConnectedCases_ConnectedTypes] FOREIGN KEY([ConnectedTypeId])
REFERENCES [dbo].[ConnectedTypes] ([ConnectedTypeId])
GO
ALTER TABLE [dbo].[ConnectedCases] CHECK CONSTRAINT [FK_ConnectedCases_ConnectedTypes]
GO
ALTER TABLE [dbo].[HigherCourts]  WITH CHECK ADD  CONSTRAINT [FK_HigherCourts_Courts] FOREIGN KEY([CourtId])
REFERENCES [dbo].[Courts] ([CourtId])
GO
ALTER TABLE [dbo].[HigherCourts] CHECK CONSTRAINT [FK_HigherCourts_Courts]
GO
ALTER TABLE [dbo].[HigherCourts]  WITH CHECK ADD  CONSTRAINT [FK_HigherCourts_SendToDocumentKinds] FOREIGN KEY([SendToDocumentKindId])
REFERENCES [dbo].[SendToDocumentKinds] ([SendToDocumentKindId])
GO
ALTER TABLE [dbo].[HigherCourts] CHECK CONSTRAINT [FK_HigherCourts_SendToDocumentKinds]
GO
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD  CONSTRAINT [FK_Logs_ActionLogTypes] FOREIGN KEY([ActionLogTypeId])
REFERENCES [dbo].[ActionLogTypes] ([ActionLogTypeId])
GO
ALTER TABLE [dbo].[Logs] CHECK CONSTRAINT [FK_Logs_ActionLogTypes]
GO
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD  CONSTRAINT [FK_Logs_ActKinds] FOREIGN KEY([ActKindId])
REFERENCES [dbo].[ActKinds] ([ActKindId])
GO
ALTER TABLE [dbo].[Logs] CHECK CONSTRAINT [FK_Logs_ActKinds]
GO
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD  CONSTRAINT [FK_Logs_Courts] FOREIGN KEY([CourtId])
REFERENCES [dbo].[Courts] ([CourtId])
GO
ALTER TABLE [dbo].[Logs] CHECK CONSTRAINT [FK_Logs_Courts]
GO
ALTER TABLE [dbo].[RolesPermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolesPermissions_Permissions] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([Id])
GO
ALTER TABLE [dbo].[RolesPermissions] CHECK CONSTRAINT [FK_RolesPermissions_Permissions]
GO
ALTER TABLE [dbo].[RolesPermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolesPermissions_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[RolesPermissions] CHECK CONSTRAINT [FK_RolesPermissions_Roles]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Roles]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Users]
GO
