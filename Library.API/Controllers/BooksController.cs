using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Service.DataAccess;
using AutoMapper;
using Library.API.Models;

namespace Library.API.Controllers
{
    [Route("api/authors/{authorId}/books")]
    public class BooksController : Controller
    {
        private readonly ILibraryRepository liraryRepository;

        public BooksController(ILibraryRepository liraryRepository)
        {
            this.liraryRepository = liraryRepository;
        }

        [HttpGet]
        public IActionResult GetBooksForAuuthor(Guid authorId)
        {
            if (!liraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var booksForAuthorfromRepo = liraryRepository.GetBooksForAuthor(authorId);

            var booksForAuthor = Mapper.Map<IEnumerable<BookDto>>(booksForAuthorfromRepo);
            return Ok(booksForAuthorfromRepo);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookForAuuthor(Guid authorId, Guid id)
        {
            if (!liraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var bookForAuthorfromRepo = liraryRepository.GetBookForAuthor(authorId,id);


            Mapper.Map<BookDto>(bookForAuthorfromRepo);

            return Ok(bookForAuthorfromRepo);
        }

    }
}