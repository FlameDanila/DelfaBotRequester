USE [TelegramBotData]
GO
/****** Object:  Table [dbo].[usersRequest]    Script Date: 20.06.2022 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usersRequest](
	[Block] [int] NULL,
	[counter] [int] NULL,
	[id] [nvarchar](50) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[usersRequest] ([Block], [counter], [id]) VALUES (3, 4, N'654110382')
INSERT [dbo].[usersRequest] ([Block], [counter], [id]) VALUES (0, 0, N'732980706')
INSERT [dbo].[usersRequest] ([Block], [counter], [id]) VALUES (0, 0, N'995734455')
GO
