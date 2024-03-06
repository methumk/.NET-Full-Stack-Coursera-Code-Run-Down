CREATE DATABASE WorkoutDB;
USE WorkoutDB;

CREATE TABLE Users
(

	Id NVARCHAR(450) NOT NULL PRIMARY KEY,
	Username VARCHAR(255) NOT NULL UNIQUE,
	Email NVARCHAR(255) NOT NULL UNIQUE,
	Password NVARCHAR(255) NOT NULL,
);	


CREATE TABLE Workouts
(
	Id NVARCHAR(450) NOT NULL PRIMARY KEY,

	User_id NVARCHAR(450) NOT NULL,
	FOREIGN KEY (User_id) REFERENCES Users(Id),

	Exercise VARCHAR(255) NOT NULL,
	Weight INT NOT NULL CHECK(weight >= 0),
	Sets INT NOT NULL CHECK(sets >= 1),
	Reps INT NOT NULL CHECK(reps >= 0),
	Rest FLOAT CHECK(rest >= 0),
	Date DATE NOT NULL,
);