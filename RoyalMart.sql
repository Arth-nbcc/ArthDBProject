USE [master]
GO
/****** Object:  Database [RoyalMartDB]    Script Date: 17-12-2021 16:12:28 ******/
CREATE DATABASE [RoyalMartDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RoyalMartDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\RoyalMartDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RoyalMartDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\RoyalMartDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RoyalMartDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RoyalMartDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RoyalMartDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RoyalMartDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RoyalMartDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RoyalMartDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RoyalMartDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [RoyalMartDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RoyalMartDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RoyalMartDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RoyalMartDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RoyalMartDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RoyalMartDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RoyalMartDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RoyalMartDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RoyalMartDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RoyalMartDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RoyalMartDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RoyalMartDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RoyalMartDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RoyalMartDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RoyalMartDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RoyalMartDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RoyalMartDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RoyalMartDB] SET RECOVERY FULL 
GO
ALTER DATABASE [RoyalMartDB] SET  MULTI_USER 
GO
ALTER DATABASE [RoyalMartDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RoyalMartDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RoyalMartDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RoyalMartDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RoyalMartDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RoyalMartDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'RoyalMartDB', N'ON'
GO
ALTER DATABASE [RoyalMartDB] SET QUERY_STORE = OFF
GO
USE [RoyalMartDB]
GO
/****** Object:  Table [dbo].[items_table]    Script Date: 17-12-2021 16:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[items_table](
	[item_ID] [int] IDENTITY(1,1) NOT NULL,
	[item_Name] [nvarchar](50) NOT NULL,
	[item_Price] [float] NOT NULL,
	[item_Discount] [float] NOT NULL,
 CONSTRAINT [PK__items_ta__5203084580E510FA] PRIMARY KEY CLUSTERED 
(
	[item_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 17-12-2021 16:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_details]    Script Date: 17-12-2021 16:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_details](
	[order_detailsID] [int] IDENTITY(1,1) NOT NULL,
	[invoice_id] [int] NULL,
	[item_name] [varchar](50) NOT NULL,
	[unit_price] [float] NOT NULL,
	[discount_peritem] [float] NOT NULL,
	[quantity] [float] NOT NULL,
	[subtotal] [float] NOT NULL,
	[tax] [float] NOT NULL,
	[totalcost] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[order_detailsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_master]    Script Date: 17-12-2021 16:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_master](
	[invoice_id] [int] NOT NULL,
	[username] [varchar](50) NOT NULL,
	[datetime] [varchar](50) NOT NULL,
	[finalcost] [float] NOT NULL,
 CONSTRAINT [PK_order_master] PRIMARY KEY CLUSTERED 
(
	[invoice_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[signup]    Script Date: 17-12-2021 16:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[signup](
	[iser_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[surname] [varchar](50) NOT NULL,
	[gender] [varchar](50) NOT NULL,
	[age] [int] NOT NULL,
	[address] [varchar](max) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[iser_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[items_table] ON 

INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (1, N'Milk 1L   ', 6, 1.5)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (2, N'Eggs 12PC ', 10, 1)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (3, N'Juice 1L  ', 11, 4)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (4, N'Oil 1L    ', 25, 4)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (5, N'Flour 10KG', 35, 4.5)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (6, N'Chicken 500G', 115, 25)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (7, N'Roti 10PC', 15, 3)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (8, N'Fish 100G', 35, 3.5)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (9, N'Pasta', 15, 2.5)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (10, N'Cereals', 25, 5)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (11, N'Cheese', 20, 2.5)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (12, N'Candy Bars', 15, 3)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (13, N'Tomtoes 1KG', 20, 2.5)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (14, N'Peanut Butter', 12, 1.5)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (15, N'Soda', 6, 0.5)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (16, N'Rice 1KG', 85, 15)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (17, N'Shampoo', 8, 2)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (18, N'Cat food', 15, 2)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (19, N'Toilet paper', 12, 1.5)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (21, N'Frozen Pizza', 12, 1.5)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (1021, N'skjddj', 453, 23)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (1022, N'djhbjd', 234, 4)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (1023, N'apple', 45, 2)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (1024, N'Chair', 15, 2)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (1025, N'', 0, 0)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (1026, N'', 0, 0)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (1027, N'', 0, 0)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (1028, N'', 0, 0)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (1029, N'', 0, 0)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (1030, N'banana', 9, 1)
INSERT [dbo].[items_table] ([item_ID], [item_Name], [item_Price], [item_Discount]) VALUES (1031, N'watermelon', 15, 2)
SET IDENTITY_INSERT [dbo].[items_table] OFF
GO
SET IDENTITY_INSERT [dbo].[Login] ON 

INSERT [dbo].[Login] ([ID], [Username], [Password]) VALUES (1, N'Arth', N'Arth@123')
INSERT [dbo].[Login] ([ID], [Username], [Password]) VALUES (2, N'nbcc', N'Nbcc@123')
INSERT [dbo].[Login] ([ID], [Username], [Password]) VALUES (3, N'Rohit', N'Rohit@123')
SET IDENTITY_INSERT [dbo].[Login] OFF
GO
SET IDENTITY_INSERT [dbo].[order_details] ON 

INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (1, 1, N'Cereals', 25, 5, 4, 80, 12, 92)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (2, 1, N'Shampoo', 8, 2, 1, 6, 0.3, 6.3)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (3, 4, N'Chicken 500G', 115, 25, 3, 270, 54, 324)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (4, 4, N'Frozen Pizza', 12, 1.5, 7, 73.5, 11.025, 84.525)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (5, 5, N'Frozen Pizza', 12, 1.5, 2, 21, 1.05, 22.05)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (6, 5, N'Oil 1L    ', 25, 4, 3, 63, 9.45, 72.45)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (7, 6, N'Frozen Pizza', 12, 1.5, 4, 42, 4.2, 46.2)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (8, 6, N'Eggs 12PC ', 10, 1, 1, 9, 0.45, 9.45)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (9, 7, N'Frozen Pizza', 12, 1.5, 2, 21, 1.05, 22.05)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (10, 7, N'Pasta', 15, 2.5, 4, 50, 7.5, 57.5)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (12, 9, N'Eggs 12PC ', 10, 1, 4, 36, 3.6, 39.6)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (13, 11, N'Flour 10KG', 35, 4.5, 2, 61, 9.15, 70.15)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (15, 13, N'Oil 1L    ', 25, 4, 5, 105, 18.9, 123.9)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (16, 15, N'Fish 100G', 35, 3.5, 3, 94.5, 14.175, 108.675)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (17, 15, N'Peanut Butter', 12, 1.5, 5, 52.5, 7.875, 60.375)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (18, 15, N'Toilet paper', 12, 1.5, 6, 63, 9.45, 72.45)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (19, 15, N'Fish 100G', 35, 3.5, 3, 94.5, 14.175, 108.675)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (20, 15, N'Tomtoes 1KG', 20, 2.5, 6, 105, 18.9, 123.9)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (21, 15, N'Cereals', 25, 5, 8, 160, 32, 192)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (22, 15, N'Peanut Butter', 12, 1.5, 1, 10.5, 0.525, 11.025)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (25, 19, N'5', 115, 25, 2, 180, 36, 216)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (26, 19, N'9', 25, 5, 1, 20, 1, 21)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (27, 22, N'10', 20, 2.5, 2, 35, 3.5, 38.5)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (28, 22, N'0', 15, 3, 1, 12, 0.6, 12.6)
INSERT [dbo].[order_details] ([order_detailsID], [invoice_id], [item_name], [unit_price], [discount_peritem], [quantity], [subtotal], [tax], [totalcost]) VALUES (29, 22, N'6', 15, 3, 4, 48, 4.8, 52.8)
SET IDENTITY_INSERT [dbo].[order_details] OFF
GO
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (1, N'Arth', N'24-11-2021 01:29:31', 174)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (2, N'Arth', N'01-12-2021 02:00:42', 172.025)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (3, N'Arth', N'05-12-2021 03:35:45', 98.3)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (4, N'Arth', N'05-12-2021 03:37:35', 408.525)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (5, N'Arth', N'05-12-2021 03:41:28', 94.5)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (6, N'Arth', N'05-12-2021 03:44:14', 55.65)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (7, N'Arth', N'05-12-2021 03:48:54', 79.55)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (9, N'Arth', N'05-12-2021 03:55:20', 39.6)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (10, N'heta', N'05-12-2021 03:58:38', 0)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (11, N'heta', N'05-12-2021 04:10:27', 70.15)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (13, N'heta', N'05-12-2021 04:20:04', 123.9)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (14, N'heta', N'14-12-2021 22:19:33', 0)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (15, N'heta', N'14-12-2021 22:46:55', 677.1)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (16, N'nbcc', N'17-12-2021 08:19:16', 5356.85)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (17, N'nbcc', N'17-12-2021 08:20:23', 1478.45)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (18, N'nbcc', N'17-12-2021 08:23:03', 113.075)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (19, N'', N'17-12-2021 08:31:12', 237)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (20, N'nbcc', N'17-12-2021 08:40:24', 0)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (21, N'', N'17-12-2021 08:47:18', 0)
INSERT [dbo].[order_master] ([invoice_id], [username], [datetime], [finalcost]) VALUES (22, N'rohit', N'17-12-2021 09:26:09', 103.9)
GO
SET IDENTITY_INSERT [dbo].[signup] ON 

INSERT [dbo].[signup] ([iser_id], [name], [surname], [gender], [age], [address], [email], [password]) VALUES (2, N'meet', N'patel', N'MALE', 23, N'1234 mountain road', N'meet@gmail.com', N'Meet@123')
INSERT [dbo].[signup] ([iser_id], [name], [surname], [gender], [age], [address], [email], [password]) VALUES (1002, N'Arth', N'Patel', N'MALE', 22, N'1234 m road', N'adasda', N'Arth@123')
INSERT [dbo].[signup] ([iser_id], [name], [surname], [gender], [age], [address], [email], [password]) VALUES (1003, N'nbcc', N'patel', N'MALE', 20, N'1234 mountain road', N'nbcc1@gmail.com', N'Nbcc@123')
INSERT [dbo].[signup] ([iser_id], [name], [surname], [gender], [age], [address], [email], [password]) VALUES (1004, N'rohit', N'patel', N'MALE', 55, N'india', N'rohit1@gmail.com', N'Rohit@123')
INSERT [dbo].[signup] ([iser_id], [name], [surname], [gender], [age], [address], [email], [password]) VALUES (1005, N'manisha', N'patel', N'MALE', 45, N'india', N'manisha1@gmail.com', N'Manisha@123')
SET IDENTITY_INSERT [dbo].[signup] OFF
GO
ALTER TABLE [dbo].[order_details]  WITH CHECK ADD FOREIGN KEY([invoice_id])
REFERENCES [dbo].[order_master] ([invoice_id])
GO
/****** Object:  StoredProcedure [dbo].[getBothTablesData]    Script Date: 17-12-2021 16:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getBothTablesData]
AS
BEGIN
SELECT 
	A.invoice_id,
	A.[datetime],
	B.item_name,
	B.unit_price,
	B.discount_peritem,
	B.quantity, 
	B.subtotal,
	B.tax,
	B.totalcost,
	A.finalcost
from order_master  as   A
INNER JOIN    order_details as   B
ON A.invoice_id = B.invoice_id;
END
GO
/****** Object:  StoredProcedure [dbo].[SearchByIncoiceID]    Script Date: 17-12-2021 16:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SearchByIncoiceID]
@invoiceID int
AS
BEGIN
SELECT 
	A.invoice_id,
	A.[datetime],
	B.item_name,
	B.unit_price,
	B.discount_peritem,
	B.quantity, 
	B.subtotal,
	B.tax,
	B.totalcost,
	A.finalcost
from order_master  as   A
INNER JOIN    order_details as   B
ON A.invoice_id = B.invoice_id
WHERE A.invoice_id = @invoiceID
END
GO
/****** Object:  StoredProcedure [dbo].[SearchByInvoiceID]    Script Date: 17-12-2021 16:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SearchByInvoiceID]
@invoiceID int
AS
BEGIN
SELECT 
	A.invoice_id,
	A.[datetime],
	B.item_name,
	B.unit_price,
	B.discount_peritem,
	B.quantity, 
	B.subtotal,
	B.tax,
	B.totalcost,
	A.finalcost
from order_master  as   A
INNER JOIN    order_details as   B
ON A.invoice_id = B.invoice_id
WHERE A.invoice_id = @invoiceID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SearchByInvoiceID]    Script Date: 17-12-2021 16:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_SearchByInvoiceID]
@invoiceID int
AS
BEGIN
SELECT 
	A.invoice_id,
	A.[datetime],
	B.item_name,
	B.unit_price,
	B.discount_peritem,
	B.quantity, 
	B.subtotal,
	B.tax,
	B.totalcost,
	A.finalcost
from order_master  as   A
INNER JOIN    order_details as   B
ON A.invoice_id = B.invoice_id
WHERE A.invoice_id = @invoiceID
END
GO
USE [master]
GO
ALTER DATABASE [RoyalMartDB] SET  READ_WRITE 
GO
