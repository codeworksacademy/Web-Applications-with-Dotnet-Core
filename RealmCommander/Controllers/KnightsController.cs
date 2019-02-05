using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealmCommander.Models;
using RealmCommander.Services;

namespace RealmCommander.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class KnightsController : ControllerBase
  {
    private readonly KnightsService _service;

    // Get All
    [HttpGet]
    public ActionResult<IEnumerable<Knight>> Get()
    {
      return Ok(_service.Find());
    }

    // Get One usually by ID
    [HttpGet("{id}")]
    public ActionResult<Knight> Get(int id)
    {
      return Ok(_service.FindById(id));
    }

    // Create One
    [HttpPost]
    public ActionResult<Knight> Create([FromBody] Knight knight)
    {
      return Ok(_service.Create(knight));
    }

    // Edit One
    [HttpPut("{id}")]
    public void Update(int id)
    {

    }

    // Delete One
    [HttpDelete("{id}")]
    public ActionResult<bool> Delete(int id)
    {
      return Ok(_service.Delete(id));
    }


    public KnightsController(KnightsService service)
    {
      _service = service;
    }


  }
}