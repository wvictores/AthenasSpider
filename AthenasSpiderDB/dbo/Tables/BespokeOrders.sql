CREATE TABLE [dbo].[BespokeOrders]
(
	[BespokeId] INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [CId] INT NOT NULL FOREIGN KEY REFERENCES Customers (CId), 
    [OId] INT NULL FOREIGN KEY REFERENCES Orders (OID), 
    [ItemId] INT NULL FOREIGN KEY REFERENCES Products (ItemId), 
    [VarId] INT NULL, 
    [DueDate] DATE NOT NULL, 
    [Price] MONEY NULL, 
    [Status] VARCHAR(50) NOT NULL
)
