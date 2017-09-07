CREATE TABLE [dbo].[Shipments]
(
	[ShipId] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [CId] INT NOT NULL Foreign Key References Customers (CId),
    [OId] INT NOT NULL Foreign Key References Orders (OId), 
    [TrackingNum] VARCHAR(50) NULL, 
    [ShippingAddressLine1] VARCHAR(50) NOT NULL, 
    [ShippingAddressLine2] VARCHAR(50) NULL, 
    [City] VARCHAR(20) NOT NULL, 
    [State] NCHAR(2) NOT NULL, 
    [ZipCode] NCHAR(5) NOT NULL
)
