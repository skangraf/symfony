USE [PZ_Projekt]
GO

ALTER TABLE [dbo].[PZUsers] DROP CONSTRAINT [DF_PZUsers_status]
GO

/****** Object:  Table [dbo].[PZUsers]    Script Date: 11.01.2020 01:52:38 ******/
DROP TABLE [dbo].[PZUsers]
GO

/****** Object:  Table [dbo].[PZUsers]    Script Date: 11.01.2020 01:52:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PZUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[userid] [varchar](255) NULL,
	[email] [varchar](255) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[imie] [varchar](255) NULL,
	[nazwisko] [varchar](255) NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_PZUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PZUsers] ADD  CONSTRAINT [DF_PZUsers_status]  DEFAULT ((1)) FOR [status]
GO
