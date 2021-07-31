CREATE DATABASE `ordersdb` 

USE `ordersdb`;

CREATE TABLE `Customer` (
  `Id` bigint(11) NOT NULL AUTO_INCREMENT,
  `CustomerNumber` long(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `TaxNumber` varchar(100) NOT NULL
  `CustomerType` int(1) NOT NULL
)

CREATE TABLE `Order` (
  `Id` bigint(11) NOT NULL AUTO_INCREMENT,
  `OrderType` int(1) NOT NULL,
  `OrderNumber`varchar(100) NOT NULL,
  `RequestDate` date NOT NULL,
  `ScheduledDate` date NOT NULL,
  `DeliveryDate` date,
  `State` int(1) NOT NULL,
   PRIMARY KEY (Id),
   FOREIGN KEY (CustomerId) REFERENCES Customer(Id)
) 

CREATE TABLE `OrderItem` (
  `Id` bigint(11) NOT NULL AUTO_INCREMENT,
  `Quantity` int(11) NOT NULL,
  `OrderId` bigint(100) NOT NULL,
   PRIMARY KEY (Id),
   FOREIGN KEY (OrderId) REFERENCES Order(Id)
) 

CREATE TABLE `Product` (
  `Id` bigint(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(1000) NOT NULL,
  `image` varchar(1000) NOT NULL,
  `Price` decimal(10,0) NOT NULL,
  `OrderItemId` bigint(100) NOT NULL,
   PRIMARY KEY (Id),
   FOREIGN KEY (OrderItemId) REFERENCES OrderItem(Id)
) 

