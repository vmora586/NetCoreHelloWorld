using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.Contexts;
using BooksApi.Dtos;
using BooksApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper=mapper;
            this._context = context;
        }

        /// <summary>
        /// Get all existing authors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> Get()
        {
            var authors= await _context.Authors.Include(a => a.Books).ToListAsync();
            var authorDtos=_mapper.Map<List<AuthorDto>>(authors);
            return authorDtos;
        }

        /// <summary>
        /// Gets an Author by its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<ActionResult<AuthorDto>> Get(int id)
        {
            var author = await _context.Authors.Include(a => a.Books).
                        FirstOrDefaultAsync(a => a.id == id);

            if (author == null)
            {
                return NotFound();
            }

            return _mapper.Map<AuthorDto>(author);
        }

        /// <summary>
        /// Creates a new Author
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreateAuthorDto createAuthorDto)
        {
            var author=_mapper.Map<Author>(createAuthorDto);
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            var authorDto= _mapper.Map<AuthorDto>(author);
            return CreatedAtRoute("GetAuthor", new { Id = author.id }, authorDto);
        }

        /// <summary>
        /// Updates an Author based on its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateAuthorDto updateAuthorDto)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            author.id=id;
            author.name = updateAuthorDto.name;
            _context.Entry(author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Deletes an Author based on its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NoContent();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}