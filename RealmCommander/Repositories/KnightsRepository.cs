using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using RealmCommander.Models;

namespace RealmCommander.Repositories
{
  public class KnightsRepository : IRepository<Knight, int>
  {
    private readonly IDbConnection _db;
    public KnightsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Knight Create(Knight knight)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO knights (name, userId) VALUES (@Name, @UserId); 
      SELECT LAST_INSERT_ID()", knight
      );
      knight.Id = id;
      return knight;
    }

    public bool Delete(int id)
    {
      int success = _db.Execute(@"
        DELETE FROM knights WHERE id = @id
      ", new { id });
      return success > 0;
    }


    public Knight FindById(int id)
    {
      return _db.Query<Knight>(@"
      SELECT * FROM knights WHERE id = @id
      ", new { id }).FirstOrDefault();
    }

    public List<Knight> Find()
    {
      return _db.Query<Knight>(@"
      SELECT * FROM knights
      ").ToList();
    }
  }
}