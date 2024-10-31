using Microsoft.AspNetCore.Mvc;
using SpecificationPattern.Models;
using SpecificationPattern.Repository.Interfaces;
using SpecificationPattern.Specification.Developers;

namespace SpecificationPattern.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DeveloperController : ControllerBase
{
    private readonly IGenericRepository<Developer> _repository;

    public DeveloperController(IGenericRepository<Developer> repository)
    {
        _repository = repository;
    }
    //Get developers with higher salary(api/Developer/highincome/[MinSalary])
    [HttpGet("highincome/{minSalary}")]
    public async Task<ActionResult<IEnumerable<Developer>>> DevWithHighSalary(decimal minSalary)
    {
        var specification = new DevWIthHighSalary(minSalary);
        var query = await _repository.GetAllAsync(specification);
        if (query == null) return NotFound(new {message = "Developers not found"});
        return Ok(query);
    }
    //Get developers with higher experience(api/Developer/highexp/[MinExp])
    [HttpGet("highexperience/{minExp}")]
    public async Task<ActionResult<IEnumerable<Developer>>> DevWithHighExp(int minExp)
    {
        var specification = new DevWithHighExperience(minExp);
        
        var query = await _repository.GetAllAsync(specification);
        if (query == null) return NotFound(new {message = "Developers not found"});
        return Ok(query);
    }
    //Get developers (api/Developer)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Developer>>> GetDevelopers(
        [FromQuery] int? minYearsOfExperience = null,
        [FromQuery] decimal? minSalary = null,
        [FromQuery] string? orderByField = null,
        [FromQuery] bool orderByAscending = true,
        [FromQuery] int? skip = null,
        [FromQuery] int? take = null
        )
    {
        var specification = new DeveloperSpecification(minYearsOfExperience, minSalary, orderByField, orderByAscending, skip, take);
        var developers = await _repository.GetAllAsync(specification);
        if (developers == null) return NotFound(new {message = "Developers not found"});
        return Ok(developers);
    }
    //GetById (api/Developer/id)
    [HttpGet("{id}")]
    public async Task<ActionResult<Developer>> GetDeveloper(int id)
    {
        if(id < 0) return BadRequest(new {message = "Invalid id"});
        var developer = await _repository.GetById(id);
        if(developer == null) return NotFound(new {message = "Developer not found"});
        return Ok(developer);
    }

    // Create developer (api/Developer/add)
    [HttpPost("add")]
    public async Task<ActionResult<Developer>> AddDeveloper(Developer developer)
    {
        if (developer == null) return BadRequest(new { message = "Invalid developer data" });
        if(!ModelState.IsValid) return BadRequest(ModelState);
        await _repository.AddAsync(developer);
        await _repository.SaveChangesAsync();
        return CreatedAtAction(nameof(GetDeveloper), new { id = developer.Id }, developer);
    }
    
    // update developer (api/Developer/update/{id})
    [HttpPut("update/{id}")]
    public async Task<ActionResult> UpdateDeveloper(int id, Developer developer)
    {
        if (id < 0) return BadRequest(new { message = "Invalid developer id" });
        if(developer == null) return BadRequest(new { message = "Invalid developer data" });
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var existingDeveloper = await _repository.GetById(id);
        if(existingDeveloper == null) return NotFound(new { message = "Developer not found" });
        existingDeveloper.Name = developer.Name;
        existingDeveloper.Email = developer.Email;
        existingDeveloper.YearsOfExperience = developer.YearsOfExperience;
        existingDeveloper.Income = developer.Income;
        existingDeveloper.Address = developer.Address;
        _repository.Update(existingDeveloper);
        await _repository.SaveChangesAsync();
        return NoContent();
    }
    //delete developer (api/developer/delete/id)
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteDeveloper(int id)
    {
        if(id < 0) return BadRequest(new { message = "Invalid developer id" });
        var developer = await _repository.GetById(id);
        if(developer == null) return NotFound(new { message = "Developer not found" });
         _repository.Delete(developer);
        await _repository.SaveChangesAsync();
        return NoContent();
    }
    
}