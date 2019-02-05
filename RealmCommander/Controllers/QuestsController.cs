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
  public class QuestsController : ControllerBase
  {
    private readonly QuestsService _service;

    // Get All
    [HttpGet]
    public ActionResult<IEnumerable<Quest>> Get()
    {
      return Ok(_service.Find());
    }

    // Get One usually by ID
    [HttpGet("{id}")]
    public ActionResult<Quest> Get(int id)
    {
      return Ok(_service.FindById(id));
    }

    // Create One
    [HttpPost]
    public ActionResult<Quest> Create([FromBody] Quest q)
    {
      return Ok(_service.Create(q));
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

    public QuestsController(QuestsService service)
    {
      _service = service;
    }
  }
}