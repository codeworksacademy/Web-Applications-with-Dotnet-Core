using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PizzaShack.Mock;
using PizzaShack.Models;

namespace PizzaShack.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class PizzasController : ControllerBase
  {

    [HttpGet]
    public ActionResult<IEnumerable<Pizza>> Get()
    {
      return FakeDb.Pizzas;
    }
  }

}