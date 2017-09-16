CREATE TABLE [dbo].[BespokeOrders] (
    [BespokeId]     INT             IDENTITY (1, 1) NOT NULL,
    [CId]           INT             NOT NULL,
    [OId]           INT             NOT NULL,
    [ItemId]        INT             NULL,
    [VarId]         INT             NULL,
    [DueDate]       DATE            NULL,
    [Price]         MONEY           NULL,
    [Status]        VARCHAR (50)    NULL,
    [CustomerImage] NVARCHAR (1000) NULL,
    [Description]   NTEXT           NULL,
    PRIMARY KEY CLUSTERED ([BespokeId] ASC),
    FOREIGN KEY ([CId]) REFERENCES [dbo].[Customers] ([CID]),
    FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Products] ([ItemId]),
    FOREIGN KEY ([OId]) REFERENCES [dbo].[Orders] ([OId])
);


