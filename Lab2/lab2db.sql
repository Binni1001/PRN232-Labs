CREATE DATABASE CosmeticsDB;
GO

USE CosmeticsDB;
GO

CREATE TABLE CosmeticCategory (
    CategoryID NVARCHAR(30) PRIMARY KEY,
    CategoryName NVARCHAR(120),
    UsagePurpose NVARCHAR(250),
    FormulationType NVARCHAR(250)
);

CREATE TABLE CosmeticInformation (
    CosmeticID NVARCHAR(30) PRIMARY KEY,
    CosmeticName NVARCHAR(160),
    SkinType NVARCHAR(200),
    ExpirationDate NVARCHAR(160),
    CosmeticSize NVARCHAR(400),
    DollarPrice DECIMAL(18,0),
    CategoryID NVARCHAR(30),
    FOREIGN KEY (CategoryID) REFERENCES CosmeticCategory(CategoryID)
);

CREATE TABLE SystemAccount (
    AccountID INT PRIMARY KEY,
    EmailAddress NVARCHAR(100) UNIQUE,
    AccountPassword NVARCHAR(100),
    Role INT,
    AccountNote NVARCHAR(240)
);

INSERT INTO CosmeticCategory VALUES
('C001', 'Skincare', 'Moisturizing', 'Cream'),
('C002', 'Makeup', 'Coverage', 'Liquid'),
('C003', 'Haircare', 'Repairing', 'Gel');

INSERT INTO CosmeticInformation VALUES
('PL100001', 'Hydra Cream', 'Dry', '2027-12-31', '50ml', 25, 'C001'),
('PL100002', 'Matte Foundation', 'Oily', '2027-10-20', '30ml', 35, 'C002'),
('PL100003', 'Hair Repair Gel', 'All', '2028-01-15', '100ml', 18, 'C003');

INSERT INTO SystemAccount VALUES
(1, 'admin@gmail.com', '123', 1, 'Administrator'),
(2, 'manager@gmail.com', '123', 2, 'Manager'),
(3, 'staff@gmail.com', '123', 3, 'Staff'),
(4, 'member@gmail.com', '123', 4, 'Member');