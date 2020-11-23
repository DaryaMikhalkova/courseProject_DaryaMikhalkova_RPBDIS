CREATE DATABASE Atelier

GO
ALTER DATABASE Atelier SET RECOVERY SIMPLE
GO

USE Atelier
CREATE TABLE dbo.Materials(
	IdMaterial int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	MaterialName nvarchar(50) NOT NULL,
	MaterialType nvarchar(50) NOT NULL,
	QuantityMaterialInStock int NOT NULL
)

CREATE TABLE dbo.AtelierDepartments(
	IdDepartment int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	DepartmentName nvarchar(50) NOT NULL,
	AmountOfWorkers int NOT NULL,
	DescriptionOfTheTypeOfWork nvarchar(50) NOT NULL
)

CREATE TABLE dbo.Products(
	IdProduct int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	ProductName nvarchar(50) NOT NULL,
	MaterialNameId int NOT NULL FOREIGN KEY REFERENCES Materials(IdMaterial) ON DELETE CASCADE,
	NumberOfMaterialsPerItem nvarchar(50) NOT NULL,
	ProductPrice money NOT NULL
)

CREATE TABLE dbo.Employees(
	IdEmployee int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FullName nvarchar(50) NOT NULL,
	DepartmentId int NOT NULL FOREIGN KEY REFERENCES AtelierDepartments(IdDepartment) ON DELETE CASCADE,
	Position nvarchar(50) NOT NULL,
	Telephone int
)

CREATE TABLE dbo.Orders(
	IdOrder int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CustomerName nvarchar(50) NOT NULL,
	ProductNameID int NOT NULL FOREIGN KEY REFERENCES Products(IdProduct) ON DELETE CASCADE,
	EmployeeID int NOT NULL FOREIGN KEY REFERENCES Employees(IdEmployee) ON DELETE CASCADE,
	NumberOfProducts int NOT NULL,
	Price decimal NOT NULL,
	OrderDate date NOT NULL,
	CheckSaleDate date NOT NULL
)

CREATE TABLE MaterialSupply(
	MaterialSuplyId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Provider nvarchar(50) NOT NULL,
	MaterialId int NOT NULL FOREIGN KEY REFERENCES Materials(IdMaterial) ON DELETE CASCADE,
	PriceOfMaterials int NOT NULL,
	AmountOfMaterial int NOT NULL,
	DeliveryDate date NOT NULL
)