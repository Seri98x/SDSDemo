USE [master]
GO
/****** Object:  Database [SDSDemo]    Script Date: 10/09/2024 11:22:39 pm ******/
CREATE DATABASE [SDSDemo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SDSDemo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SDSDemo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SDSDemo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SDSDemo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SDSDemo] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SDSDemo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SDSDemo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SDSDemo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SDSDemo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SDSDemo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SDSDemo] SET ARITHABORT OFF 
GO
ALTER DATABASE [SDSDemo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SDSDemo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SDSDemo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SDSDemo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SDSDemo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SDSDemo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SDSDemo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SDSDemo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SDSDemo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SDSDemo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SDSDemo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SDSDemo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SDSDemo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SDSDemo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SDSDemo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SDSDemo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SDSDemo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SDSDemo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SDSDemo] SET  MULTI_USER 
GO
ALTER DATABASE [SDSDemo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SDSDemo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SDSDemo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SDSDemo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SDSDemo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SDSDemo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SDSDemo] SET QUERY_STORE = ON
GO
ALTER DATABASE [SDSDemo] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SDSDemo]
GO
/****** Object:  Table [dbo].[RecyclableItem]    Script Date: 10/09/2024 11:22:39 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecyclableItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecyclableTypeId] [int] NULL,
	[Weight] [decimal](18, 2) NOT NULL,
	[ComputedRate] [decimal](18, 2) NOT NULL,
	[ItemDescription] [varchar](150) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecyclableType]    Script Date: 10/09/2024 11:22:39 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecyclableType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](100) NOT NULL,
	[Rate] [decimal](18, 2) NOT NULL,
	[MinKg] [decimal](18, 2) NOT NULL,
	[MaxKg] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RecyclableItem]  WITH CHECK ADD FOREIGN KEY([RecyclableTypeId])
REFERENCES [dbo].[RecyclableType] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[SP_AddRecyclableItem]    Script Date: 10/09/2024 11:22:39 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddRecyclableItem]
    @RecyclableTypeId INT,
    @Weight DECIMAL(18, 2),
    @ComputedRate DECIMAL(18, 2),
    @ItemDescription VARCHAR(150)
AS
BEGIN
    INSERT INTO RecyclableItem (RecyclableTypeId, Weight, ComputedRate, ItemDescription)
    VALUES (@RecyclableTypeId, @Weight, @ComputedRate, @ItemDescription)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddRecyclableType]    Script Date: 10/09/2024 11:22:39 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddRecyclableType]
    @Type VARCHAR(100),
    @Rate DECIMAL(18, 2),
    @MinKg DECIMAL(18, 2),
    @MaxKg DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO RecyclableType (Type, Rate, MinKg, MaxKg)
    VALUES (@Type, @Rate, @MinKg, @MaxKg)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteRecyclableItem]    Script Date: 10/09/2024 11:22:39 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeleteRecyclableItem]
    @Id INT
AS
BEGIN
    DELETE FROM RecyclableItem
    WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteRecyclableType]    Script Date: 10/09/2024 11:22:39 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeleteRecyclableType]
    @Id INT
AS
BEGIN
    -- Start a transaction to ensure atomicity
    BEGIN TRANSACTION

    BEGIN TRY
        -- Delete related records in RecyclableItem
        DELETE FROM RecyclableItem
        WHERE RecyclableTypeId = @Id;

        -- Delete the specified RecyclableType record
        DELETE FROM RecyclableType
        WHERE Id = @Id;

        -- Commit the transaction
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        -- Rollback the transaction in case of an error
        ROLLBACK TRANSACTION;

        -- Optional: You can log the error or raise it
        -- RAISERROR('Error occurred while deleting RecyclableType.', 16, 1);
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllRecyclableItems]    Script Date: 10/09/2024 11:22:39 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetAllRecyclableItems]
AS
BEGIN
 SELECT 
        ri.Id,
        ri.RecyclableTypeId,
        ri.Weight,
        ri.ComputedRate,
        ri.ItemDescription,
        rt.Type AS RecyclableTypeType, -- Include Type from RecyclableType
        rt.Rate AS RecyclableTypeRate, -- Include Rate from RecyclableType (if needed)
        rt.MinKg AS RecyclableTypeMinKg, -- Include MinKg from RecyclableType (if needed)
        rt.MaxKg AS RecyclableTypeMaxKg -- Include MaxKg from RecyclableType (if needed)
    FROM 
        RecyclableItem ri
    INNER JOIN 
        RecyclableType rt ON ri.RecyclableTypeId = rt.Id
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllRecyclableTypes]    Script Date: 10/09/2024 11:22:39 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetAllRecyclableTypes]
AS
BEGIN
    SELECT Id, Type, Rate, MinKg, MaxKg
    FROM RecyclableType
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetRecyclableItemById]    Script Date: 10/09/2024 11:22:39 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetRecyclableItemById]
    @Id INT
AS
BEGIN
    SELECT 
        ri.Id,
        ri.RecyclableTypeId,
        ri.Weight,
        ri.ComputedRate,
        ri.ItemDescription,
        rt.Type AS RecyclableTypeType, -- Include Type from RecyclableType
        rt.Rate AS RecyclableTypeRate, -- Include Rate from RecyclableType (if needed)
        rt.MinKg AS RecyclableTypeMinKg, -- Include MinKg from RecyclableType (if needed)
        rt.MaxKg AS RecyclableTypeMaxKg -- Include MaxKg from RecyclableType (if needed)
    FROM 
        RecyclableItem ri
    INNER JOIN 
        RecyclableType rt ON ri.RecyclableTypeId = rt.Id
    WHERE 
        ri.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetRecyclableTypeById]    Script Date: 10/09/2024 11:22:39 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetRecyclableTypeById]
    @Id INT
AS
BEGIN
    SELECT Id, Type, Rate, MinKg, MaxKg
    FROM RecyclableType
    WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateRecyclableItem]    Script Date: 10/09/2024 11:22:39 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateRecyclableItem]
    @Id INT,
    @RecyclableTypeId INT,
    @Weight DECIMAL(18, 2),
    @ComputedRate DECIMAL(18, 2),
    @ItemDescription VARCHAR(150)
AS
BEGIN
    UPDATE RecyclableItem
    SET RecyclableTypeId = @RecyclableTypeId,
        Weight = @Weight,
        ComputedRate = @ComputedRate,
        ItemDescription = @ItemDescription
    WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateRecyclableType]    Script Date: 10/09/2024 11:22:39 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateRecyclableType]
    @Id INT,
    @Type VARCHAR(100),
    @Rate DECIMAL(18, 2),
    @MinKg DECIMAL(18, 2),
    @MaxKg DECIMAL(18, 2)
AS
BEGIN
    UPDATE RecyclableType
    SET Type = @Type,
        Rate = @Rate,
        MinKg = @MinKg,
        MaxKg = @MaxKg
    WHERE Id = @Id
END
GO
USE [master]
GO
ALTER DATABASE [SDSDemo] SET  READ_WRITE 
GO
