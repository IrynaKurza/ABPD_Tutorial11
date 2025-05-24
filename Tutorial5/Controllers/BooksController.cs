using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.Services;

namespace Tutorial5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IDbService _dbService;
        public BooksController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _dbService.GetBooks();
            return Ok(books);
        }
    }
}
