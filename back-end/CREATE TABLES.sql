CREATE TABLE Product (
    Id int NOT NULL AUTO_INCREMENT,
    Name varchar(50) NOT NULL UNIQUE,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,
    CONSTRAINT PK_Product PRIMARY KEY (Id)
);

CREATE TABLE EngelsCurve (
    Id int NOT NULL AUTO_INCREMENT,
    ProductId int NOT NULL,
    Income DOUBLE NOT NULL,
    Amount DOUBLE NOT NULL,
    AngularCoefficient DOUBLE NOT NULL,
    Classification TINYINT,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (ProductId) REFERENCES Product(Id)
);