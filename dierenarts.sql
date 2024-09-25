DROP TABLE IF EXISTS Dierenarts.Afspraak;

DROP TABLE IF EXISTS Dierenarts.Diersoort;

DROP TABLE IF EXISTS Dierenarts.Dokter;

DROP TABLE IF EXISTS Dierenarts.Huisdier;

DROP TABLE IF EXISTS Dierenarts.Klant;

DROP TABLE IF EXISTS Dierenarts.Soort;

DROP SCHEMA IF EXISTS Dierenarts;

GO 
	CREATE SCHEMA Dierenarts;
GO
	CREATE TABLE Dierenarts.Afspraak (
	id INT PRIMARY KEY,
	behandeling varchar(40) NOT NULL,
	datum datetime NOT NULL,
	dokterId INT,
	klantId INT NOT NULL
	);
GO
	CREATE TABLE Dierenarts.Diersoort (
	id INT PRIMARY KEY,
	soortId INT NOT NULL,
	huisdierId INT NOT NULL
	);
GO
	CREATE TABLE Dierenarts.Dokter (
	id INT PRIMARY KEY,
	naam varchar(40) NOT NULL,
	voornaam varchar(40) NOT NULL,
	email varchar(40) NOT NULL,
	tel VARCHAR(10) NOT NULL
	);
GO
	CREATE TABLE Dierenarts.Huisdier (
	id INT PRIMARY KEY,
	naam varchar(40) NOT NULL,
	voornaam varchar(40) NOT NULL,
	geboortedatum date NOT NULL,
	klantId INT NOT NULL
	);
GO
	CREATE TABLE Dierenarts.Klant (
	id INT PRIMARY KEY, 
	naam varchar(40) NOT NULL,
	voornaam varchar(40) NOT NULL,
	tel VARCHAR(10) NOT NULL,
	email varchar(50) NOT NULL,
	postcode int NOT NULL
	);
GO
	CREATE TABLE Dierenarts.Soort (
	id INT PRIMARY KEY,
	soort varchar(40) NOT NULL,
	ras varchar(40) NOT NULL
	);
GO

/*Alter tables*/
ALTER TABLE Dierenarts.Afspraak
			ADD CONSTRAINT FK_Afspraak_Dokter 
				FOREIGN KEY (dokterId)
					REFERENCES Dierenarts.Dokter (id),
			CONSTRAINT FK_Afspraak_Klant
				FOREIGN KEY (klantId)
					REFERENCES Dierenarts.Klant (id);
					GO

ALTER TABLE Dierenarts.Diersoort
			ADD CONSTRAINT FK_Diersoort_Soort
				FOREIGN KEY (soortId)
					REFERENCES Dierenarts.Soort (id),
			CONSTRAINT FK_Diersoort_Huisdier
				FOREIGN KEY (huisdierId)
					REFERENCES Dierenarts.Huisdier (id);
					GO

ALTER TABLE Dierenarts.Huisdier
			ADD CONSTRAINT FK_Huisdier_Klant FOREIGN KEY (klantId) REFERENCES Dierenarts.Klant (id)
					GO

INSERT INTO
	Dierenarts.Dokter (
		id, naam, voornaam, email, tel
	)
VALUES
	(1, 'Jan', 'Jansen', 'jan@dokter.be', '0462815598'),
	(2, 'John', 'Johnson', 'john@dokter.be', '0462845545'),
	(3, 'Bob', 'Bobson', 'bob@dokter.be', '0485536314'),
	(4, 'Verheyen', 'Marie', 'marie@dokter.be', '0496521189'),
	(5, 'Jefsen', 'Jef', 'jef@dokter.be', '0412853648'),
	(6, 'Jackson', 'Jack', 'jack@dokter.be', '0476491582'),
	(7, 'Ronson', 'Ron', 'ron@dokter.be', '0413467952'),
	(8, 'Vanasche', 'Marijke', 'marijke@dokter.be', '0448423641'),
	(9, 'Jefsen', 'Noah', 'noah@dokter.be', '0479185135'),
	(10, 'Mertens', 'Emilie', 'emilie@dokter.be', '0416259322'),
	(11, 'Ronson', 'Joana', 'joana@dokter.be', '0422464118'),
	(12, 'Martens', 'Michael', 'michael@dokter.be', '0436361425'),
	(13, 'Sanden', 'Sam', 'sam@dokter.be', '0485524196'),
	(14, 'Brunson', 'Sten', 'sten@dokter.be', '0412565498'),
	(15, 'Verheye', 'Joren', 'joren@dokter.be', '0412345678'),
	(16, 'Verheyen', 'Ken', 'ken@dokter.be', '0498879665');
GO
INSERT INTO 
	Dierenarts.Klant (
		id, naam, voornaam, tel, email, postcode
	)
VALUES
	(1, 'Martens', 'Marte', '0486855584', 'marte@gmail.com', 2430),
	(2, 'Verheye', 'Niel', '0486117532', 'niel@gmail.com', 2300),
	(3, 'Van Dam', 'Karsten', '0468951125', 'karsten@gmail.com', 2330),
	(4, 'Rivas', 'Gabriel', '0485931165', 'gabriel@gmail.com', 2320),
	(5, 'Kanari', 'Karel', '0486855584', 'karel@gmail.com', 2230),
	(6, 'Mertens', 'Merel', '0486117532', 'merel@gmail.com', 2340),
	(7, 'De Grote', 'Alex', '0468951125', 'alex@gmail.com', 2350),
	(8, 'Thomas', 'Ian', '0485931165', 'ian@gmail.com', 2350),
	(9, 'Landen', 'Lei', '0486855584', 'lei@gmail.com', 2330),
	(10, 'Mertens', 'Marc', '0486117532', 'marc@gmail.com', 2340),
	(11, 'Van Kaart', 'Mark', '0468951125', 'mark@gmail.com', 2380),
	(12, 'Polsen', 'Pol', '0485931165', 'pol@gmail.com', 2380),
	(13, 'Poker', 'Pool', '0486855584', 'pool@gmail.com', 2430),
	(14, 'Terveren', 'Thibault', '0486117532', 'thibault@gmail.com', 2300),
	(15, 'Montana', 'Hanna', '0468951125', 'hanna@gmail.com', 2340),
	(16, 'Flex', 'Ronnie', '0485931165', 'ronnie@gmail.com', 2380);
GO
INSERT INTO 
	Dierenarts.Afspraak (
		id, behandeling, datum, dokterId, klantId
	)
VALUES
	(1, 'Gips check', '2022-04-02', 1, 16),
	(2, 'Onsteking poot', '2022-05-01', 2,15),
	(3, 'Gips zetten', '2022-02-01', 3,14),
	(4, 'Ontsteking oor', '2022-04-05', 4,13),
	(5, 'Vaccinatie', '2022-08-11', 5,12),
	(6, 'Gips check', '2022-12-22', 6, 11),
	(7, 'Onsteking poot', '2022-11-30', 7,10),
	(8, 'Gips zetten', '2022-10-11', 8,9),
	(9, 'Ontsteking oor', '2022-02-15', 9,8),
	(10, 'Vaccinatie', '2022-01-21', 10,7),
	(11, 'Gips check', '2022-03-15', 11, 6),
	(12, 'Onsteking poot', '2022-08-16', 12,5),
	(13, 'Gips zetten', '2022-09-18', 13,4),
	(14, 'Ontsteking oor', '2022-06-19', 14,3),
	(15, 'Ontsteking oor', '2022-05-02', 15,2),
	(16, 'Vaccinatie', '2022-04-14', 16,1);
GO
INSERT INTO
	Dierenarts.Huisdier (
		id, naam, voornaam, geboortedatum, klantId
	)
VALUES
	(1, 'Jeanson','Bobby', '2001-04-05', 1),
	(2, 'Rantan', 'Bella', '2000-05-08', 1),
	(3, 'Rebban', 'Flappy', '2016-12-12', 2),
	(4, 'Laben', 'Rex', '2018-09-06', 2),
	(5, 'Teffin', 'Ross', '2020-01-11', 3),
	(6, 'Pandor', 'Ramie', '2019-05-03', 3),
	(7, 'Pango', 'Flekie', '2015-06-04', 4),
	(8, 'Panganie', 'Flok', '2015-06-04', 4),
	(9,  'Bassel','Bas', '2001-04-05', 5),
	(10, 'Maxon', 'Max', '2000-05-08', 6),
	(11, 'Sammon', 'Sam', '2016-12-12', 7),
	(12, 'Landers', 'Luca', '2018-09-06', 8),
	(13, 'Brown', 'Millie', '2020-01-11', 9),
	(14, 'Lond', 'Lala', '2019-05-03', 10),
	(15, 'Laf', 'Lola', '2015-06-04', 11),
	(16, 'Farien', 'Fops', '2015-06-04', 12),
	(17, 'Flol','Flap', '2001-04-05', 13),
	(18, 'Tira', 'Flop', '2000-05-08', 14),
	(19, 'Eduardo', 'Ferry', '2016-12-12', 15),
	(20, 'Mommers', 'Korrie', '2018-09-06', 16),
	(21, 'Proost', 'Kas', '2020-01-11', 5),
	(22, 'Liekes', 'Fleur', '2019-05-03', 6),
	(23, 'Hilfiger', 'Tommy', '2015-06-04', 7),
	(24, 'Gentle', 'Garfield', '2015-06-04', 8),
	(25, 'Beats','Kenny', '2001-04-05', 9),
	(26, 'Banter', 'Boris', '2000-05-08', 10),
	(27, 'Perry', 'Katy', '2016-12-12', 11),
	(28, 'JP', 'Caster', '2018-09-06', 12),
	(29, 'Jeans', 'Balto', '2020-01-11', 13),
	(30, 'Randal', 'Ramones', '2019-05-03', 14),
	(31, 'Trekker', 'Stark', '2015-06-04', 15),
	(32, 'Zonderling', 'Zelda', '2015-06-04', 16);
GO
INSERT INTO 
	Dierenarts.Soort (
		id, soort, ras
	)
VALUES
	(1, 'Hond', 'Golden Retriever'),
	(2, 'Kat', 'Naaktkat Sphynx'),
	(3, 'Vogel', 'Papegaai'),
	(4, 'Vogel', 'Tucan'),
	(5, 'Hond', 'Berner Senner'),
	(6, 'Hond', 'Duitse Herder'),
	(7, 'Vogel', 'Arend'),
	(8, 'Vogel', 'Duif'),
	(9, 'Hond', 'Deense'),
	(10, 'Kat', 'American Bobtail'),
	(11, 'Vogel', 'Nachtegaal'),
	(12, 'Kat', 'American Curl'),
	(13, 'Hond', 'Golden Retriever'),
	(14, 'Kat', 'Abessijn'),
	(15, 'Vogel', 'Papegaai'),
	(16, 'Vogel', 'Arend'),
	(17, 'Hond', 'Terriër'),
	(18, 'Kat', 'Naaktkat Sphynx'),
	(19, 'Vogel', 'Duif'),
	(20, 'Vogel', 'Tucan'),
	(21, 'Hond', 'Golden Retriever'),
	(22, 'Kat', 'Naaktkat Sphynx'),
	(23, 'Vogel', 'Papegaai'),
	(24, 'Vogel', 'Tucan');
GO
INSERT INTO 
	Dierenarts.Diersoort (
		id, soortId, huisdierId
	)
VALUES
	(1, 1, 1),
	(2, 2, 2),
	(3, 3, 3),
	(4, 3, 4),
	(5, 1, 5),
	(6, 4, 6),
	(7, 5, 7),
	(8, 6, 8),
	(9, 7, 9),
	(10, 8, 10),
	(11, 9, 11),
	(12, 10, 12),
	(13, 11, 13),
	(14, 12, 14),
	(15, 13, 15),
	(16, 14, 16),
	(17, 15, 17),
	(18, 16, 18),
	(19, 17, 19),
	(20, 18, 20),
	(21, 19, 21),
	(22, 20, 22),
	(23, 21, 23),
	(24, 22, 24),
	(25, 23, 25),
	(26, 24, 26),
	(27, 1, 27),
	(28, 2, 28),
	(29, 3, 29),
	(30, 4, 30),
	(31, 5, 31),
	(32, 6, 32);
GO

