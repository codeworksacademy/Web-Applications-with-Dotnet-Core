-- TABLE CREATION
CREATE TABLE IF NOT EXISTS users (
  id VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL,
  hash VARCHAR(255) NOT NULL,
  role int NOT NULL,

  PRIMARY KEY (id),
  UNIQUE KEY (email)
);


CREATE TABLE IF NOT EXISTS knights(
  id int AUTO_INCREMENT NOT NULL,
  userId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,

  INDEX userId ( userId),
  FOREIGN KEY (userId)
    REFERENCES users(id)
    ON DELETE CASCADE,

  PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS quests(
  id int AUTO_INCREMENT NOT NULL,
  userId VARCHAR(255) NOT NULL,
  title VARCHAR(255) NOT NULL,
  description VARCHAR(255),
  completed TINYINT,

  INDEX userId ( userId),
  FOREIGN KEY (userId)
    REFERENCES users(id)
    ON DELETE CASCADE,

  PRIMARY KEY (id)
);

CREATE TABLE assignedQuests (
  id int AUTO_INCREMENT NOT NULL,
  questId int NOT NULL,
  knightId int NOT NULL,
  userId VARCHAR(255) NOT NULL,

  PRIMARY KEY (id),
  INDEX (questId, knightId),
  INDEX (userId),

  FOREIGN KEY (userId)
    REFERENCES users(id)
    ON DELETE CASCADE,

  FOREIGN KEY (knightId)
    REFERENCES knights(id)
    ON DELETE CASCADE,

  FOREIGN KEY (questId)
    REFERENCES quests(id)
    ON DELETE CASCADE
);


-- INSERTING FAKE DATA
INSERT INTO knights (name) VALUES ("Sir Knight");
SELECT * FROM knights;

INSERT INTO quests (title, description, completed)
VALUES ("Find the Great Wizard", "", 0)

INSERT INTO quests (title, completed)
VALUES ("Seek the Holy Grail", 0)

SELECT * FROM quests

-- UPDATING RECORDS
UPDATE quests
SET description = "He goes by the name Merlin", completed = 1
WHERE id = 1;

-- ALTERING TABLES

ALTER TABLE knights
ADD COLUMN weapon VARCHAR(255) DEFAULT "Sword";


-- REMOVE RECORD
DELETE FROM knights WHERE id = 1;
SELECT * FROM knights;