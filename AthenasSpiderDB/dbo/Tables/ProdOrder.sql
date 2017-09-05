CREATE TABLE [dbo].[ProdOrder]
(
	
    [ItemID] INT NOT NULL FOREIGN KEY References Products (ItemID), 
    [VarID] INT NOT NULL FOREIGN KEY REFERENCES ProdVariant (VarID), 
    [OId] INT NOT NULL FOREIGN KEY REFERENCES Orders (OId), 
    [Quantity] INT NOT NULL
	PRIMARY KEY (ItemId,OId)



)
