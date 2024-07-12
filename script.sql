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
CreatedDate DATETIME,
ModifiedDate DATETIME,
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

GO
INSERT [dbo].[Role] ([ID], [RoleName]) VALUES (N'1', N'Admin')
INSERT [dbo].[Role] ([ID], [RoleName]) VALUES (N'2', N'Customer')
INSERT [dbo].[Role] ([ID], [RoleName]) VALUES (N'3', N'Manager')
INSERT [dbo].[Role] ([ID], [RoleName]) VALUES (N'4', N'Staff')
GO
INSERT [dbo].[User] ([ID], [Avatar], [FullName], [PhoneNumber], [Email], [Password], [Address], [Status], [RoleID]) VALUES (N'07638394-c1ac-4f5a-8a37-86db6df38262', N'', N'Tuan Nguyen', N'0913756519', N'tuntase171712@fpt.edu.vn', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', N'Việt Nam', N'Active', N'2')
INSERT [dbo].[User] ([ID], [Avatar], [FullName], [PhoneNumber], [Email], [Password], [Address], [Status], [RoleID]) VALUES (N'1', N'link', N'John Doe', N'1234567890', N'johndoe@example.com', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', N'123 Elm Street', N'Active', N'1')
INSERT [dbo].[User] ([ID], [Avatar], [FullName], [PhoneNumber], [Email], [Password], [Address], [Status], [RoleID]) VALUES (N'2', N'https://res.cloudinary.com/dlszawdms/image/upload/v1720400984/qmladluozuseqeybqydq.png', N'Jane Smith', N'0987654321', N'janesmith@example.com', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', N'0987654321', N'Active', N'2')
INSERT [dbo].[User] ([ID], [Avatar], [FullName], [PhoneNumber], [Email], [Password], [Address], [Status], [RoleID]) VALUES (N'29f8212a-1ef6-4cc2-a5de-2d3f53b7c813', N'link', N'Pham Van Hoang', N'0792628240', N'vanhoang@gmail.com', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', N'Binh Thanh', N'Active', N'4')
INSERT [dbo].[User] ([ID], [Avatar], [FullName], [PhoneNumber], [Email], [Password], [Address], [Status], [RoleID]) VALUES (N'3', N'link', N'Mike Johnson', N'5678901234', N'mikejohnson@example.com', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', N'789 Pine Street', N'Active', N'3')
INSERT [dbo].[User] ([ID], [Avatar], [FullName], [PhoneNumber], [Email], [Password], [Address], [Status], [RoleID]) VALUES (N'4', N'link', N'Emily Davis', N'4321098765', N'emilydavis@example.com', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', N'321 Cedar Street', N'Active', N'4')
INSERT [dbo].[User] ([ID], [Avatar], [FullName], [PhoneNumber], [Email], [Password], [Address], [Status], [RoleID]) VALUES (N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7', N'https://res.cloudinary.com/dlszawdms/image/upload/v1720592649/ivssnd6lontjfs0ao3fc.jpg', N'Nguyen Thanh Anh Tu', N'0389334795', N'nguyentu100312@gmail.com', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', N'Dong Nai, Trang Bom update', N'Active', N'2')
INSERT [dbo].[User] ([ID], [Avatar], [FullName], [PhoneNumber], [Email], [Password], [Address], [Status], [RoleID]) VALUES (N'7e684547-d2ec-43d0-848a-84fa0ec0ca90', N'', N'Tuan Nguyen', N'0389334721', N'tuanlt6@fpt.com', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', N'85 Bắc Hợp, xã Bắc Sơn, huyện Trảng Bom, tỉnh Đồng Nai', N'Active', N'2')
INSERT [dbo].[User] ([ID], [Avatar], [FullName], [PhoneNumber], [Email], [Password], [Address], [Status], [RoleID]) VALUES (N'9b3a7442-7446-465a-8570-b4fb13846d64', N'link', N'Nguyễn Quý Đức Tiến', N'0792628243', N'tututu@gmail.com', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', N'Việt Nam', N'Active', N'2')
INSERT [dbo].[User] ([ID], [Avatar], [FullName], [PhoneNumber], [Email], [Password], [Address], [Status], [RoleID]) VALUES (N'd62e33df-116a-4259-bcae-72f6aeb3a2bb', N'', N'Nguyen Thanh Anh Tu', N'0222333443', N'tunguyen100312@gmail.com', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', N'85 Bắc Hợp, xã Bắc Sơn, huyện Trảng Bom, tỉnh Đồng Nai', N'Active', N'2')
GO
INSERT [dbo].[Pet] ([ID], [Avatar], [PetName], [Species], [Breed], [Dob], [Status], [UserID]) VALUES (N'061c0d5e-899e-45a7-94dd-a4515ec61d1c', N'https://res.cloudinary.com/dlszawdms/image/upload/v1720408589/xsjc3aaavjj2yrkuk6fa.jpg', N'Bobby', N'Dog', N'Golden retriever', CAST(N'2002-10-12' AS Date), N'Active', N'd62e33df-116a-4259-bcae-72f6aeb3a2bb')
INSERT [dbo].[Pet] ([ID], [Avatar], [PetName], [Species], [Breed], [Dob], [Status], [UserID]) VALUES (N'1', N'https://res.cloudinary.com/dlszawdms/image/upload/v1719451418/fcz0oib7hxitfe01nfto.jpg', N'Buddy', N'Dog', N'Labrador', CAST(N'2022-09-16' AS Date), N'Active', N'2')
INSERT [dbo].[Pet] ([ID], [Avatar], [PetName], [Species], [Breed], [Dob], [Status], [UserID]) VALUES (N'2', N'https://res.cloudinary.com/dlszawdms/image/upload/v1719475570/y65exefjt4h76rqwcsuc.jpg', N'Whiskers', N'Cat', N'Siamese', CAST(N'2022-10-12' AS Date), N'Active', N'2')
INSERT [dbo].[Pet] ([ID], [Avatar], [PetName], [Species], [Breed], [Dob], [Status], [UserID]) VALUES (N'274487b9-2f1d-4e6f-ab77-ec6a69349709', N'https://res.cloudinary.com/dlszawdms/image/upload/v1719476627/eivyubqfvixwchmsbjmg.jpg', N'Hero', N'Dog', N'Shiba', CAST(N'2022-10-12' AS Date), N'Active', N'9b3a7442-7446-465a-8570-b4fb13846d64')
INSERT [dbo].[Pet] ([ID], [Avatar], [PetName], [Species], [Breed], [Dob], [Status], [UserID]) VALUES (N'6fa84891-0491-4728-92da-1e7f628ff10c', N'https://res.cloudinary.com/dlszawdms/image/upload/v1719448858/ii81dtinqzmy3jbakjvw.jpg', N'Hope', N'Dog', N'Golden retriever', CAST(N'2022-10-12' AS Date), N'Active', N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7')
INSERT [dbo].[Pet] ([ID], [Avatar], [PetName], [Species], [Breed], [Dob], [Status], [UserID]) VALUES (N'824f014e-f08e-4d13-97e6-6b22688c0b0e', N'https://res.cloudinary.com/dlszawdms/image/upload/v1719448650/uxoxinajawk3az2fcc0b.jpg', N'Mew', N'Cat', N'Bristish', CAST(N'2022-10-12' AS Date), N'Active', N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7')
INSERT [dbo].[Pet] ([ID], [Avatar], [PetName], [Species], [Breed], [Dob], [Status], [UserID]) VALUES (N'dd7fb4a5-21b5-4953-ba52-737c65964653', N'https://res.cloudinary.com/dlszawdms/image/upload/v1720530629/ulvxgjoxga4u0rd8a497.jpg', N'Monkey', N'Dog', N'Bristish', CAST(N'0001-01-01' AS Date), N'Active', N'7e684547-d2ec-43d0-848a-84fa0ec0ca90')
INSERT [dbo].[Pet] ([ID], [Avatar], [PetName], [Species], [Breed], [Dob], [Status], [UserID]) VALUES (N'fa519cc2-c560-4376-b6b8-2b84f68a66ca', N'https://res.cloudinary.com/dlszawdms/image/upload/v1720431683/hfufzewd9cds7x35rcao.jpg', N'1', N'Dog', N'1', CAST(N'2022-10-12' AS Date), N'Active', N'9b3a7442-7446-465a-8570-b4fb13846d64')
GO
INSERT [dbo].[Accommodation] ([ID], [Name], [Type], [Capacity], [Status], [Description], [Price]) VALUES (N'1', N'Kennel ABC', N'Kennel', 1, N'Available', N'Standard kennel for dogs', CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[Accommodation] ([ID], [Name], [Type], [Capacity], [Status], [Description], [Price]) VALUES (N'2', N'Suite A', N'Suite', 2, N'Available', N'Luxurious suite with all amenities', CAST(200000.00 AS Decimal(10, 2)))
INSERT [dbo].[Accommodation] ([ID], [Name], [Type], [Capacity], [Status], [Description], [Price]) VALUES (N'3', N'Kennel B', N'Kennel', 1, N'Available', N'Standard kennel for dogs', CAST(200000.00 AS Decimal(10, 2)))
INSERT [dbo].[Accommodation] ([ID], [Name], [Type], [Capacity], [Status], [Description], [Price]) VALUES (N'4', N'Suite B', N'Suite', 2, N'Available', N'Luxurious suite with all amenities', CAST(200000.00 AS Decimal(10, 2)))
INSERT [dbo].[Accommodation] ([ID], [Name], [Type], [Capacity], [Status], [Description], [Price]) VALUES (N'5', N'Kennel C', N'Kennel', 1, N'Available', N'Standard kennel for dogs', CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[Accommodation] ([ID], [Name], [Type], [Capacity], [Status], [Description], [Price]) VALUES (N'6', N'Suite C', N'Suite', 1, N'Available', N'Luxurious suite with all amenities', CAST(200000.00 AS Decimal(10, 2)))
INSERT [dbo].[Accommodation] ([ID], [Name], [Type], [Capacity], [Status], [Description], [Price]) VALUES (N'7', N'Communal Area C', N'Communal Area', 15, N'Available', N'Large communal area for pets to play and socialize', CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[Accommodation] ([ID], [Name], [Type], [Capacity], [Status], [Description], [Price]) VALUES (N'a0323c06-41ad-49eb-ad47-f1d94d5b4fed', N'Kennel D', N'Kennel', 1, N'Available', N'New Kennel', CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[Accommodation] ([ID], [Name], [Type], [Capacity], [Status], [Description], [Price]) VALUES (N'b80caa1d-36b0-45c7-8f6e-d53209482aa3', N'New Accommodation', N'Kennel', 1, N'Available', N'New Kennel', CAST(200000.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[BookingInformation] ([ID], [BoardingType], [StartDate], [EndDate], [Note], [Status], [UserID], [AccommodationID], [PetID], [CreatedDate]) VALUES (N'363aba66-f845-4085-ba23-40fa0902556e', N'Overnight', CAST(N'2024-07-12T08:00:00.000' AS DateTime), CAST(N'2024-07-17T21:00:00.000' AS DateTime), N'no', N'Cancelled', N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7', N'1', N'6fa84891-0491-4728-92da-1e7f628ff10c', CAST(N'2024-07-10T21:10:11.153' AS DateTime))
INSERT [dbo].[BookingInformation] ([ID], [BoardingType], [StartDate], [EndDate], [Note], [Status], [UserID], [AccommodationID], [PetID], [CreatedDate]) VALUES (N'40fea55f-5b54-4db1-8b85-2c076093d4b4', N'Overnight', CAST(N'2024-07-11T08:00:00.000' AS DateTime), CAST(N'2024-07-13T21:00:00.000' AS DateTime), N'no', N'Confirmed', N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7', N'2', N'824f014e-f08e-4d13-97e6-6b22688c0b0e', CAST(N'2024-07-10T21:10:11.153' AS DateTime))
INSERT [dbo].[BookingInformation] ([ID], [BoardingType], [StartDate], [EndDate], [Note], [Status], [UserID], [AccommodationID], [PetID], [CreatedDate]) VALUES (N'52cc0a01-9562-40d2-90ad-413696c4f932', N'Day care', CAST(N'2024-07-11T08:00:00.000' AS DateTime), CAST(N'2024-07-11T21:00:00.000' AS DateTime), N'nice', N'Pending', N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7', N'1', N'6fa84891-0491-4728-92da-1e7f628ff10c', CAST(N'2024-07-10T21:10:11.153' AS DateTime))
INSERT [dbo].[BookingInformation] ([ID], [BoardingType], [StartDate], [EndDate], [Note], [Status], [UserID], [AccommodationID], [PetID], [CreatedDate]) VALUES (N'6c9cd8a8-ed21-40ad-bcfc-0d43c6020622', N'Day care', CAST(N'2024-07-13T08:00:00.000' AS DateTime), CAST(N'2024-07-13T21:00:00.000' AS DateTime), N'nice', N'Pending', N'2', N'a0323c06-41ad-49eb-ad47-f1d94d5b4fed', N'1', CAST(N'2024-07-10T21:10:11.153' AS DateTime))
GO
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'1', N'Feeding', N'Regular feeding according to schedule', N'Available', CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'2', N'Grooming', N'Full grooming service', N'Available', CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'3', N'Exercise', N'Daily exercise routine', N'Available', CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'4', N'Playtime', N'Supervised playtime with other pets', N'Available', CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'5', N'Spa', N'Relaxing spa treatments for pets', N'Available', CAST(250000.00 AS Decimal(10, 2)))
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'56b56cde-e472-4944-8525-7f0a3ebafb27', N'Hotel new hotel', N'Hotel for pets', N'Available', CAST(200000.00 AS Decimal(10, 2)))
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'6', N'Training', N'Advanced training and behavior modification', N'Available', CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'6fbaf958-9115-43c7-8206-d39a6e021781', N'new new new', N'123', N'Unavailable', CAST(123123.00 AS Decimal(10, 2)))
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'7', N'Dietary Accommodation', N'Special dietary plans and accommodations', N'Available', CAST(10000.00 AS Decimal(10, 2)))
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'8', N'Hotel', N'Overnight accommodation for pets', N'Unavailable', CAST(100000.00 AS Decimal(10, 2)))
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'9', N'Day Care', N'Daytime care and supervision', N'Unavailable', CAST(50000.00 AS Decimal(10, 2)))
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'e48d0a86-af01-46a2-bac0-4ccbb68f0bbd', N'Play', N'new service for booking v2', N'Unavailable', CAST(1000.00 AS Decimal(10, 2)))
INSERT [dbo].[PetCareService] ([ID], [Type], [Description], [Status], [Price]) VALUES (N'e9a593e3-1afa-4aee-8f61-ec304f3d4157', N'New service update', N'new service update', N'Unavailable', CAST(1000.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[ServiceBooking] ([ID], [ServiceID], [BookingId]) VALUES (N'26d934cd-b8d8-493a-8e45-d4897434347b', N'1', N'6c9cd8a8-ed21-40ad-bcfc-0d43c6020622')
INSERT [dbo].[ServiceBooking] ([ID], [ServiceID], [BookingId]) VALUES (N'4a411465-509b-42e0-b1ee-787987a811f6', N'3', N'363aba66-f845-4085-ba23-40fa0902556e')
INSERT [dbo].[ServiceBooking] ([ID], [ServiceID], [BookingId]) VALUES (N'52e21ee8-8578-4dcc-9b13-68df7ff58030', N'3', N'40fea55f-5b54-4db1-8b85-2c076093d4b4')
INSERT [dbo].[ServiceBooking] ([ID], [ServiceID], [BookingId]) VALUES (N'5d10f9b3-45ee-4348-a084-d69053ab068c', N'3', N'52cc0a01-9562-40d2-90ad-413696c4f932')
INSERT [dbo].[ServiceBooking] ([ID], [ServiceID], [BookingId]) VALUES (N'897cb24b-eb25-4e51-9e15-a0be053fc8f1', N'2', N'52cc0a01-9562-40d2-90ad-413696c4f932')
INSERT [dbo].[ServiceBooking] ([ID], [ServiceID], [BookingId]) VALUES (N'b48c1dce-f943-4634-abaa-164f06732f3c', N'1', N'52cc0a01-9562-40d2-90ad-413696c4f932')
INSERT [dbo].[ServiceBooking] ([ID], [ServiceID], [BookingId]) VALUES (N'd8d01e1c-ad8c-41f4-868a-644917292f5f', N'1', N'40fea55f-5b54-4db1-8b85-2c076093d4b4')
INSERT [dbo].[ServiceBooking] ([ID], [ServiceID], [BookingId]) VALUES (N'db58f0af-f334-48cf-8c2b-af8213542449', N'4', N'363aba66-f845-4085-ba23-40fa0902556e')
INSERT [dbo].[ServiceBooking] ([ID], [ServiceID], [BookingId]) VALUES (N'e5554f13-daba-4b7d-b860-9569307c6d49', N'2', N'6c9cd8a8-ed21-40ad-bcfc-0d43c6020622')
GO
INSERT [dbo].[PaymentRecord] ([ID], [Price], [Date], [Method], [Status], [UserID], [BookingID]) VALUES (N'0fcd1bb2-ee7f-4d5c-bb07-9c344df57aeb', CAST(300000.00 AS Decimal(10, 2)), CAST(N'2024-07-10T21:10:11.153' AS DateTime), N'Cash', N'Cancelled', N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7', N'52cc0a01-9562-40d2-90ad-413696c4f932')
INSERT [dbo].[PaymentRecord] ([ID], [Price], [Date], [Method], [Status], [UserID], [BookingID]) VALUES (N'2454298d-c9bd-4db5-9414-098170cf228f', CAST(1500000.00 AS Decimal(10, 2)), CAST(N'2024-07-10T21:31:15.897' AS DateTime), N'TransferCash', N'Cancelled', N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7', N'363aba66-f845-4085-ba23-40fa0902556e')
INSERT [dbo].[PaymentRecord] ([ID], [Price], [Date], [Method], [Status], [UserID], [BookingID]) VALUES (N'2d3dadf8-0bf0-4bdf-95b0-19f185570106', CAST(300000.00 AS Decimal(10, 2)), CAST(N'2024-07-10T21:10:20.380' AS DateTime), N'Cash', N'Unpaid', N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7', N'52cc0a01-9562-40d2-90ad-413696c4f932')
INSERT [dbo].[PaymentRecord] ([ID], [Price], [Date], [Method], [Status], [UserID], [BookingID]) VALUES (N'7b6b2f00-1a6d-4f9e-9b3f-19f2b7c9180d', CAST(200000.00 AS Decimal(10, 2)), CAST(N'2024-07-10T21:18:39.050' AS DateTime), N'Cash', N'Unpaid', N'2', N'6c9cd8a8-ed21-40ad-bcfc-0d43c6020622')
INSERT [dbo].[PaymentRecord] ([ID], [Price], [Date], [Method], [Status], [UserID], [BookingID]) VALUES (N'f8b1af54-ebaf-4bc4-8033-1564283c842f', CAST(300000.00 AS Decimal(10, 2)), CAST(N'2024-07-10T21:07:56.627' AS DateTime), N'Cash', N'Cancelled', N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7', N'52cc0a01-9562-40d2-90ad-413696c4f932')
INSERT [dbo].[PaymentRecord] ([ID], [Price], [Date], [Method], [Status], [UserID], [BookingID]) VALUES (N'fe40e666-89a6-46d4-96d1-2d7f7deed7e5', CAST(800000.00 AS Decimal(10, 2)), CAST(N'2024-07-10T21:08:36.717' AS DateTime), N'TransferCash', N'Paid', N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7', N'40fea55f-5b54-4db1-8b85-2c076093d4b4')
GO
INSERT [dbo].[Feedbacks] ([ID], [Comment], [Rating], [Date], [UserID]) VALUES (N'3aefd252-80a5-4b7e-a38a-b16b2b8bceb7', N'The service is very good, my pet really loves it', 5, CAST(N'2024-07-10T21:39:43.403' AS DateTime), N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7')
INSERT [dbo].[Feedbacks] ([ID], [Comment], [Rating], [Date], [UserID]) VALUES (N'3c93099f-794c-43e5-b02d-aec79d3e02c2', N'The service quite expensive, but very good, should consider about the price', 5, CAST(N'2024-06-27T08:19:59.570' AS DateTime), N'722b8423-ec7c-4aa4-b338-b09dc2e45ff7')
INSERT [dbo].[Feedbacks] ([ID], [Comment], [Rating], [Date], [UserID]) VALUES (N'63498a8a-d784-4ce3-8e69-870219191fb7', N'The service is very good, my pet is very happy, thanks you so much, The service is very good, my pet is very happy, thanks you so much, The service is very good, my pet is very happy, thanks you so much, The service is very good, my pet is very happy, thanks you so much	
', 4, CAST(N'2024-06-27T08:19:31.890' AS DateTime), N'2')
INSERT [dbo].[Feedbacks] ([ID], [Comment], [Rating], [Date], [UserID]) VALUES (N'88ef31c6-bbda-4e41-84b6-b9eabc19ccc9', N'Very satisfied with the care.	
', 5, CAST(N'2024-06-27T08:19:23.160' AS DateTime), N'2')
GO
INSERT [dbo].[ServiceImage] ([ID], [Image], [ServiceID]) VALUES (N'092b6917-4cc5-4667-8def-8015b8e52f4e', N'https://res.cloudinary.com/dlszawdms/image/upload/v1719159135/nky24x82qqy9y2kywpiq.png', N'e9a593e3-1afa-4aee-8f61-ec304f3d4157')
INSERT [dbo].[ServiceImage] ([ID], [Image], [ServiceID]) VALUES (N'1f6beb36-797c-49e5-87e5-dd5a6c8c38f1', N'https://res.cloudinary.com/dlszawdms/image/upload/v1719159143/cao5o1ijm3tva28fzrfd.png', N'e9a593e3-1afa-4aee-8f61-ec304f3d4157')
INSERT [dbo].[ServiceImage] ([ID], [Image], [ServiceID]) VALUES (N'5ca5e3b2-eb62-46c3-b8cc-a6001cf78c97', N'https://res.cloudinary.com/dlszawdms/image/upload/v1719379347/kybqrddbxtg4jcikkdhr.png', N'56b56cde-e472-4944-8525-7f0a3ebafb27')
INSERT [dbo].[ServiceImage] ([ID], [Image], [ServiceID]) VALUES (N'675d4ecd-b04f-4687-aab5-c0673fde3fe0', N'https://res.cloudinary.com/dlszawdms/image/upload/v1720612077/auwlqamm803w8k0huivc.jpg', N'6fbaf958-9115-43c7-8206-d39a6e021781')
INSERT [dbo].[ServiceImage] ([ID], [Image], [ServiceID]) VALUES (N'67eb2eb4-57a0-45c7-b691-253454d7db8c', N'https://res.cloudinary.com/dlszawdms/image/upload/v1719379329/jtgwrwkblfv1pszjoc31.png', N'56b56cde-e472-4944-8525-7f0a3ebafb27')
INSERT [dbo].[ServiceImage] ([ID], [Image], [ServiceID]) VALUES (N'856bce53-65b9-481a-a6eb-10c14fe7bce5', N'https://res.cloudinary.com/dlszawdms/image/upload/v1719374620/nsqkdezhicstt4y8vuno.png', N'e48d0a86-af01-46a2-bac0-4ccbb68f0bbd')
INSERT [dbo].[ServiceImage] ([ID], [Image], [ServiceID]) VALUES (N'95d8940d-7e6f-45c8-b8f1-ccdf3554d33f', N'https://res.cloudinary.com/dlszawdms/image/upload/v1720612079/ovamaarb6mdtq4ddjdhy.webp', N'6fbaf958-9115-43c7-8206-d39a6e021781')
INSERT [dbo].[ServiceImage] ([ID], [Image], [ServiceID]) VALUES (N'c9291ad6-9922-4c9d-9c77-571c82d8a52c', N'https://res.cloudinary.com/dlszawdms/image/upload/v1720612078/v6isremoggfubb2zyfcp.jpg', N'6fbaf958-9115-43c7-8206-d39a6e021781')
INSERT [dbo].[ServiceImage] ([ID], [Image], [ServiceID]) VALUES (N'fa4124e1-120b-4338-aeb4-2927d473989c', N'https://res.cloudinary.com/dlszawdms/image/upload/v1719379340/rdygnoq0sbluh6jgtgpl.png', N'56b56cde-e472-4944-8525-7f0a3ebafb27')
INSERT [dbo].[ServiceImage] ([ID], [Image], [ServiceID]) VALUES (N'fd174bee-3b76-4ed3-9589-5c0588f9ecaf', N'https://res.cloudinary.com/dlszawdms/image/upload/v1719159123/vdmuuszb9uece5u6i3fy.png', N'e9a593e3-1afa-4aee-8f61-ec304f3d4157')
GO