CREATE TABLE [dbo].[Products]
(
	[ItemId] INT IDENTITY(1,1) NOT NULL, 
    [ItemName] NVARCHAR(100) NULL, 
    [Description] NTEXT NULL, 
    [Price] MONEY NOT NULL DEFAULT 1.00, 
    [Image] NVARCHAR(100) NULL, 
    CONSTRAINT [PK_Products] PRIMARY KEY ([ItemId])
)
