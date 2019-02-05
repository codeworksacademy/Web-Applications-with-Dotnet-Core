using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealmCommander.Models;
using RealmCommander.Services;

namespace RealmCommander.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;

    [HttpPost("login")]
    public ActionResult<User> Login([FromBody] UserLogin creds)
    {
      try
      {
        return Ok(_accountService.Login(creds));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost("register")]
    public ActionResult<User> Register([FromBody] UserRegistration creds)
    {
      try
      {
        return Ok(_accountService.Register(creds));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpGet("authenticate")]
    public ActionResult<User> Authenticate()
    {
      return Ok(_accountService.User);
    }


    public AccountController(AccountService accountService)
    {
      _accountService = accountService;
    }

  }
}