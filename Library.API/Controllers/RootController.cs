﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api")]
    public class RootController : Controller
    {
        private readonly IUrlHelper _urlHelper;

        public RootController(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot([FromHeader] string mediaType)
        {
            if (mediaType.Equals("application/vnd.marvin.hateos+json"))
            {
                var links = new List<LinkDto>();

                links.Add(
                    new LinkDto(_urlHelper.Link("GetRoot", new { }),
                        "self",
                        "GET"));

                links.Add(
                    new LinkDto(_urlHelper.Link("GetAuthors", new { }),
                        "authors",
                        "GET"));

                links.Add(
                    new LinkDto(_urlHelper.Link("CreateAuthor", new { }),
                        "create_author",
                        "POST"));

                return Ok(links);
            }

            return NoContent();

        }
    }
}
