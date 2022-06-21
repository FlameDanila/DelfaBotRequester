USE [TelegramBotData]
GO
/****** Object:  Table [dbo].[usersRequest]    Script Date: 21.06.2022 14:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usersRequest](
	[Block] [int] NULL,
	[counter] [int] NULL,
	[id] [nvarchar](50) NULL,
	[selectedProfession] [nvarchar](50) NULL,
	[phoneNumber] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
	[ansver] [nvarchar](150) NULL,
	[score] [nvarchar](50) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[usersRequest] ([Block], [counter], [id], [selectedProfession], [phoneNumber], [name], [ansver], [score]) VALUES (3, 4, N'654110382', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[usersRequest] ([Block], [counter], [id], [selectedProfession], [phoneNumber], [name], [ansver], [score]) VALUES (0, 0, N'732980706', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[usersRequest] ([Block], [counter], [id], [selectedProfession], [phoneNumber], [name], [ansver], [score]) VALUES (0, 0, N'995734455', N'Аренда конференц зала??', NULL, NULL, N'dddd', N'?????')
GO
