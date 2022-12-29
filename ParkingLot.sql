CREATE DATABASE ParkingLot

USE ParkingLot

CREATE TABLE VehicleTypes (
	VehicleTypeID	int IDENTITY (1, 1) PRIMARY KEY,
	VehicleTypeName nvarchar(MAX) not null,
	ParkingFee		int not null
);

CREATE TABLE Vehicles (
	VehicleID			int IDENTITY (1, 1) PRIMARY KEY,
	VehicleTypeID		int not null,
	ParkingCardID		bigint not null,
	TimeStartedParking	smalldatetime not null, 
	TimeEndedParking	smalldatetime,
	VehicleState		int not null, -- 1: Parked, 0: Unparked --
	VehicleImage		nvarchar(max) not null -- the path to the vehicle's image --
);

CREATE TABLE ParkingCards (
	ParkingCardID	bigint PRIMARY KEY,
	CardState		int not null -- 1: Used, 0: Unused --
);

CREATE TABLE Receipts (
	ReceiptID	int IDENTITY (1, 1) PRIMARY KEY,
	VehicleID	int not null,
	StaffID		int not null,
	TimePaid	smalldatetime not null,
	ParkingFee	int not null
);

CREATE TABLE Accounts (
	AccountID		int IDENTITY (1, 1) PRIMARY KEY,
	StaffID			int not null,
	RoleID			int not null,
	AccountName		nvarchar(MAX) not null,
	AccountPassword nvarchar(MAX) not null,
)

CREATE TABLE Roles (
	RoleID		int IDENTITY (1, 1) PRIMARY KEY,  -- 1: staff, 2: admin --
	RoleName	nvarchar(MAX) not null
);

CREATE TABLE Staff (
	StaffID			int IDENTITY (1, 1) PRIMARY KEY,
	CivilID			nvarchar(MAX) not null,
	StaffName		nvarchar(MAX) not null,
	RoleID			int not null,
	PhoneNumber		nvarchar(MAX),
	StaffAddress	nvarchar(MAX),
	DateOfBirth		smalldatetime
);

CREATE TABLE Timekeeps (
	TimekeepID int IDENTITY (1, 1) PRIMARY KEY,
	StaffID int not null,
	LoginTime smalldatetime not null,
	LogoutTime smalldatetime
);

CREATE TABLE FinancialReports (
	FinancialReportID		int IDENTITY (1, 1) PRIMARY KEY,
	FinancialReportDate		smalldatetime not null,
	Income					int not null
);

ALTER TABLE Vehicles ADD CONSTRAINT Vehicles_VehicleTypeID_FK FOREIGN KEY (VehicleTypeID) REFERENCES VehicleTypes(VehicleTypeID);
ALTER TABLE Vehicles ADD CONSTRAINT Vehicles_CardID_FK FOREIGN KEY (ParkingCardID) REFERENCES ParkingCards(ParkingCardID);
ALTER TABLE Receipts ADD CONSTRAINT Receipts_VehicleID_FK FOREIGN KEY (VehicleID) REFERENCES Vehicles(VehicleID);
ALTER TABLE Receipts ADD CONSTRAINT Receipts_StaffID_FK FOREIGN KEY (StaffID) REFERENCES Staff(StaffID);
ALTER TABLE Timekeeps ADD CONSTRAINT Timekeeps_StaffID_FK FOREIGN KEY (StaffID) REFERENCES Staff(StaffID);
ALTER TABLE Accounts ADD CONSTRAINT Accounts_StaffID_FK FOREIGN KEY (StaffID) REFERENCES Staff(StaffID);
ALTER TABLE Accounts ADD CONSTRAINT Accounts_RoleID_FK FOREIGN KEY (RoleID) REFERENCES Roles(RoleID);
ALTER TABLE Staff ADD CONSTRAINT Staff_RoleID_FK FOREIGN KEY (RoleID) REFERENCES Roles(RoleID);

INSERT INTO Roles VALUES ('staff'), ('admin')
INSERT INTO VehicleTypes (VehicleTypeName, ParkingFee) VALUES ('Xe máy', 4000), ('Xe hơi', 8000)
INSERT INTO Staff (CivilID, StaffName, RoleID, PhoneNumber, StaffAddress, DateOfBirth) VALUES ('CCCD1233466', 'admin', 2, '09123784', '221 Baker St', 1/1/1990)
INSERT INTO Accounts (StaffID, RoleID, AccountName, AccountPassword) VALUES (1, 2, 'admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918')