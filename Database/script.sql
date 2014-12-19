SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE if exists Personen;
DROP TABLE if exists Score;
SET FOREIGN_KEY_CHECKS = 1;

CREATE TABLE Personen
(
	inlognaam varchar(50) PRIMARY KEY,
	wachtwoord varchar(50) NOT NULL
);

CREATE TABLE Score
(
	scoreID int(100) PRIMARY KEY auto_increment,
	inlognaam varchar(50) NOT NULL,
	score int(100) NOT NULL,

	CONSTRAINT FK_inlognaam FOREIGN KEY (inlognaam) REFERENCES Personen(inlognaam) ON DELETE CASCADE ON UPDATE CASCADE
);

START TRANSACTION;
SAVEPOINT sp_Empty;

INSERT INTO Personen(inlognaam, wachtwoord)
VALUES('mel', 'w');
INSERT INTO Personen(inlognaam, wachtwoord)
VALUES('alex', '123');
INSERT INTO Personen(inlognaam, wachtwoord)
VALUES('bart', 'bb');

INSERT INTO Score(inlognaam, score)
VALUES('mel', 1100);
INSERT INTO Score(inlognaam, score)
VALUES('alex', 1450);
INSERT INTO Score(inlognaam, score)
VALUES('bart', 760);

SELECT * FROM Personen;
SELECT * FROM Score;

/*ROLLBACK TO sp_Empty;*/
COMMIT;