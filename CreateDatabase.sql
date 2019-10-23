-- CREATE DATABASE TThreeTeas
-- Create manually in Azure

-- DROP all previous tables
DROP TABLE LineItem;
DROP TABLE Orders;
DROP TABLE Customer;
DROP TABLE Inventory;
DROP TABLE Location;
DROP TABLE Product;

-- CREATE TABLE Product
CREATE TABLE Product (
	ID INT IDENTITY(1,1),
	Name NVARCHAR(255) NOT NULL,
	Price MONEY NOT NULL,
	CONSTRAINT PK_Product PRIMARY KEY (ID),
	CONSTRAINT CHK_Price_Nonnegative CHECK (Price > 0)
);

-- CREATE TABLE Location
CREATE TABLE Location (
	ID INT IDENTITY(1,1),
	Address NVARCHAR(255) NOT NULL,
	City NVARCHAR(255) NOT NULL,
	Zipcode NVARCHAR(9) NOT NULL,
	State NVARCHAR(2) NOT NULL,
	CONSTRAINT PK_Location PRIMARY KEY (ID),
	CONSTRAINT CHK_Zipcode_CorrectDigits CHECK (LEN(Zipcode) = 5 OR LEN(Zipcode) = 9),
	CONSTRAINT CHK_State_CorrectLength CHECK (LEN(State) = 2)
);

-- CREATE TABLE Inventory
CREATE TABLE Inventory (
	LocationID INT NOT NULL,
	ProductID INT NOT NULL,
	Stock INT NOT NULL,
	CONSTRAINT PK_Inventory PRIMARY KEY (LocationID, ProductID),
	CONSTRAINT FK_Inventory_Location_ID FOREIGN KEY (LocationID) REFERENCES Location(ID),
	CONSTRAINT FK_Inventory_Product_ID FOREIGN KEY (ProductID) REFERENCES Product(ID),
	CONSTRAINT CHK_Stock_Nonnegative CHECK (Stock >= 0)
);

-- CREATE TABLE Customer
CREATE TABLE Customer (
	ID INT IDENTITY(1,1),
	FirstName NVARCHAR(255) NOT NULL,
	LastName NVARCHAR (255) NOT NULL,
	CONSTRAINT PK_Customer PRIMARY KEY (ID)
);

-- CREATE TABLE Orders
CREATE TABLE Orders (
	ID INT IDENTITY(1,1),
	LocationID INT NOT NULL,
	CustomerID INT NOT NULL,
	OrderTime DATETIME NOT NULL,
	CONSTRAINT PK_Orders PRIMARY KEY (ID),
	CONSTRAINT FK_Orders_Location_ID FOREIGN KEY (LocationID) REFERENCES Location(ID),
	CONSTRAINT FK_Orders_Customer_ID FOREIGN KEY (CustomerID) REFERENCES Customer(ID)
);

-- CREATE TABLE LineItem
CREATE TABLE LineItem (
	OrdersID INT NOT NULL,
	ProductID INT NOT NULL,
	Quantity INT NOT NULL,
	CONSTRAINT PK_LineItem PRIMARY KEY (OrdersID, ProductID),
	CONSTRAINT FK_LineItem_Orders_ID FOREIGN KEY (OrdersID) REFERENCES Orders(ID),
	CONSTRAINT FK_LineItem_Product_ID FOREIGN KEY (ProductID) REFERENCES Product(ID),
	CONSTRAINT CHK_Quantity_Nonnegative CHECK (Quantity > 0)
);

-- INSERT INTO Products
INSERT INTO Product (Name, Price) VALUES
	('Butterscotch', 20.56),
	('Dark Chocolate Peppermint', 15.78),
	('White Winter Chai', 9.78),
	('Fresh Greens Tea', 23.62),
	('Pumpkin Pie', 8.34),
	('Jasmine Ancient Beauty Tea', 30.12);

-- INSERT INTO Locations
INSERT INTO Location (Address, City, Zipcode, State) VALUES
	('8 Winding Street', 'Hilly Glory', 71550, 'AK'),
	('32 Bull', 'Ranch Plaza', 90235, 'LA'),
	('192 Main', 'Shining Beacon', 89567, 'SD');

-- INSERT INTO Inventories for each Location
INSERT INTO Inventory (LocationID, ProductID, Stock) VALUES
	(1, 3, 4),
	(1, 4, 11),
	(1, 6, 21),
	(2, 2, 8),
	(2, 3, 14),
	(2, 5, 6),
	(3, 1, 6),
	(3, 3, 15),
	(3, 5, 7),
	(3, 6, 12);

-- INSERT INTO Customers
INSERT INTO Customer (FirstName, LastName) VALUES
	('Ojan', 'Negahban'),
	('Henry', 'Ford'),
	('Nikola', 'Tesla'),
	('Lucy', 'Shepherd');

-- INSERT INTO Orders
INSERT INTO Orders (LocationID, CustomerID, OrderTime) VALUES
	(2, 3, DATEADD(hh, -5, GETDATE())),
	(3, 4, DATEADD(hh, -20, GETDATE())),
	(1, 2, DATEADD(hh, -2, GETDATE())),
	(3, 4, DATEADD(hh, -1, GETDATE())),
	(1, 1, DATEADD(hh, -11, GETDATE()));
	
-- INSERT INTO LineItems for each Order
INSERT INTO LineItem (OrdersID, ProductID, Quantity) VALUES
	(1, 2, 1),
	(2, 1, 4),
	(2, 3, 1),
	(3, 4, 3),
	(4, 6, 3),
	(4, 5, 1),
	(4, 1, 1),
	(5, 4, 5),
	(5, 6, 3);

-- SELECT tables for debugging
SELECT * FROM Orders;
SELECT * FROM Location;
SELECT * FROM Inventory;
SELECT * FROM Product;
SELECT * FROM Customer;
SELECT * FROM LineItem;