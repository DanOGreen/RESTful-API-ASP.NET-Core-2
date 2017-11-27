using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Service.DataAccess;
using Library.API.Models;
using AutoMapper;
using Entities.Entities;
using Library.API.Helpers;

namespace Library.API.Controllers
{
    [Route("api/authorcollections")]
    public class AuthorCollectionsController : Controller
    {
        private readonly ILibraryRepository libraryRepository;

        public AuthorCollectionsController(ILibraryRepository libraryRepository)
        {
            this.libraryRepository = libraryRepository;
        }

        [HttpPost]
        public IActionResult CreateAuthorCollection([FromBody] IEnumerable<AuthorCreationDto> authorCollection)
        {
            if(authorCollection == null)
            {
                return BadRequest();
            }

            var authorEntities = Mapper.Map<IEnumerable<Author>>(authorCollection);

            foreach (var author in authorEntities)
            {
                libraryRepository.AddAuthor(author);
            }

            if (!libraryRepository.Save())
            {
                throw new Exception("Creation of author collection failed");
            }
            var authorCollectionToReturn = Mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            var idsAsString = string.Join(",", authorCollectionToReturn.Select(a => a.Id));
            return CreatedAtRoute("GetAuthorCollection", new { ids = idsAsString }, authorCollectionToReturn);
        }

        [HttpGet("{ids}",Name ="GetAuthorCollection")]
        public IActionResult GetAuthorCollections([ModelBinder(BinderType = typeof(ArrayModelBinder)]IEnumerable<Guid> ids)
        {
            if(ids == null)
            {
                return BadRequest();
            }


            var authorEntities = libraryRepository.GetAuthors(ids);

            if(ids.Count() != authorEntities.Count())
            {
                return NotFound();
            }

            var authorsToReturn = Mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            return Ok(authorsToReturn);
        }
    }
}