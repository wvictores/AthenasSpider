/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--INSERT INTO Product (Name, Price) VALUES ('Denim Pants', 59.99);
--INSERT INTO Product (Name, Price) VALUES ('T-Shirt', 19.99);

DELETE FROM Products

GO

INSERT INTO Products (ItemName, Description, Price, Image) VALUES 
('Dog Collar', 'This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.', 12, '/Content/Images/Spoiled Rotten.jpg'),
('Dog Leash', '', 20, '/Content/Images/AdoptedHuman.jpg'),
('Dog Hat', '', 24, '/Content/Images/FreeKisses.jpg'),
('Dog Pants', '', 24, '/Content/Images/BadToBone.jpg')