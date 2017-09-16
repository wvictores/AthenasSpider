CREATE TABLE [dbo].[Orders] (
    [OId]              INT              IDENTITY (1, 1) NOT NULL,
    [CId]              INT              NULL,
    [ShipAddressLine1] VARCHAR (50)     NOT NULL,
    [ShipAddressLine2] VARCHAR (50)     NULL,
    [City]             VARCHAR (50)     NULL,
    [State]            NCHAR (2)        NULL,
    [ZipCode]          NCHAR (5)        NULL,
    [ShipFee]          MONEY            DEFAULT ((0.00)) NOT NULL,
    [Tax]              DECIMAL (5, 2)   NULL,
    [Name]             UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [Completed]        BIT              DEFAULT ((0)) NOT NULL,
    [TransactionID]    VARCHAR (100)    NULL,
    PRIMARY KEY CLUSTERED ([OId] ASC),
    FOREIGN KEY ([CId]) REFERENCES [dbo].[Customers] ([CID])
);


