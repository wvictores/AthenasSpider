CREATE TABLE [dbo].[Customers]
(
	[CID] INT IDENTITY (1,1)NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NULL, 
    [LastName] VARCHAR(50) NULL, 
    [AspNetUserId] NVARCHAR(128) NULL, 
    [YearOfBirth] INT NULL, 
    CONSTRAINT [FK_Customers_AspNetUsers] FOREIGN KEY (AspNetUserID) REFERENCES AspNetUsers(ID)
)
