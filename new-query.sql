CREATE DATABASE Temp

USE Temp

CREATE TABLE Polyclinic
(
	Id		int PRIMARY KEY NOT NULL,	
	Name		varchar(255) NOT NULL,
	AddedOn		datetime NOT NULL,
	ModifiedOn	datetime NULL			
)

CREATE TABLE Doctor
(
	Id		int PRIMARY KEY NOT NULL,
	FirstName		varchar(255) NOT NULL,
	LastName		varchar(255) NOT NULL,
	PolId		int FOREIGN KEY REFERENCES Polyclinic(Id),
	AddedOn		datetime NOT NULL,
	ModifiedOn	datetime NULL
)

CREATE TABLE Patient
(	
	Id		varchar(255) PRIMARY KEY NOT NULL,
	AddedOn		datetime NOT NULL,
	ModifiedOn	datetime NULL
)

CREATE TABLE Appointment
(
	Id		int IDENTITY(0,1) PRIMARY KEY NOT NULL,
	AppointmentDate		datetime NOT NULL,
	IsEmpty 			BIT NOT NULL,
	DoctorId	int FOREIGN KEY REFERENCES Doctor(Id),
	PatientId		varchar(255) FOREIGN KEY REFERENCES Patient(Id),
	AddedOn		datetime NOT NULL,
	ModifiedOn	datetime NULL
)

INSERT INTO Polyclinic
VALUES
(1, 'Kardiyoloji','2016-12-21 00:00:00.000', NULL),
(2, 'Ortopedi ve Travmatoloji','2016-12-21 00:00:00.000', NULL)

INSERT INTO Doctor
VALUES
(1, 'Utku', 'Kaya', 1, '2016-12-21 00:00:00.000', NULL),
(2, 'Fatma', 'Aslan', 1,'2016-12-21 00:00:00.000', NULL),
(3, 'Kemal', 'Baycan', 2,'2016-12-21 00:00:00.000', NULL),
(4, 'Yaren', 'Atan', 2, '2016-12-21 00:00:00.000', NULL)

INSERT INTO Patient
VALUES
('52621459889', '2016-12-21 00:00:00.000', NULL),
('94367459231', '2016-12-21 00:00:00.000', NULL),
('49412459247', '2016-12-21 00:00:00.000', NULL),
('23621156863', '2016-12-21 00:00:00.000', NULL),
('34526732824', '2016-12-21 00:00:00.000', NULL)