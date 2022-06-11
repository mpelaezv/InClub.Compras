
SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

USE InClub
GO

IF DB_NAME() <> N'InClub' SET NOEXEC ON
GO

--
-- Create table [dbo].[Usuario]
--
PRINT (N'Create table [dbo].[Usuario]')
GO
CREATE TABLE dbo.Usuario (
  ID int IDENTITY (1000, 1),
  UsCorreo varchar(140) NOT NULL,
  UsPass binary(16) NOT NULL,
  UsNombre varchar(200) NOT NULL,
  UsApellidos varchar(200) NOT NULL,
  AuditFechaCreacion datetime NOT NULL DEFAULT (getdate()),
  AuditUsuarioCreacion int NOT NULL,
  AuditFechaUltimaModif datetime NULL,
  AuditUsuarioUltimaModif int NULL,
  AuditEstado bit NOT NULL DEFAULT (1),
  CONSTRAINT PK_Usuario_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create index [UK_Usuario_UsCorreo] on table [dbo].[Usuario]
--
PRINT (N'Create index [UK_Usuario_UsCorreo] on table [dbo].[Usuario]')
GO
CREATE UNIQUE INDEX UK_Usuario_UsCorreo
  ON dbo.Usuario (UsCorreo)
  ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER ON
GO

--
-- Create function [dbo].[CodigoGen]
--
GO
PRINT (N'Create function [dbo].[CodigoGen]')
GO
CREATE FUNCTION dbo.CodigoGen (@Prefix NVARCHAR(10),
@Id INT, -- 10
@Length INT,
@PaddingChar CHAR(1) = '0')
RETURNS NVARCHAR(128) WITH SCHEMABINDING
AS
BEGIN

  RETURN (SELECT
      @Prefix + RIGHT(REPLICATE(@PaddingChar, @Length) + CAST(@Id AS NVARCHAR(10)), @Length))
END
GO

--
-- Create table [dbo].[Producto]
--
PRINT (N'Create table [dbo].[Producto]')
GO
CREATE TABLE dbo.Producto (
  ID int IDENTITY (1000, 1),
  ProdCodigo AS ([dbo].[CodigoGen]('PROD-',CONVERT([nvarchar](6),[ID]),(8),'0')) PERSISTED,
  ProdNombre varchar(400) NOT NULL,
  ProdUM int NOT NULL,
  ProdPrecio decimal(18, 2) NOT NULL,
  AuditFechaCreacion datetime NOT NULL DEFAULT (getdate()),
  AuditUsuarioCreacion int NOT NULL,
  AuditFechaUltimaModif datetime NULL,
  AuditUsuarioUltimaModif int NULL,
  AuditEstado bit NOT NULL DEFAULT (1),
  CONSTRAINT PK_Producto_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

--
-- Create procedure [dbo].[ProcProductoEliminar]
--
GO
PRINT (N'Create procedure [dbo].[ProcProductoEliminar]')
GO
CREATE PROCEDURE dbo.ProcProductoEliminar @ParamID INT
AS
  UPDATE dbo.Producto
  SET AuditEstado = 0
  WHERE ID = @ParamID;
GO

--
-- Create procedure [dbo].[ProcProductoAgregar]
--
GO
PRINT (N'Create procedure [dbo].[ProcProductoAgregar]')
GO
CREATE PROCEDURE dbo.ProcProductoAgregar @ParamProdNombre VARCHAR(400),
@ParamProdUM INT,
@ParamProdPrecio DECIMAL(18, 2),
@ParamAuditUsuarioCreacion INT
AS
BEGIN
  INSERT INTO dbo.Producto (ProdNombre
  , ProdUM
  , ProdPrecio
  , AuditUsuarioCreacion)
    VALUES (@ParamProdNombre, @ParamProdUM, @ParamProdPrecio, @ParamAuditUsuarioCreacion);
END
GO

--
-- Create procedure [dbo].[ProcProductoActualizar]
--
GO
PRINT (N'Create procedure [dbo].[ProcProductoActualizar]')
GO
CREATE PROCEDURE dbo.ProcProductoActualizar @ParamID INT,
@ParamProdNombre VARCHAR(400),
@ParamProdUM INT,
@ParamProdPrecio DECIMAL(18, 2),
@ParamAuditUsuarioUltimaModif INT
AS
BEGIN
  UPDATE dbo.Producto
  SET ProdNombre = @ParamProdNombre
     ,ProdUM = @ParamProdUM
     ,ProdPrecio = @ParamProdPrecio
     ,AuditFechaUltimaModif = GETDATE()
     ,AuditUsuarioUltimaModif = @ParamAuditUsuarioUltimaModif
  WHERE ID = @ParamID;
END
GO

--
-- Create table [dbo].[Compra]
--
PRINT (N'Create table [dbo].[Compra]')
GO
CREATE TABLE dbo.Compra (
  ID int IDENTITY (1000, 1),
  CompraCod AS ([dbo].[CodigoGen]('No ',CONVERT([nvarchar](10),[ID]),(12),'0')) PERSISTED,
  CompraUsuarioID int NOT NULL,
  CompraMontoTotal decimal(18, 2) NOT NULL DEFAULT (0),
  AuditFechaCrecion datetime NOT NULL DEFAULT (getdate()),
  AuditUsuarioCreacion int NOT NULL,
  AuditFechaUltimaModif datetime NULL,
  AuditUsuarioUltimaModif int NULL,
  CONSTRAINT PK_Compra_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create procedure [dbo].[ProcCompraAgregar]
--
GO
PRINT (N'Create procedure [dbo].[ProcCompraAgregar]')
GO
CREATE PROCEDURE dbo.ProcCompraAgregar @ID INT OUTPUT, @ParamCompraUsuarioID INT,
@ParamAuditUsuarioCreacion INT
AS
BEGIN
  INSERT INTO dbo.Compra (CompraUsuarioID
  , AuditUsuarioCreacion)
    VALUES (@ParamCompraUsuarioID, @ParamAuditUsuarioCreacion);
  SET @ID = CAST(SCOPE_IDENTITY() AS INT);
END
GO

--
-- Create table [dbo].[BizData]
--
PRINT (N'Create table [dbo].[BizData]')
GO
CREATE TABLE dbo.BizData (
  ID int IDENTITY (1000, 1),
  CatID int NOT NULL,
  ItemID int NOT NULL,
  ItemDesc varchar(200) NOT NULL,
  ItemTag varchar(50) NULL,
  CONSTRAINT PK_BizData_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create procedure [dbo].[ProdProductoListar]
--
GO
PRINT (N'Create procedure [dbo].[ProdProductoListar]')
GO
CREATE PROCEDURE dbo.ProdProductoListar
AS
BEGIN
  SELECT
    p.ID
   ,p.ProdCodigo
   ,p.ProdNombre
   ,p.ProdUM
   ,b.ItemDesc AS ProdUMDesc
   ,p.ProdPrecio
   ,p.AuditFechaCreacion
   ,p.AuditUsuarioCreacion
   ,LTRIM(RTRIM(CONCAT(u1.UsNombre, ' ', u1.UsApellidos))) AS AuditUsuarioCreacionDesc
   ,p.AuditFechaUltimaModif
   ,p.AuditUsuarioUltimaModif
   ,LTRIM(RTRIM(CONCAT(u2.UsNombre, ' ', u2.UsApellidos))) AS AuditUsuarioUltimaModifDesc
   ,p.AuditEstado
  FROM dbo.Producto p
  LEFT JOIN dbo.BizData b
    ON p.ProdUM = b.ItemID
  LEFT JOIN dbo.Usuario u1
    ON p.AuditUsuarioCreacion = u1.ID
  LEFT JOIN dbo.Usuario u2
    ON p.AuditUsuarioUltimaModif = u2.ID
  WHERE p.AuditEstado = 1
  AND b.CatID = 1000;
END
GO

--
-- Create table [dbo].[CompraDetalle]
--
PRINT (N'Create table [dbo].[CompraDetalle]')
GO
CREATE TABLE dbo.CompraDetalle (
  ID int IDENTITY (1000, 1),
  CompraID int NOT NULL,
  ProductoID int NOT NULL,
  ItemCant int NOT NULL,
  ItemUM int NOT NULL,
  ItemPU decimal(18, 2) NOT NULL,
  ItemTotal decimal(18, 2) NOT NULL,
  AuditFechaCreacion datetime NOT NULL DEFAULT (getdate()),
  AuditUsuarioCreacion int NOT NULL,
  CONSTRAINT PK_CompraDetalle_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create procedure [dbo].[ProcCompraDetalleAgregar]
--
GO
PRINT (N'Create procedure [dbo].[ProcCompraDetalleAgregar]')
GO

CREATE PROCEDURE dbo.ProcCompraDetalleAgregar @ParamCompraID INT,
@ParamProductoID INT,
@ParamItemCant INT,
@ParamAuditUsuarioCreacion INT
AS
BEGIN
  DECLARE @ProdPrecio DECIMAL(18, 2);
  SELECT
    @ProdPrecio = ProdPrecio
  FROM Producto
  WHERE ID = @ParamProductoID;
  INSERT INTO dbo.CompraDetalle (CompraID
  , ProductoID
  , ItemCant
  , ItemUM
  , ItemPU
  , ItemTotal
  , AuditUsuarioCreacion)
    VALUES (@ParamCompraID, @ParamProductoID, @ParamItemCant, (SELECT ProdUM FROM Producto WHERE ID = @ParamProductoID), @ProdPrecio, (@ParamItemCant * @ProdPrecio), @ParamAuditUsuarioCreacion);
END
GO
-- 
-- Dumping data for table BizData
--
PRINT (N'Dumping data for table BizData')
SET IDENTITY_INSERT dbo.BizData ON
GO
INSERT dbo.BizData(ID, CatID, ItemID, ItemDesc, ItemTag) VALUES (1000, 1000, 0, 'Unidades de Medida', NULL)
INSERT dbo.BizData(ID, CatID, ItemID, ItemDesc, ItemTag) VALUES (1001, 1000, 1, 'Kilo(s)', 'Kg(s)')
INSERT dbo.BizData(ID, CatID, ItemID, ItemDesc, ItemTag) VALUES (1002, 1000, 2, 'Unidad(es)', 'Und(s)')
GO
SET IDENTITY_INSERT dbo.BizData OFF
GO
-- 
-- Dumping data for table Compra
--
PRINT (N'Dumping data for table Compra')
-- Table InClub.dbo.Compra does not contain any data (it is empty)
-- 
-- Dumping data for table CompraDetalle
--
PRINT (N'Dumping data for table CompraDetalle')
-- Table InClub.dbo.CompraDetalle does not contain any data (it is empty)
-- 
-- Dumping data for table Producto
--
PRINT (N'Dumping data for table Producto')
-- Table InClub.dbo.Producto does not contain any data (it is empty)
-- 
-- Dumping data for table Usuario
--
PRINT (N'Dumping data for table Usuario')
-- Table InClub.dbo.Usuario does not contain any data (it is empty)
SET NOEXEC OFF
GO