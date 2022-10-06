using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RStore.Api.Data;
using RStore.Api.Dto.Author;
using RStore.Api.Static;

namespace RStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthorsController : ControllerBase
{
    private readonly RStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthorsController> _logger;

    public AuthorsController(RStoreDbContext context, IMapper mapper, ILogger<AuthorsController> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    // GET: api/Authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorReadOnlyDto>>> GetAuthors()
    {
        try
        {
            var authors = await _context.Authors.ToListAsync();
            var authorDtos = _mapper.Map<IEnumerable<AuthorReadOnlyDto>>(authors);
            return Ok(authorDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error performing GET in {nameof(AuthorsController)}");
            return StatusCode(500, Messages.Error500Message);
        }
        
    }

    // GET: api/Authors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorReadOnlyDto>> GetAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            _logger.LogWarning($"Record with {id} not found in {nameof(GetAuthor)}");
            return NotFound();
        }

        return Ok(_mapper.Map<AuthorReadOnlyDto>(author));
    }

    // PUT: api/Authors/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
    {
        if (id != authorDto.Id)
        {
            return BadRequest();
        }

        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        _mapper.Map(authorDto, author);
        _context.Entry(author).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (! await AuthorExists(id))
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

    // POST: api/Authors
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorDto)
    {
        var author = _mapper.Map<Author>(authorDto);
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
    }

    // DELETE: api/Authors/5
    [HttpDelete("{id}")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> AuthorExists(int id)
    {
        return await _context.Authors.AnyAsync(e => e.Id == id);
    }
}
