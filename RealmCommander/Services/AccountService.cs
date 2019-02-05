using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using RealmCommander.Models;
using RealmCommander.Repositories;

namespace RealmCommander.Services
{
  public class AccountService
  {
    private readonly AccountRepository _repo;
    private HttpContext _httpContext;

    public User User { get; private set; }

    public void SetUser(string id)
    {
      User = _repo.FindById(id);
    }

    public User Login(UserLogin creds)
    {
      User u = _repo.FindByEmail(creds.Email);
      //invalid email
      if (u == null)
      {
        throw new Exception("Invalid User Credentials");
      }
      //invalid password
      if (!BCrypt.Net.BCrypt.Verify(creds.Password, u.Hash))
      {
        throw new Exception("Invalid User Credentials");
      }

      User = u;
      return SignInAsync().Result;
    }

    public User Register(UserRegistration creds)
    {
      string hash = BCrypt.Net.BCrypt.HashPassword(creds.Password);
      User u = User.CreateUser(creds, hash);
      User loggedInUser = _repo.Create(u);
      if (loggedInUser == null)
      {
        throw new Exception("Invalid User Credentials");
      }
      User = loggedInUser;
      return SignInAsync().Result;
    }

    private async Task<User> SignInAsync()
    {
      User.SetClaims();
      await _httpContext.SignInAsync(User.Principal);
      return User;
    }

    public void SetContext(HttpContext httpContext)
    {
      _httpContext = httpContext;
    }

    public AccountService(AccountRepository repo)
    {
      _repo = repo;
    }
  }
}