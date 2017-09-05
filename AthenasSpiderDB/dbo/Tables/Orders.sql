CREATE TABLE [dbo].[Orders]
(
	[OId] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [CId] INT NOT NULL FOREIGN KEY References Customers (CID), 
    [ShipAddressLine1] VARCHAR(50) NOT NULL, 
    [ShipAddressLine2] VARCHAR(50) NULL, 
    [City] VARCHAR(50) NOT NULL, 
    [State] NCHAR(2) NOT NULL, 
    [ZipCode] NCHAR(5) NOT NULL, 
    [ShipFee] MONEY NOT NULL DEFAULT 0.00, 
    [Tax] DECIMAL(5, 2) NOT NULL
)
