CREATE DATABASE PetHotelApplicationDB

USE PetHotelApplicationDB

CREATE TABLE "Role" (
    ID VARCHAR(100) PRIMARY KEY,
    RoleName VARCHAR(50) NOT NULL
)

-- Customer table
CREATE TABLE "User" (
ID VARCHAR(100) PRIMARY KEY,
Avatar VARCHAR(MAX),
FullName NVARCHAR(100) NOT NULL,
PhoneNumber VARCHAR(15) NOT NULL,
Email NVARCHAR(100) NOT NULL,
Password NVARCHAR(100) NOT NULL,
Address NVARCHAR(255) NOT NULL,
Status NVARCHAR(20) NOT NULL,
RoleID VARCHAR(100) NOT NULL,
FOREIGN KEY (RoleID) REFERENCES "Role"(ID)
);


-- Pet table
CREATE TABLE Pet (
ID VARCHAR(100) PRIMARY KEY,
Avatar VARCHAR(MAX),
PetName NVARCHAR(100) NOT NULL,
Species NVARCHAR(50) NOT NULL,
Breed NVARCHAR(50) NOT NULL,
DOB Date NOT NULL,
Status NVARCHAR(20) NOT NULL,
UserID VARCHAR(100) NOT NULL,
FOREIGN KEY (UserID) REFERENCES "User"(ID)
);

-- Accommodation table
CREATE TABLE Accommodation (
ID VARCHAR(100) PRIMARY KEY,
Name NVARCHAR(100) NOT NULL,
Type NVARCHAR(50) NOT NULL,
Capacity INT NOT NULL,
Status NVARCHAR(20) NOT NULL,
Description NVARCHAR(max) NOT NULL,
Price DECIMAL(10, 2) NOT NULL
);

-- BookingInformation table
CREATE TABLE BookingInformation (
ID VARCHAR(100) PRIMARY KEY,
BoardingType NVARCHAR(50) NOT NULL,
StartDate DATETIME NOT NULL,
EndDate DATETIME NOT NULL,
Note NVARCHAR(max),
Status NVARCHAR(20),
UserID VARCHAR(100) NOT NULL,
AccommodationID VARCHAR(100) NOT NULL,
PetID VARCHAR(100) NOT NULL,
FOREIGN KEY (UserID) REFERENCES "User"(ID),
FOREIGN KEY (PetID) REFERENCES Pet (ID),
FOREIGN KEY (AccommodationID) REFERENCES Accommodation(ID)
);


-- PetCareService table
CREATE TABLE PetCareService (
    ID VARCHAR(100) PRIMARY KEY,
    Type VARCHAR(50) NOT NULL,
    Description VARCHAR(MAX),
    Status NVARCHAR(20) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL
);

CREATE TABLE ServiceImage (
ID VARCHAR(100) PRIMARY KEY,
Image VARCHAR(MAX) NOT NULL,
ServiceID VARCHAR(100) NOT NULL,
FOREIGN KEY (ServiceID) REFERENCES PetCareService(ID),
);

CREATE TABLE ServiceBooking (
ID VARCHAR(100) PRIMARY KEY,
ServiceID VARCHAR(100) NOT NULL,
BookingId VARCHAR(100) NOT NULL,
FOREIGN KEY (ServiceID) REFERENCES PetCareService(ID),
FOREIGN KEY (BookingID) REFERENCES BookingInformation(ID)
);

-- PaymentRecord table
CREATE TABLE PaymentRecord (
ID VARCHAR(100) PRIMARY KEY,
Price DECIMAL(10, 2) NOT NULL,
Date DATETIME NOT NULL,
Method VARCHAR(50) NOT NULL,
Status VARCHAR(20) NOT NULL,
UserID VARCHAR(100) NOT NULL,
BookingID VARCHAR(100) NOT NULL,
FOREIGN KEY (UserID) REFERENCES "User"(ID),
FOREIGN KEY (BookingID) REFERENCES BookingInformation(ID)
);

-- Feedbacks table
CREATE TABLE Feedbacks (
ID VARCHAR(100) PRIMARY KEY,
Comment VARCHAR(MAX),
Rating INT NOT NULL,
Date DATETIME NOT NULL,
UserID VARCHAR(100) NOT NULL,
FOREIGN KEY (UserID) REFERENCES "User"(ID)
);

-- Sample data for Role table
INSERT INTO "Role" (ID, RoleName) VALUES 
('1', 'Admin'),
('2', 'Customer'),
('3', 'Manager'),
('4', 'Staff');

-- Sample data for User table
INSERT INTO "User" (ID, Avatar, FullName, PhoneNumber, Email, Password, Address, Status, RoleID) VALUES 
('1', 'link','John Doe', '1234567890', 'johndoe@example.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', '123 Elm Street', 'Active', '1'),
('2', 'link','Jane Smith', '0987654321', 'janesmith@example.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', '456 Oak Street', 'Active', '2'),
('3', 'link','Mike Johnson', '5678901234', 'mikejohnson@example.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', '789 Pine Street', 'Active', '3'),
('4', 'link','Emily Davis', '4321098765', 'emilydavis@example.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', '321 Cedar Street', 'Active', '4');

-- Sample data for Pet table
INSERT INTO Pet (ID, Avatar, PetName, Species, Breed, Age, Status, UserID) VALUES 
('1', 'link','Buddy', 'Dog', 'Labrador', '2023-10-12', 'Active', '2'),
('2', 'link','Whiskers', 'Cat', 'Siamese', '2023-11-12', 'Active', '2');

-- Sample data for Accommodation table
INSERT INTO Accommodation (ID, Name, Type, Capacity, Status, Description, Price) VALUES 
('1', 'Kennel A', 'Kennel', 1, 'Available', 'Standard kennel for dogs', 100000),
('2', 'Suite A', 'Suite', 2, 'Available', 'Luxurious suite with all amenities', 200000),
('3', 'Kennel B', 'Kennel', 1, 'Available', 'Standard kennel for dogs', 200000),
('4', 'Suite B', 'Suite', 2, 'Available', 'Luxurious suite with all amenities', 200000),
('5', 'Kennel C', 'Kennel', 1, 'Available', 'Standard kennel for dogs', 100000),
('6', 'Suite C', 'Suite', 1, 'Available', 'Luxurious suite with all amenities', 200000),
('7', 'Communal Area C', 'Communal Area', 15, 'Available', 'Large communal area for pets to play and socialize', 50000);

-- Sample data for BookingInformation table
INSERT INTO BookingInformation (ID, BoardingType, StartDate, EndDate, Note, Status, UserID, AccommodationID, PetID) VALUES 
('1', 'Overnight', '2024-06-01 10:00:00', '2024-06-05 10:00:00', 'No special notes', 'Confirmed', '2', '1', '1'),
('2', 'Daycare', '2024-06-02 08:00:00', '2024-06-02 18:00:00', 'Prefers wet food', 'Confirmed', '2', '2', '2'),
('3', 'Extended Stay', '2024-06-03 08:00:00', '2024-06-10 18:00:00', 'Morning and evening walks', 'Confirmed', '2', '3', '1');

-- Sample data for PetCareService table
INSERT INTO PetCareService (ID, Type, Description, Status, Price) VALUES 
('1', 'Feeding', 'Regular feeding according to schedule', 'Available', 100000),
('2', 'Grooming', 'Full grooming service', 'Available', 100000),
('3', 'Exercise', 'Daily exercise routine', 'Available', 100000),
('4', 'Playtime', 'Supervised playtime with other pets', 'Available', 100000),
('5', 'Spa', 'Relaxing spa treatments for pets', 'Available', 250000),
('6', 'Training', 'Advanced training and behavior modification', 'Available', 100000);


-- Sample data for ServiceBooking table
INSERT INTO ServiceBooking (ID, ServiceID, BookingID) VALUES 
('1', '1', '1'),
('2', '2', '2'),
('3', '3', '3');

-- Sample data for PaymentRecord table
INSERT INTO PaymentRecord (ID, Price, Date, Method, Status, UserID, BookingID) VALUES 
('1', 2000, '2024-06-01 11:00:00', 'Credit Card', 'Paid', '2', '1'),
('2', 5000, '2024-06-02 09:00:00', 'Cash', 'Paid', '2', '2'),
('3', 5000, '2024-06-03 09:00:00', 'Credit Card', 'Paid', '2', '3');

-- Sample data for Feedbacks table
INSERT INTO Feedbacks (ID, Comment, Rating, Date, UserID) VALUES 
('1', 'Great service!', 5, '2024-06-06 12:00:00', '2'),
('2', 'Very satisfied with the care.', 4, '2024-06-03 13:00:00', '2');