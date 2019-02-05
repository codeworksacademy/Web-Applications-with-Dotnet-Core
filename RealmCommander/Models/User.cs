using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RealmCommander.Models
{

  public class UserRegistration : UserLogin
  {
    [Required]
    public string Name { get; set; }
  }

  public class UserLogin
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
  }

  public class User
  {
    public string Id { get; private set; }

    [Required]
    public string Name { get; private set; }

    [Required]
    [EmailAddress]
    public string Email { get; private set; }

    public Roles Role { get; private set; }

    internal string Hash { get; private set; }

    internal ClaimsPrincipal Principal { get; private set; }

    internal void SetClaims()
    {
      var claims = new List<Claim>()
      {
        new Claim(ClaimTypes.Name, Id)
      };
      var userIdentity = new ClaimsIdentity(claims, "login");
      Principal = new ClaimsPrincipal(userIdentity);
    }

    public void MOCKUSER()
    {
      Id = "8fdsa89u3289ujfds0a32";
      Email = "TEST@TEST.COM";
      Role = Roles.Ruler;
    }

    public static User CreateUser(UserRegistration creds, string hash)
    {
      return new User()
      {
        Id = Guid.NewGuid().ToString(),
        Name = creds.Name,
        Email = creds.Email,
        Hash = hash,
        Role = Roles.Knight
      };
    }

  }
}