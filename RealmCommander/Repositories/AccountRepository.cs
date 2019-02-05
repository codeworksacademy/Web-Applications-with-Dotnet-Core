using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using RealmCommander.Models;

namespace RealmCommander.Repositories
{
  public class AccountRepository : IRepository<User, string>
  {
    private readonly IDbConnection _db;

    public User Create(User u)
    {
      int rows = _db.Execute(@"
      INSERT INTO users (id, name, email, hash, role)
      VALUES (@Id, @Name, @Email, @Hash, @Role);
      ", new
      {
        Id = u.Id,
        Name = u.Name,
        Email = u.Email,
        Hash = u.Hash,
        Role = u.Role
      });

      if (rows != 1)
      {
        return null;
      }

      return u;
    }

    public bool Delete(string id)
    {
      return _db.Execute("REMOVE FROM users WHERE id = @id", new { id }) == 1;
    }

    internal User FindByEmail(string email)
    {
      return _db.Query<User>("SELECT * FROM users WHERE email = @email", new { email }).FirstOrDefault();
    }

    public List<User> Find()
    {
      throw new System.NotImplementedException();
    }

    public User FindById(string id)
    {
      return _db.Query<User>("SELECT * FROM users WHERE id = @id", new { id }).FirstOrDefault();
    }

    public AccountRepository(IDbConnection db)
    {
      _db = db;
    }
  }
}