using System.Collections.Generic;
using RealmCommander.Models;
using RealmCommander.Repositories;

namespace RealmCommander.Services
{
  public class KnightsService
  {
    private readonly KnightsRepository _repo;
    private readonly AccountService _accountService;

    public KnightsService(KnightsRepository repo, AccountService accountService)
    {
      _repo = repo;
      _accountService = accountService;
    }

    public Knight Create(Knight knight)
    {
      knight.UserId = _accountService.User.Id;
      return _repo.Create(knight);
    }

    public bool Delete(int id)
    {
      return _repo.Delete(id);
    }

    public Knight FindById(int id)
    {
      return _repo.FindById(id);
    }

    public List<Knight> Find()
    {
      return _repo.Find();
    }

  }
}