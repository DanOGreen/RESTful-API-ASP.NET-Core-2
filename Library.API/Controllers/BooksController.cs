using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Service.DataAccess;
using AutoMapper;
using Library.API.Models;
using Entities.Entities;

namespace Library.API.Controllers
{
    [Route("api/authors/{authorId}/books")]
    public class BooksController : Controller
    {
        private readonly ILibraryRepository libraryRepository;

        public BooksController(ILibraryRepository libraryRepository)
        {
            this.libraryRepository = libraryRepository;
        }

        [HttpGet]
        public IActionResult GetBooksForAuuthor(Guid authorId)
        {
            if (!libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var booksForAuthorfromRepo = libraryRepository.GetBooksForAuthor(authorId);

            var booksForAuthor = Mapper.Map<IEnumerable<BookDto>>(booksForAuthorfromRepo);
            return Ok(booksForAuthorfromRepo);
        }

        [HttpGet("{id}", Name ="GetBookForAuthor")]
        public IActionResult GetBookForAuuthor(Guid authorId, Guid id)
        {
            if (!libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var bookForAuthorfromRepo = libraryRepository.GetBookForAuthor(authorId,id);


            Mapper.Map<BookDto>(bookForAuthorfromRepo);

            return Ok(bookForAuthorfromRepo);
        }

        [HttpPost]
        public IActionResult CreateBookForAuthor(Guid authorId, [FromBody] BookCreationDto book)
        {
            if(book == null)
            {
                return BadRequest();
            }

            if (!libraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookEntity = Mapper.Map<Book>(book);
            libraryRepository.AddBookForAuthor(authorId, bookEntity);
            if (!libraryRepository.Save())
            {
                throw new BadImageFormatException($"Creating a book for {authorId} failed on save");
            }

            var bookToReturn = Mapper.Map<BookDto>(bookEntity);

            return CreatedAtRoute("GetBookForAuthor", new { authorId = authorId, id = bookToReturn.Id }, bookToReturn);
        }

    }
}