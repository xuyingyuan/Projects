using AutoMapper;
using CourseLibrary.API.DbContexts;

using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/authors")]
    public class AuthorsController: ControllerBase
    {
        private readonly ICourseLibraryRepository _courselibraryRepository;
        private readonly IMapper _mapper;
        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courselibraryRepository = courseLibraryRepository?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery]AuthorResourceParameters authorResourceParameters)
        {
           var authors =  _courselibraryRepository.GetAuthors(authorResourceParameters);
         
            if (authors == null)
                return NotFound();
            //foreach (var author in authors)
            //{
            //    var authorDto = new AuthorDto()
            //    {
            //        Id = author.Id,
            //        Name = author.FirstName + " " + author.LastName,
            //        MainCategory = author.MainCategory,
            //        Age = author.DateOfBirth.getCurrentAge()
            //    };
            //    authorDtos.Add(authorDto);
            //}
            //return new JsonResult(authors);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpGet("{authorId:guid}", Name ="GetAuthor")]
        public IActionResult GetAuthors(Guid authorId)
        {   var author = _courselibraryRepository.GetAuthor(authorId);
            if(author == null)
            {
                return NotFound();
            }
                return Ok(_mapper.Map<AuthorDto>(author));

        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto author)
        {
            if(author == null)
            {
                return BadRequest();
            }

            var authorEntity = _mapper.Map<Entities.Author>(author);
            _courselibraryRepository.AddAuthor(authorEntity);
            _courselibraryRepository.Save();
            var authorDto = _mapper.Map<AuthorDto>(authorEntity);

            return CreatedAtRoute("GetAuthor", new { authorId = authorDto.Id }, authorDto);
        }
    }
}
