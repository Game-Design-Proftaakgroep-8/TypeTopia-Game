SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE if exists Person;
DROP TABLE if exists Score;
DROP TABLE if exists Game;
DROP TABLE if exists CommaOption;
DROP TABLE if exists SumComma;
DROP TABLE if exists Sums;
SET FOREIGN_KEY_CHECKS = 1;

CREATE TABLE Person
(
	pName varchar(50) PRIMARY KEY,
	pPassword varchar(50) NOT NULL
);

CREATE TABLE Score
(
	scoreID int(100) PRIMARY KEY auto_increment,
	pName varchar(50) NOT NULL,
	score int(100) NOT NULL,

	CONSTRAINT FK_pName FOREIGN KEY (pName) REFERENCES Person(pName) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Game
(
	gameID int(100) PRIMARY KEY auto_increment,
	gamename varchar(50) NOT NULL
);

CREATE TABLE CommaOption
(
	commaID int(100) PRIMARY KEY auto_increment,
	commaValue decimal(65,2) NOT NULL
);

CREATE TABLE Sums
(
	sumID int(100) PRIMARY KEY auto_increment,
	sumType varchar(50) NOT NULL,
	sumLevel int(100) NOT NULL,
	minRange decimal(65,2) NOT NULL,
	maxRange decimal(65,2) NOT NULL,
	sumCommas int(100) NOT NULL,
	commaOptions boolean NOT NULL
);

CREATE TABLE SumComma
(
	sumID int(100) NOT NULL,
	commaID int(100) NOT NULL,

	CONSTRAINT PK_sumID PRIMARY KEY (sumID,commaID),
	CONSTRAINT FK_sumID FOREIGN KEY (sumID) REFERENCES Sums(sumID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_commaID FOREIGN KEY (commaID) REFERENCES CommaOption(commaID) ON DELETE CASCADE ON UPDATE CASCADE
);

START TRANSACTION;
SAVEPOINT sp_Empty;

INSERT INTO Person(pName, pPassword)
VALUES('mel', 'w');
INSERT INTO Person(pName, pPassword)
VALUES('alex', '123');
INSERT INTO Person(pName, pPassword)
VALUES('bart', 'bb');

INSERT INTO Score(pName, score)
VALUES('mel', 1100);
INSERT INTO Score(pName, score)
VALUES('alex', 1450);
INSERT INTO Score(pName, score)
VALUES('bart', 760);

INSERT INTO Game(gamename)
VALUES('workbench');
INSERT INTO Game(gamename)
VALUES('oven');
INSERT INTO Game(gamename)
VALUES('counter');

INSERT INTO Sums(sumType, sumLevel, minRange, maxRange, sumCommas, commaOptions)
VALUES('money',0,1,10,0,false);
INSERT INTO Sums(sumType, sumLevel, minRange, maxRange, sumCommas, commaOptions)
VALUES('money',1,0,5,1,false);
INSERT INTO Sums(sumType, sumLevel, minRange, maxRange, sumCommas, commaOptions)
VALUES('money',2,0,6,2,true);
INSERT INTO Sums(sumType, sumLevel, minRange, maxRange, sumCommas, commaOptions)
VALUES('money',3,0,6,2,false);

INSERT INTO Sums(sumType, sumLevel, minRange, maxRange, sumCommas, commaOptions)
VALUES('time',0,0,23,0,false);
INSERT INTO Sums(sumType, sumLevel, minRange, maxRange, sumCommas, commaOptions)
VALUES('time',1,0,23,2,true);
INSERT INTO Sums(sumType, sumLevel, minRange, maxRange, sumCommas, commaOptions)
VALUES('time',2,0,23,2,false);
INSERT INTO Sums(sumType, sumLevel, minRange, maxRange, sumCommas, commaOptions)
VALUES('time',3,0,23,2,false);

INSERT INTO Sums(sumType, sumLevel, minRange, maxRange, sumCommas, commaOptions)
VALUES('volume',0,0,10,0,false);
INSERT INTO Sums(sumType, sumLevel, minRange, maxRange, sumCommas, commaOptions)
VALUES('volume',1,0,100,0,false);
INSERT INTO Sums(sumType, sumLevel, minRange, maxRange, sumCommas, commaOptions)
VALUES('volume',2,0,10,1,false);
INSERT INTO Sums(sumType, sumLevel, minRange, maxRange, sumCommas, commaOptions)
VALUES('volume',3,0,10,1,false);

INSERT INTO CommaOption(commaValue)
VALUES (0.25);
INSERT INTO CommaOption(commaValue)
VALUES (0.50);
INSERT INTO CommaOption(commaValue)
VALUES (0.75);
INSERT INTO CommaOption(commaValue)
VALUES (0.15);
INSERT INTO CommaOption(commaValue)
VALUES (0.30);
INSERT INTO CommaOption(commaValue)
VALUES (0.45);

INSERT INTO SumComma(sumID, commaID)
VALUES(3, 1);
INSERT INTO SumComma(sumID, commaID)
VALUES(3, 2);
INSERT INTO SumComma(sumID, commaID)
VALUES(3, 3);
INSERT INTO SumComma(sumID, commaID)
VALUES(6, 4);
INSERT INTO SumComma(sumID, commaID)
VALUES(6, 5);
INSERT INTO SumComma(sumID, commaID)
VALUES(6, 6);

/*ROLLBACK TO sp_Empty;*/
COMMIT;