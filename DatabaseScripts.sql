USE [master]
GO
/****** Object:  Database [Library]    Script Date: 06-07-2023 11:31:11 ******/
CREATE DATABASE [Library]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Library', FILENAME = N'D:\InterviewPracticeDotNet\DataBaseInstallSQLServer\MSSQL16.SQLEXPRESS\MSSQL\DATA\Library.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Library_log', FILENAME = N'D:\InterviewPracticeDotNet\DataBaseInstallSQLServer\MSSQL16.SQLEXPRESS\MSSQL\DATA\Library_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Library] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Library].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Library] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Library] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Library] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Library] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Library] SET ARITHABORT OFF 
GO
ALTER DATABASE [Library] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Library] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Library] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Library] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Library] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Library] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Library] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Library] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Library] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Library] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Library] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Library] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Library] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Library] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Library] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Library] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Library] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Library] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Library] SET  MULTI_USER 
GO
ALTER DATABASE [Library] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Library] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Library] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Library] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Library] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Library] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Library] SET QUERY_STORE = ON
GO
ALTER DATABASE [Library] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Library]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 06-07-2023 11:31:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Author] [nvarchar](100) NOT NULL,
	[YearOfPublish] [int] NULL,
	[IsAvailable] [bit] NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[ShelfId] [int] NOT NULL,
 CONSTRAINT [PK__Books__3DE0C207D57EEFA8] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Racks]    Script Date: 06-07-2023 11:31:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Racks](
	[RackId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shelves]    Script Date: 06-07-2023 11:31:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shelves](
	[ShelfId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[RackId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ShelfId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([BookId], [Code], [Name], [Author], [YearOfPublish], [IsAvailable], [Price], [ShelfId]) VALUES (1, N'BK001', N'To Kill a Mockingbird', N'Harper Lee', 1999, 1, CAST(100.00 AS Decimal(10, 2)), 6)
INSERT [dbo].[Books] ([BookId], [Code], [Name], [Author], [YearOfPublish], [IsAvailable], [Price], [ShelfId]) VALUES (2, N'BK002', N'The Great Gatsby', N'F. Scott Fitzgerald', 1925, 0, CAST(100.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[Books] ([BookId], [Code], [Name], [Author], [YearOfPublish], [IsAvailable], [Price], [ShelfId]) VALUES (4, N'BK003', N'Brave New World', N'Brave New World', 1932, 1, CAST(150.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Books] ([BookId], [Code], [Name], [Author], [YearOfPublish], [IsAvailable], [Price], [ShelfId]) VALUES (5, N'BK005', N'The Lord of the Rings', N'The Lord of the Rings', 1954, 1, CAST(80.00 AS Decimal(10, 2)), 5)
INSERT [dbo].[Books] ([BookId], [Code], [Name], [Author], [YearOfPublish], [IsAvailable], [Price], [ShelfId]) VALUES (6, N'BK006', N'The Hobbit', N'Tolkien', 1937, 1, CAST(250.00 AS Decimal(10, 2)), 6)
INSERT [dbo].[Books] ([BookId], [Code], [Name], [Author], [YearOfPublish], [IsAvailable], [Price], [ShelfId]) VALUES (7, N'BK007', N'War and Peace', N'Leo Tolstoy', 1865, 0, CAST(900.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Books] ([BookId], [Code], [Name], [Author], [YearOfPublish], [IsAvailable], [Price], [ShelfId]) VALUES (8, N'BK008', N'The Alchemist', N'Paulo Coelho', 1988, 1, CAST(120.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Books] ([BookId], [Code], [Name], [Author], [YearOfPublish], [IsAvailable], [Price], [ShelfId]) VALUES (9, N'BK009', N'To the Lighthouse', N'To the Lighthouse', 1927, 0, CAST(129.00 AS Decimal(10, 2)), 6)
INSERT [dbo].[Books] ([BookId], [Code], [Name], [Author], [YearOfPublish], [IsAvailable], [Price], [ShelfId]) VALUES (10, N'BK010', N'One Hundred Years', N'Gabriel', 1967, 0, CAST(150.00 AS Decimal(10, 2)), 6)
INSERT [dbo].[Books] ([BookId], [Code], [Name], [Author], [YearOfPublish], [IsAvailable], [Price], [ShelfId]) VALUES (11, N'BK011', N'The Alchemist', N'Paulo Coelho', 1988, 0, CAST(150.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[Books] ([BookId], [Code], [Name], [Author], [YearOfPublish], [IsAvailable], [Price], [ShelfId]) VALUES (12, N'BK012', N'Jane Eyre', N'Charlotte Bronte', 1847, 0, CAST(120.00 AS Decimal(10, 2)), 1)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Racks] ON 

INSERT [dbo].[Racks] ([RackId], [Code]) VALUES (1, N'RK001')
INSERT [dbo].[Racks] ([RackId], [Code]) VALUES (2, N'RK002')
INSERT [dbo].[Racks] ([RackId], [Code]) VALUES (3, N'RK003')
INSERT [dbo].[Racks] ([RackId], [Code]) VALUES (4, N'RK004')
INSERT [dbo].[Racks] ([RackId], [Code]) VALUES (5, N'RK005')
INSERT [dbo].[Racks] ([RackId], [Code]) VALUES (6, N'RK006')
INSERT [dbo].[Racks] ([RackId], [Code]) VALUES (7, N'RK007')
SET IDENTITY_INSERT [dbo].[Racks] OFF
GO
SET IDENTITY_INSERT [dbo].[Shelves] ON 

INSERT [dbo].[Shelves] ([ShelfId], [Code], [RackId]) VALUES (1, N'SH001', 1)
INSERT [dbo].[Shelves] ([ShelfId], [Code], [RackId]) VALUES (2, N'SH002', 2)
INSERT [dbo].[Shelves] ([ShelfId], [Code], [RackId]) VALUES (3, N'SH003', 3)
INSERT [dbo].[Shelves] ([ShelfId], [Code], [RackId]) VALUES (5, N'SH005', 5)
INSERT [dbo].[Shelves] ([ShelfId], [Code], [RackId]) VALUES (6, N'SH006', 6)
SET IDENTITY_INSERT [dbo].[Shelves] OFF
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Shelves] FOREIGN KEY([ShelfId])
REFERENCES [dbo].[Shelves] ([ShelfId])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Shelves]
GO
ALTER TABLE [dbo].[Shelves]  WITH CHECK ADD  CONSTRAINT [FK_Shelves_Racks] FOREIGN KEY([RackId])
REFERENCES [dbo].[Racks] ([RackId])
GO
ALTER TABLE [dbo].[Shelves] CHECK CONSTRAINT [FK_Shelves_Racks]
GO
/****** Object:  StoredProcedure [dbo].[CreateBook]    Script Date: 06-07-2023 11:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateBook]
    @Code NVARCHAR(50),
    @Name NVARCHAR(100),
    @Author NVARCHAR(100),
    @YearOfPublish INT,
    @IsAvailable BIT,
    @Price DECIMAL(18, 2),
    @ShelfId INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Books (Code, Name, Author, YearOfPublish, IsAvailable, Price, ShelfId)
    VALUES (@Code, @Name, @Author, @YearOfPublish, @IsAvailable, @Price, @ShelfId);
END
GO
/****** Object:  StoredProcedure [dbo].[CreateRack]    Script Date: 06-07-2023 11:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateRack]
    @Code NVARCHAR(50)
AS
BEGIN
    INSERT INTO Racks (Code)
    VALUES (@Code)
END


GO
/****** Object:  StoredProcedure [dbo].[DeleteBook]    Script Date: 06-07-2023 11:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteBook]
  @BookId INT
AS
BEGIN
  UPDATE [dbo].[Books]
  SET IsAvailable = 0 -- Assuming 0 represents "Deleted"
  WHERE BookId = @BookId
END
GO
/****** Object:  StoredProcedure [dbo].[GetBookDetails]    Script Date: 06-07-2023 11:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBookDetails]
    @BookId INT
AS
BEGIN
    SELECT *
    FROM Books
    WHERE BookId = @BookId
END

GO
/****** Object:  StoredProcedure [dbo].[GetBooks]    Script Date: 06-07-2023 11:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBooks]
AS
BEGIN
    SELECT 
        b.BookId,
        b.Code,
        b.Name,
        b.Author,
		b.YearOfPublish,
        b.IsAvailable,
        b.Price,
        b.ShelfId,
        s.RackId
    FROM 
        Books b
    INNER JOIN 
        Shelves s ON b.ShelfId = s.ShelfId
END
GO
/****** Object:  StoredProcedure [dbo].[GetRackDetails]    Script Date: 06-07-2023 11:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRackDetails]
    @RackId INT
AS
BEGIN
    SELECT *
    FROM Racks
    WHERE RackId = @RackId
END

GO
/****** Object:  StoredProcedure [dbo].[GetRacks]    Script Date: 06-07-2023 11:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRacks]
AS
BEGIN
    SELECT * FROM Racks;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateBook]    Script Date: 06-07-2023 11:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateBook]
    @BookId INT,
    @Code NVARCHAR(50),
    @Name NVARCHAR(100),
    @Author NVARCHAR(100),
    @YearOfPublish INT,
    @IsAvailable BIT,
    @Price DECIMAL(18, 2),
    @ShelfId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Books
    SET Code = @Code,
        Name = @Name,
        Author = @Author,
        YearOfPublish = @YearOfPublish,
        IsAvailable = @IsAvailable,
        Price = @Price,
        ShelfId = @ShelfId
    WHERE BookId = @BookId;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRack]    Script Date: 06-07-2023 11:31:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateRack]
    @RackId INT,
    @Code NVARCHAR(50)
AS
BEGIN
    UPDATE Racks
    SET Code = @Code
    WHERE RackId = @RackId
END
GO
USE [master]
GO
ALTER DATABASE [Library] SET  READ_WRITE 
GO
