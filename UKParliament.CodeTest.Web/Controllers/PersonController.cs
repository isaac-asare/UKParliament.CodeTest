using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController(IPersonService personService) : ControllerBase
{
    private readonly IPersonService _personService = personService;
    private static readonly string[] mismatchError = ["Mismatched Person ID."];

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonModel>>> GetAll()
    {
        var people = await _personService.GetAllAsync();
        return Ok(people);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonModel>> Get(int id)
    {
        var person = await _personService.GetByIdAsync(id);
        if (person == null)
            return NotFound();

        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<PersonModel>> Create([FromBody] PersonModel personModel)
    {
        var (Success, Errors, Result) = await _personService.CreateAsync(personModel);

        if (!Success) return BadRequest(new { errors = Errors });

        return CreatedAtAction(nameof(Get), new { id = Result!.Id }, Result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PersonModel personModel)
    {
        if (id != personModel.Id) return BadRequest(new { errors = mismatchError });

        var (Success, Errors) = await _personService.UpdateAsync(personModel);

        if (!Success) return BadRequest(new { errors = Errors });
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var person = await _personService.GetByIdAsync(id);
        if (person == null) return NotFound();

        await _personService.DeleteAsync(id);
        return NoContent();
    }
}