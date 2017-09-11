CREATE TABLE [dbo].[ProdOrder]
(
	
    [VarID] INT NOT NULL FOREIGN KEY REFERENCES ProdVariant (VarID), 
    [OId] INT NOT NULL FOREIGN KEY REFERENCES Orders (OId), 
    [Quantity] INT NOT NULL, 
    CONSTRAINT [PK_ProdOrder] PRIMARY KEY ([VarID], [OId])



)
