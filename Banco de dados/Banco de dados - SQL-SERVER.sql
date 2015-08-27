USE [master]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 08/27/2015 05:19:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[RegDate] [datetime] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[usuario] ON
INSERT [dbo].[usuario] ([Id], [Username], [Password], [RegDate], [Email]) VALUES (1, N'admin', N'2e6f9b0d5885b6010f9167787445617f553a735f', CAST(0x0000A4FE0171A2B0 AS DateTime), N'test@test.test')
INSERT [dbo].[usuario] ([Id], [Username], [Password], [RegDate], [Email]) VALUES (2, N'user', N'2e6f9b0d5885b6010f9167787445617f553a735f', CAST(0x0000A500011A008E AS DateTime), N'user@user.com')
INSERT [dbo].[usuario] ([Id], [Username], [Password], [RegDate], [Email]) VALUES (7, N'user2', N'2e6f9b0d5885b6010f9167787445617f553a735f', CAST(0x0000A501004353A9 AS DateTime), N'usuario@teste.teste')
SET IDENTITY_INSERT [dbo].[usuario] OFF
/****** Object:  Table [dbo].[transportadora]    Script Date: 08/27/2015 05:19:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transportadora](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Telefone] [nvarchar](50) NOT NULL,
	[Endereco] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[InscricaoEstadual] [int] NOT NULL,
	[Cnpj] [nvarchar](21) NOT NULL,
 CONSTRAINT [PK_transportadora] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[transportadora] ON
INSERT [dbo].[transportadora] ([Codigo], [Nome], [Telefone], [Endereco], [Email], [InscricaoEstadual], [Cnpj]) VALUES (5, N'Hélio Transportes', N'(67)9951-4221', N'Rua Ivinhema, 3705 Vila Esperança                 ', N'leonardoaquinoneto@gmail.com  ', 8984, N'81.825.565/0001-00')
INSERT [dbo].[transportadora] ([Codigo], [Nome], [Telefone], [Endereco], [Email], [InscricaoEstadual], [Cnpj]) VALUES (6, N'NETO Transportes', N'(67)9925-0331', N'Rua Ivinhema, 3705 Vila Esperança', N'tete@teste.com', 8944, N'28.923.428/0001-25')
SET IDENTITY_INSERT [dbo].[transportadora] OFF
/****** Object:  Table [dbo].[usuario_avaliacao]    Script Date: 08/27/2015 05:19:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario_avaliacao](
	[Usuario_id] [int] NOT NULL,
	[Transportadora_id] [int] NULL,
	[Avaliacao] [int] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[usuario_avaliacao] ([Usuario_id], [Transportadora_id], [Avaliacao]) VALUES (2, 5, 0)
INSERT [dbo].[usuario_avaliacao] ([Usuario_id], [Transportadora_id], [Avaliacao]) VALUES (7, 4, 1)
INSERT [dbo].[usuario_avaliacao] ([Usuario_id], [Transportadora_id], [Avaliacao]) VALUES (7, 5, 0)
INSERT [dbo].[usuario_avaliacao] ([Usuario_id], [Transportadora_id], [Avaliacao]) VALUES (2, 4, 3)
/****** Object:  Table [dbo].[permissao]    Script Date: 08/27/2015 05:19:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permissao](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NULL,
	[Usuario_id] [int] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[permissao] ON
INSERT [dbo].[permissao] ([Codigo], [Nome], [Usuario_id]) VALUES (1, N'Administrador', 1)
INSERT [dbo].[permissao] ([Codigo], [Nome], [Usuario_id]) VALUES (2, N'Usuario', 2)
INSERT [dbo].[permissao] ([Codigo], [Nome], [Usuario_id]) VALUES (3, N'Usuario', 7)
SET IDENTITY_INSERT [dbo].[permissao] OFF
/****** Object:  Default [DF__System_Us__RegDa__4EDDB18F]    Script Date: 08/27/2015 05:19:45 ******/
ALTER TABLE [dbo].[usuario] ADD  DEFAULT (getdate()) FOR [RegDate]
GO
/****** Object:  ForeignKey [FK_usuario_avaliacao_transportadora]    Script Date: 08/27/2015 05:19:45 ******/
ALTER TABLE [dbo].[usuario_avaliacao]  WITH NOCHECK ADD  CONSTRAINT [FK_usuario_avaliacao_transportadora] FOREIGN KEY([Transportadora_id])
REFERENCES [dbo].[transportadora] ([Codigo])
GO
ALTER TABLE [dbo].[usuario_avaliacao] NOCHECK CONSTRAINT [FK_usuario_avaliacao_transportadora]
GO
/****** Object:  ForeignKey [FK_usuario_avaliacao_usuario]    Script Date: 08/27/2015 05:19:45 ******/
ALTER TABLE [dbo].[usuario_avaliacao]  WITH CHECK ADD  CONSTRAINT [FK_usuario_avaliacao_usuario] FOREIGN KEY([Usuario_id])
REFERENCES [dbo].[usuario] ([Id])
GO
ALTER TABLE [dbo].[usuario_avaliacao] CHECK CONSTRAINT [FK_usuario_avaliacao_usuario]
GO
/****** Object:  ForeignKey [FK_permissao_usuario]    Script Date: 08/27/2015 05:19:45 ******/
ALTER TABLE [dbo].[permissao]  WITH CHECK ADD  CONSTRAINT [FK_permissao_usuario] FOREIGN KEY([Usuario_id])
REFERENCES [dbo].[usuario] ([Id])
GO
ALTER TABLE [dbo].[permissao] CHECK CONSTRAINT [FK_permissao_usuario]
GO
