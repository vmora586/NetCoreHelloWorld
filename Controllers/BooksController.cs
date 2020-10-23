using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BooksApi.Contexts;
using BooksApi.Dtos;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public BooksController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper=mapper;
            _context = context;
        }

        /// <summary>
        /// Returns a Book based on its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}", Name = "SearchById")]
        public async Task<ActionResult<BookDto>> Get(int id)
        {
            var book = await _context.Books.Include(a => a.Author).
                             FirstOrDefaultAsync(b => b.id == id);

            if (book != null)
            {
                return Ok(_mapper.Map<BookDto>(book));
            }

            return NotFound();
        }

        /// <summary>
        /// Gets all Book including its authors 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            var books = await _context.Books.Include(b => b.Author).ToListAsync();
            return Ok(books);
        }

        /// <summary>
        /// Creates a new Book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Book book)
        {
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
            return CreatedAtRoute("SearchById", new { Id = book.id }, book);
        }

        /// <summary>
        /// Updates an existing book by its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newBook"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody] Book newBook)
        {
            if (id != newBook.id)
            {
                return BadRequest("The id sent does not match ");
            }

            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            book.Tittle = newBook.Tittle;
            book.AuthorId = newBook.AuthorId;
            book.Tittle = newBook.Tittle;
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        /// <summary>
        /// Remove a book based on its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NoContent();
            }

            _context.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}