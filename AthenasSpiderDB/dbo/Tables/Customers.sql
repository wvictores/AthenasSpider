CREATE TABLE [dbo].[Customers]
(
	[CID] INT IDENTITY (1,1)NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NULL, 
    [LastName] VARCHAR(50) NULL, 
    [E-mail] NVARCHAR(30) NULL
)
