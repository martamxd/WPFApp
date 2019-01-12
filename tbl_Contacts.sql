CREATE TABLE [dbo].[Table]
(
	[ContactId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Imie] NVARCHAR(50) NULL, 
    [Nazwisko] NVARCHAR(50) NULL, 
    [NumerTelefonu] NVARCHAR(50) NULL, 
    [Adres] NVARCHAR(50) NULL
)
