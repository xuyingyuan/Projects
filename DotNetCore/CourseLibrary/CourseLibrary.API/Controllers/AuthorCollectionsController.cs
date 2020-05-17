using AutoMapper;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authorcollections")]
    public class AuthorCollectionsController: ControllerBase
    {
        private readonly ICourseLibraryRepository _courselibraryRepository;
        private readonly IMapper _mapper;
        public AuthorCollectionsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courselibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetAuthorCollection")]

        public IActionResult GetAuthorCollection([FromRoute] 
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
                return BadRequest();

            var authorEntity = _courselibraryRepository.GetAuthors(ids);
            if(ids.Count() != authorEntity.Count())
            {
                return NotFound();
            }

            var authorsDtos = _mapper.Map<IEnumerable<AuthorDto>>(authorEntity);
            return Ok(authorsDtos);
        }

        [HttpPost]
        public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection(IEnumerable<AuthorForCreationDto> authorlist)
        {
            if (authorlist == null)
                return BadRequest();

            var authorEntityList = _mapper.Map<IEnumerable<Entities.Author>>(authorlist);
            
         

            foreach(var authorEntity in authorEntityList)
            {               
                _courselibraryRepository.AddAuthor(authorEntity);             
            }

            _courselibraryRepository.Save();

            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authorEntityList);
            var authorIds = string.Join(",", authorDtos.Select(a => a.Id));
            return CreatedAtRoute("GetAuthorCollection", new { ids = authorIds }, authorDtos);
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {

        }
    }
}
