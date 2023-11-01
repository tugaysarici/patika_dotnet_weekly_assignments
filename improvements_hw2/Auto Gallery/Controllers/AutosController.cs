using Auto_Gallery.Attributes;
using Auto_Gallery.Models;
using Auto_Gallery.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auto_Gallery.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutosController : ControllerBase
{
    private readonly AutoContext  _dbContext;

    public AutosController(AutoContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET: api/Autos
    [HttpGet]
    public List<Auto> GetAutos()
    {
        return _dbContext.Autos.ToList();
    }

    // Get: api/Autos/id/6
    [HttpGet("{id}")]
    public Auto GetAuto(int id)
    {
        var auto = _dbContext.Autos.Find(id);
        if (auto == null)
            throw new Exception("Record not found.");

        return auto;
    }

    // Post: api/Autos
    [HttpPost]
    public IActionResult PostAuto([FromBody] Auto auto)
    {
        var validator = new AutoValidator();
        var result = validator.Validate(auto);
        if (!result.IsValid) return BadRequest(result.Errors.Select(e => e.ErrorMessage).ToList());

        if (auto == null) return BadRequest();

        _dbContext.Autos.Add(auto);
        _dbContext.SaveChanges();
        return Ok("Recorded successfully.");
    }

    // Put: api/Autos/6
    [HttpPut("{id}")]
    public IActionResult PutAuto(int id, [FromQuery] bool? status, [FromQuery] string? brand, [FromQuery] string? model, [FromQuery] int? modelYear,
                        [FromQuery] DateTime? recordDate, [FromQuery] DateTime? soldDate, [FromQuery] int? price)
    {
        var autoToUpdate = _dbContext.Autos.FirstOrDefault(x => x.Id == id);
        if (autoToUpdate != null)
        {
            if (status != null) autoToUpdate.Status = status.Value;
            if (brand != null) autoToUpdate.Brand = brand.ToString();
            if (model != null) autoToUpdate.Model = model.ToString();
            if (modelYear != null) autoToUpdate.ModelYear = modelYear.Value;
            if (recordDate != null) autoToUpdate.RecordDate = recordDate.Value;
            if (soldDate != null) autoToUpdate.SoldDate = soldDate.Value;
            if(price != null) autoToUpdate.Price = price.Value;

            _dbContext.SaveChanges();
            return Ok("Record updated successfully.");
        }
        return NotFound("Record not found.");
    }

    // Delete: api/Auto/6
    [HttpDelete("{id}")]
    public IActionResult DeleteAuto(int id)
    {
        var autoToDelete = _dbContext.Autos.FirstOrDefault(x => x.Id == id);

        if(autoToDelete != null)
        {
            _dbContext.Autos.Remove(autoToDelete);
            _dbContext.SaveChanges();
            return Content("Record deleted successfully.");
        }
        return NotFound("Record not found.");
    }

    [HttpGet("FakeUser")]
    [AuthorizeFakeUser]
    public List<Auto> GetAutosForFakeUser([FromQuery] string name)
    {
        var autos = _dbContext.Autos.Where(x => x.Brand.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        return autos;
    }
}