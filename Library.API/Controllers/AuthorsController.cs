using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Service.DataAccess;

namespace Library.API.Controllers
{
    [Route("api / authors")]
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
            return new JsonResult(authorsFromRepo);
        }
    }
}