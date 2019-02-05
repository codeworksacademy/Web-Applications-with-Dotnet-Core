using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PizzaShack.Mock;
using PizzaShack.Models;

namespace PizzaShack.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class ToppingsController : ControllerBase
  {

    [HttpGet]
    public ActionResult<Dictionary<string, Topping>> Get()
    {
      return FakeDb.AvailToppings;
    }

  }

}