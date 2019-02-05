using System.Data;
using Dapper;

namespace RealmCommander.DataHelpers
{
  public class DbSetup
  {
    private readonly IDbConnection _db;


    public void CreateTables()
    {
      _db.ExecuteScalar(@"
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
      ");
    }


    public void DropTables()
    {
      _db.ExecuteScalar(@"
      DROP TABLE IF EXISTS assignedQuests; 
      DROP TABLE IF EXISTS knights; 
      DROP TABLE IF EXISTS quests;
      DROP TABLE IF EXISTS users; 
      ");
    }


    public DbSetup(IDbConnection db)
    {
      _db = db;
    }
  }
}