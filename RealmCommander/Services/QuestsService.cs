using System.Collections.Generic;
using RealmCommander.Models;
using RealmCommander.Repositories;

namespace RealmCommander.Services
{
  public class QuestsService
  {
    private readonly QuestsRepository _repo;
    private readonly AccountService _accountService;

    public QuestsService(QuestsRepository repo, AccountService accountService)
    {
      _repo = repo;
      _accountService = accountService;
    }

    public Quest Create(Quest q)
    {
      // business rule
      // user == role general
      // allow the quest.completed

      if (!Roles.General.HasFlag(_accountService.User.Role))
      {
        q.Completed = false;
      }
      q.UserId = _accountService.User.Id;
      return _repo.Create(q);
    }

    public bool Delete(int id)
    {
      return _repo.Delete(id);
    }

    public Quest FindById(int id)
    {
      return _repo.FindById(id);
    }

    public List<Quest> Find()
    {
      return _repo.Find();
    }


  }
}