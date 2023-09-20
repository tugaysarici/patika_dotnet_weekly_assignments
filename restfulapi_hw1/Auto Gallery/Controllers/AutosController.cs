using Auto_Gallery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    public async Task<ActionResult<IEnumerable<Auto>>> GetAutos()
    {
        if(_dbContext.Autos == null)
        {
            return NotFound();
        }

        return await _dbContext.Autos.ToListAsync();
    }

    // Get: api/Autos/id/6
    [HttpGet("id")]
    public async Task<ActionResult<Auto>> GetAuto(int id)
    {
        if(_dbContext.Autos == null)
        {
            return NotFound();
        }

        var auto = await _dbContext.Autos.FindAsync(id);

        if(auto == null)
        {
            return NotFound();
        }

        return auto;
    }

    // Post: api/Autos
    [HttpPost]
    public async Task<ActionResult<Auto>> PostAuto(Auto auto)
    {
        _dbContext.Autos.Add(auto);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAuto), new { id = auto.Id }, auto);
    }

    // Put: api/Autos/6
    [HttpPut]
    public async Task<IActionResult> PutAuto(int id, Auto auto)
    {
        if(id != auto.Id)
        {
            return BadRequest();
        }

        _dbContext.Entry(auto).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            if(!AutoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // Delete: api/Auto/6
    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAuto(int id)
    {
        if (_dbContext.Autos == null)
        {
            return NotFound();
        }

        var auto = await _dbContext.Autos.FindAsync(id);
        if(auto == null)
        {
            return NotFound();
        }
        _dbContext.Autos.Remove(auto);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool AutoExists(long id)
    {
        return (_dbContext.Autos?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}