CREATE TABLE [dbo].[Orders]
(
	[OId] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [CId] INT NULL FOREIGN KEY References Customers (CID), 
    [ShipAddressLine1] VARCHAR(50) NOT NULL, 
    [ShipAddressLine2] VARCHAR(50) NULL, 
    [City] VARCHAR(50) NULL, 
    [State] NCHAR(2) NULL, 
    [ZipCode] NCHAR(5) NULL, 
    [ShipFee] MONEY NOT NULL DEFAULT 0.00, 
    [Tax] DECIMAL(5, 2) NULL, 
    [Name] UNIQUEIDENTIFIER NOT NULL DEFAULT newid(), 
    [Completed] BIT NOT NULL DEFAULT 0
)
