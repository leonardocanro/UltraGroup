USE [master]
GO
/****** Object:  Database [AgenciaViajes]    Script Date: 12/08/2024 19:53:41 ******/
CREATE DATABASE [AgenciaViajes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AgenciaViajes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.DATA\MSSQL\DATA\AgenciaViajes.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AgenciaViajes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.DATA\MSSQL\DATA\AgenciaViajes_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AgenciaViajes] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AgenciaViajes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AgenciaViajes] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AgenciaViajes] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AgenciaViajes] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AgenciaViajes] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AgenciaViajes] SET ARITHABORT OFF 
GO
ALTER DATABASE [AgenciaViajes] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AgenciaViajes] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AgenciaViajes] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AgenciaViajes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AgenciaViajes] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AgenciaViajes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AgenciaViajes] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AgenciaViajes] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AgenciaViajes] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AgenciaViajes] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AgenciaViajes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AgenciaViajes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AgenciaViajes] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AgenciaViajes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AgenciaViajes] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AgenciaViajes] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AgenciaViajes] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AgenciaViajes] SET RECOVERY FULL 
GO
ALTER DATABASE [AgenciaViajes] SET  MULTI_USER 
GO
ALTER DATABASE [AgenciaViajes] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AgenciaViajes] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AgenciaViajes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AgenciaViajes] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AgenciaViajes] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AgenciaViajes] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'AgenciaViajes', N'ON'
GO
ALTER DATABASE [AgenciaViajes] SET QUERY_STORE = OFF
GO
USE [AgenciaViajes]
GO
/****** Object:  Table [dbo].[ContactoEmergencia]    Script Date: 12/08/2024 19:53:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactoEmergencia](
	[ContactoEmergenciaID] [int] IDENTITY(1,1) NOT NULL,
	[ReservaID] [int] NOT NULL,
	[NombreCompleto] [varchar](255) NOT NULL,
	[TelefonoContacto] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ContactoEmergenciaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Habitacion]    Script Date: 12/08/2024 19:53:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Habitacion](
	[HabitacionID] [int] IDENTITY(1,1) NOT NULL,
	[HotelID] [int] NOT NULL,
	[Numero] [varchar](50) NOT NULL,
	[CostoBase] [decimal](10, 2) NOT NULL,
	[Impuestos] [decimal](10, 2) NOT NULL,
	[Tipo] [varchar](50) NULL,
	[Ubicacion] [varchar](100) NULL,
	[Activo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NULL,
	[FechaModificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[HabitacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotel]    Script Date: 12/08/2024 19:53:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotel](
	[HotelID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Descripcion] [text] NULL,
	[Direccion] [varchar](255) NULL,
	[Ciudad] [varchar](100) NULL,
	[Estado] [varchar](100) NULL,
	[Pais] [varchar](100) NULL,
	[Activo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NULL,
	[FechaModificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[HotelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Huesped]    Script Date: 12/08/2024 19:53:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Huesped](
	[HuespedID] [int] IDENTITY(1,1) NOT NULL,
	[ReservaID] [int] NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Apellido] [varchar](100) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Genero] [char](1) NULL,
	[TipoDocumento] [varchar](50) NOT NULL,
	[NumeroDocumento] [varchar](50) NOT NULL,
	[Email] [varchar](255) NULL,
	[TelefonoContacto] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[HuespedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reserva]    Script Date: 12/08/2024 19:53:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reserva](
	[ReservaID] [int] IDENTITY(1,1) NOT NULL,
	[HabitacionID] [int] NOT NULL,
	[FechaEntrada] [datetime] NOT NULL,
	[FechaSalida] [datetime] NOT NULL,
	[CantidadPersonas] [int] NOT NULL,
	[EstadoReserva] [varchar](50) NOT NULL,
	[FechaReserva] [datetime] NULL,
	[FechaModificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReservaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ContactoEmergencia] ON 

INSERT [dbo].[ContactoEmergencia] ([ContactoEmergenciaID], [ReservaID], [NombreCompleto], [TelefonoContacto]) VALUES (1, 1, N'YelRa', N'31365656')
INSERT [dbo].[ContactoEmergencia] ([ContactoEmergenciaID], [ReservaID], [NombreCompleto], [TelefonoContacto]) VALUES (2, 5, N'YelRa', N'3138450360')
INSERT [dbo].[ContactoEmergencia] ([ContactoEmergenciaID], [ReservaID], [NombreCompleto], [TelefonoContacto]) VALUES (3, 6, N'YelRa', N'3138450360')
SET IDENTITY_INSERT [dbo].[ContactoEmergencia] OFF
GO
SET IDENTITY_INSERT [dbo].[Habitacion] ON 

INSERT [dbo].[Habitacion] ([HabitacionID], [HotelID], [Numero], [CostoBase], [Impuestos], [Tipo], [Ubicacion], [Activo], [FechaCreacion], [FechaModificacion]) VALUES (1, 2, N'Habitacion 1', CAST(50000.00 AS Decimal(10, 2)), CAST(14.00 AS Decimal(10, 2)), N'Plena', N'Piso 1', 0, CAST(N'2024-08-09T22:23:16.383' AS DateTime), CAST(N'2024-08-09T22:23:16.383' AS DateTime))
INSERT [dbo].[Habitacion] ([HabitacionID], [HotelID], [Numero], [CostoBase], [Impuestos], [Tipo], [Ubicacion], [Activo], [FechaCreacion], [FechaModificacion]) VALUES (2, 2, N'Habitacion 2', CAST(5000.00 AS Decimal(10, 2)), CAST(32.00 AS Decimal(10, 2)), N'Estandar', N'Piso 2', 1, CAST(N'2024-08-10T00:17:03.820' AS DateTime), CAST(N'2024-08-10T00:17:03.820' AS DateTime))
INSERT [dbo].[Habitacion] ([HabitacionID], [HotelID], [Numero], [CostoBase], [Impuestos], [Tipo], [Ubicacion], [Activo], [FechaCreacion], [FechaModificacion]) VALUES (3, 1, N'Habitacion 1', CAST(50000.00 AS Decimal(10, 2)), CAST(19.00 AS Decimal(10, 2)), N'Premium', N'Piso 20', 1, CAST(N'2024-08-10T00:17:03.820' AS DateTime), CAST(N'2024-08-10T00:17:03.820' AS DateTime))
SET IDENTITY_INSERT [dbo].[Habitacion] OFF
GO
SET IDENTITY_INSERT [dbo].[Hotel] ON 

INSERT [dbo].[Hotel] ([HotelID], [Nombre], [Descripcion], [Direccion], [Ciudad], [Estado], [Pais], [Activo], [FechaCreacion], [FechaModificacion]) VALUES (1, N'LeopA', N'Bonito', N'carrera 6', N'Bogota', N'bueno', N'Colombia', 0, CAST(N'2024-08-08T23:47:05.723' AS DateTime), CAST(N'2024-08-08T23:47:05.723' AS DateTime))
INSERT [dbo].[Hotel] ([HotelID], [Nombre], [Descripcion], [Direccion], [Ciudad], [Estado], [Pais], [Activo], [FechaCreacion], [FechaModificacion]) VALUES (2, N'Decameron Isleño', N'buen hotel', N'av siempre viva', N'Bogota', N'1', N'Colombia', 1, CAST(N'2024-08-09T18:53:09.913' AS DateTime), CAST(N'2024-08-09T18:53:09.913' AS DateTime))
SET IDENTITY_INSERT [dbo].[Hotel] OFF
GO
SET IDENTITY_INSERT [dbo].[Huesped] ON 

INSERT [dbo].[Huesped] ([HuespedID], [ReservaID], [Nombre], [Apellido], [FechaNacimiento], [Genero], [TipoDocumento], [NumeroDocumento], [Email], [TelefonoContacto]) VALUES (1, 1, N'Nelson', N'Canro', CAST(N'2024-08-09' AS Date), N'M', N'CC', N'1030', N'leonardo.9211@hotmail.com', N'3204995437')
INSERT [dbo].[Huesped] ([HuespedID], [ReservaID], [Nombre], [Apellido], [FechaNacimiento], [Genero], [TipoDocumento], [NumeroDocumento], [Email], [TelefonoContacto]) VALUES (2, 1, N'Paula', N'Guzman', CAST(N'2024-08-09' AS Date), N'F', N'CC', N'1053', N'ANDREGU', N'330168377335')
INSERT [dbo].[Huesped] ([HuespedID], [ReservaID], [Nombre], [Apellido], [FechaNacimiento], [Genero], [TipoDocumento], [NumeroDocumento], [Email], [TelefonoContacto]) VALUES (3, 5, N'Leo', N'Messi', CAST(N'2024-08-11' AS Date), N'M', N'CC', N'1030', N'leonardo.canro@gmail.com', N'3204995434')
INSERT [dbo].[Huesped] ([HuespedID], [ReservaID], [Nombre], [Apellido], [FechaNacimiento], [Genero], [TipoDocumento], [NumeroDocumento], [Email], [TelefonoContacto]) VALUES (4, 6, N'Leo', N'Messi', CAST(N'2024-08-11' AS Date), N'M', N'CC', N'1030', N'leonardo.canro@gmail.com', N'3204995434')
SET IDENTITY_INSERT [dbo].[Huesped] OFF
GO
SET IDENTITY_INSERT [dbo].[Reserva] ON 

INSERT [dbo].[Reserva] ([ReservaID], [HabitacionID], [FechaEntrada], [FechaSalida], [CantidadPersonas], [EstadoReserva], [FechaReserva], [FechaModificacion]) VALUES (1, 1, CAST(N'2024-08-09T18:17:29.610' AS DateTime), CAST(N'2024-08-09T18:17:29.610' AS DateTime), 2, N'Activa', CAST(N'2024-08-09T18:17:29.610' AS DateTime), CAST(N'2024-08-09T18:17:29.610' AS DateTime))
INSERT [dbo].[Reserva] ([ReservaID], [HabitacionID], [FechaEntrada], [FechaSalida], [CantidadPersonas], [EstadoReserva], [FechaReserva], [FechaModificacion]) VALUES (3, 2, CAST(N'2024-08-09T18:17:29.610' AS DateTime), CAST(N'2024-08-09T18:17:29.610' AS DateTime), 3, N'Activa', CAST(N'2024-08-09T18:17:29.610' AS DateTime), CAST(N'2024-08-09T18:17:29.610' AS DateTime))
INSERT [dbo].[Reserva] ([ReservaID], [HabitacionID], [FechaEntrada], [FechaSalida], [CantidadPersonas], [EstadoReserva], [FechaReserva], [FechaModificacion]) VALUES (4, 3, CAST(N'2024-08-09T18:17:29.610' AS DateTime), CAST(N'2024-08-09T18:17:29.610' AS DateTime), 2, N'Activa', CAST(N'2024-08-09T18:17:29.610' AS DateTime), CAST(N'2024-08-09T18:17:29.610' AS DateTime))
INSERT [dbo].[Reserva] ([ReservaID], [HabitacionID], [FechaEntrada], [FechaSalida], [CantidadPersonas], [EstadoReserva], [FechaReserva], [FechaModificacion]) VALUES (5, 3, CAST(N'2024-08-11T03:04:35.007' AS DateTime), CAST(N'2024-08-11T03:04:35.007' AS DateTime), 1, N'Activa', CAST(N'2024-08-11T03:04:35.007' AS DateTime), CAST(N'2024-08-11T03:04:35.007' AS DateTime))
INSERT [dbo].[Reserva] ([ReservaID], [HabitacionID], [FechaEntrada], [FechaSalida], [CantidadPersonas], [EstadoReserva], [FechaReserva], [FechaModificacion]) VALUES (6, 3, CAST(N'2024-08-11T03:04:35.007' AS DateTime), CAST(N'2024-08-11T03:04:35.007' AS DateTime), 1, N'Activa', CAST(N'2024-08-11T03:04:35.007' AS DateTime), CAST(N'2024-08-11T03:04:35.007' AS DateTime))
SET IDENTITY_INSERT [dbo].[Reserva] OFF
GO
/****** Object:  Index [IX_ContactosEmergencia_ReservaID]    Script Date: 12/08/2024 19:53:42 ******/
CREATE NONCLUSTERED INDEX [IX_ContactosEmergencia_ReservaID] ON [dbo].[ContactoEmergencia]
(
	[ReservaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Habitaciones_HotelID]    Script Date: 12/08/2024 19:53:42 ******/
CREATE NONCLUSTERED INDEX [IX_Habitaciones_HotelID] ON [dbo].[Habitacion]
(
	[HotelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DatosHuespedes_ReservaID]    Script Date: 12/08/2024 19:53:42 ******/
CREATE NONCLUSTERED INDEX [IX_DatosHuespedes_ReservaID] ON [dbo].[Huesped]
(
	[ReservaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reservas_HabitacionID]    Script Date: 12/08/2024 19:53:42 ******/
CREATE NONCLUSTERED INDEX [IX_Reservas_HabitacionID] ON [dbo].[Reserva]
(
	[HabitacionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Habitacion] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Habitacion] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Habitacion] ADD  DEFAULT (getdate()) FOR [FechaModificacion]
GO
ALTER TABLE [dbo].[Hotel] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Hotel] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Hotel] ADD  DEFAULT (getdate()) FOR [FechaModificacion]
GO
ALTER TABLE [dbo].[Reserva] ADD  DEFAULT (getdate()) FOR [FechaReserva]
GO
ALTER TABLE [dbo].[Reserva] ADD  DEFAULT (getdate()) FOR [FechaModificacion]
GO
ALTER TABLE [dbo].[ContactoEmergencia]  WITH CHECK ADD  CONSTRAINT [FK_ContactosEmergencia_Reservas] FOREIGN KEY([ReservaID])
REFERENCES [dbo].[Reserva] ([ReservaID])
GO
ALTER TABLE [dbo].[ContactoEmergencia] CHECK CONSTRAINT [FK_ContactosEmergencia_Reservas]
GO
ALTER TABLE [dbo].[Habitacion]  WITH CHECK ADD  CONSTRAINT [FK_Habitaciones_Hoteles] FOREIGN KEY([HotelID])
REFERENCES [dbo].[Hotel] ([HotelID])
GO
ALTER TABLE [dbo].[Habitacion] CHECK CONSTRAINT [FK_Habitaciones_Hoteles]
GO
ALTER TABLE [dbo].[Huesped]  WITH CHECK ADD  CONSTRAINT [FK_DatosHuespedes_Reservas] FOREIGN KEY([ReservaID])
REFERENCES [dbo].[Reserva] ([ReservaID])
GO
ALTER TABLE [dbo].[Huesped] CHECK CONSTRAINT [FK_DatosHuespedes_Reservas]
GO
ALTER TABLE [dbo].[Reserva]  WITH CHECK ADD  CONSTRAINT [FK_Reservas_Habitaciones] FOREIGN KEY([HabitacionID])
REFERENCES [dbo].[Habitacion] ([HabitacionID])
GO
ALTER TABLE [dbo].[Reserva] CHECK CONSTRAINT [FK_Reservas_Habitaciones]
GO
ALTER TABLE [dbo].[Huesped]  WITH CHECK ADD CHECK  (([Genero]='F' OR [Genero]='M'))
GO
USE [master]
GO
ALTER DATABASE [AgenciaViajes] SET  READ_WRITE 
GO
