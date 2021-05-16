
USE master
CREATE DATABASE HastaneRandevu

/*USE HastaneRandevu*/

CREATE TABLE Poliklinik
(
	ID		int PRIMARY KEY NOT NULL,	
	Ad		varchar(255) NOT NULL,			
)

CREATE TABLE Doktor
(
	ID		int PRIMARY KEY NOT NULL,
	Ad		varchar(255) NOT NULL,
	Soyad		varchar(255) NOT NULL,
	PolID		int FOREIGN KEY REFERENCES Poliklinik(ID),
)

CREATE TABLE Hasta
(	
	ID		varchar(11) PRIMARY KEY NOT NULL,
)

CREATE TABLE Randevu
(
	ID		int IDENTITY(0,1) PRIMARY KEY NOT NULL,
	Gün		date NOT NULL,
	Saat		time NOT NULL,
	DoluBilgisi 	BIT NOT NULL,

	DoktorID	int FOREIGN KEY REFERENCES Doktor(ID),
	HastaTC		varchar(11) FOREIGN KEY REFERENCES Hasta(ID)
)

INSERT INTO Poliklinik
VALUES
(1, 'Kardiyoloji'),
(2, 'Ortopedi ve Travmatoloji')

INSERT INTO Doktor
VALUES
(1, 'Utku Gümüş', 1),
(2, 'Yeşim Öztaş', 1),
(3, 'Kemal Baycan', 2),
(4, 'Nazlı Atan', 2)

DECLARE	@count int = 0
DECLARE	@i date = CAST( GETDATE() AS Date )
WHILE @count < 5
BEGIN
	
	DECLARE @j time = '09:00'
	WHILE @j < '12:30'
	BEGIN
		INSERT INTO Randevu
		VALUES
		(@i, @j, 0, 1),
		(@i, @j, 0, 2),
		(@i, @j, 0, 3),
		(@i, @j, 0, 4)

		SET @j = DATEADD(MINUTE, 15, @j)
	END

	DECLARE @k time = '13:30'
	WHILE @k < '17:00'
	BEGIN
		INSERT INTO Randevu
		VALUES
		(@i, @k, 0, 1),
		(@i, @k, 0, 2),
		(@i, @k, 0, 3),
		(@i, @k, 0, 4)

		SET @k = DATEADD(MINUTE, 15, @j)
	END

	SET @i = DATEADD(DAY, 1, @i)

	SET @count += 1
END
