CREATE TABLE [dbo].[Contact]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(50) NULL, 
    [PhoneNumber] NVARCHAR(25) NULL, 
    [Address] NVARCHAR(500) NULL, 
    [City] NVARCHAR(25) NULL, 
    [State] NVARCHAR(25) NULL, 
    [Country] NVARCHAR(25) NULL, 
    [PostalCode] NCHAR(10) NULL, 
)
