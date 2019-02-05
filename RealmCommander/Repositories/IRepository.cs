using System.Collections.Generic;

namespace RealmCommander.Repositories
{
  public interface IRepository<T, Tid>
  {
    T Create(T t);
    T FindById(Tid id);
    List<T> Find();
    bool Delete(Tid id);
  }
}