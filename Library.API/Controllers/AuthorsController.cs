using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Service.DataAccess;
using Library.API.Models;
using Library.API.Helpers;
using AutoMapper;
using Entities.Entities;

namespace Library.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private readonly ILibraryRepository libaryRepository;

        public AuthorsController(ILibraryRepository libaryRepository)
        {
            this.libaryRepository = libaryRepository;
        }

        [HttpGet()]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = libaryRepository.GetAuthors();

            var authors = Mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo);
            return Ok(authors);
        }

        [HttpGet("{id}", Name ="GetAuthor")]
        public IActionResult GetAuthor(Guid id)
        {            
            var authorFromRepo = libaryRepository.GetAuthor(id);
            
            if(authorFromRepo == null)
            {
                return NotFound();
            }
            
            var author = Mapper.Map<AuthorDto>(authorFromRepo);
            return Ok(author);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] AuthorCreationDto author)
        {
            if(author == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var authorEntity = Mapper.Map<Author>(author);

            libaryRepository.AddAuthor(authorEntity);
            if (!libaryRepository.Save())
            {
                throw new Exception("Creating an author failed on save");
                //return StatusCode(500, "A problem happened with handling your request");
            }

            var authorToReturn = Mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor", new { id = authorToReturn.Id });
        }
    }
}