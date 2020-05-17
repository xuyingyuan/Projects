using AutoMapper;
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
    [Route("api/authorcollection")]
    public class AuthorCollectionController: ControllerBase
    {
        private readonly ICourseLibraryRepository _courselibraryRepository;
        private readonly IMapper _mapper;
        public AuthorCollectionController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courselibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection(IEnumerable<AuthorForCreationDto> authorlist)
        {
            if (authorlist == null)
                return BadRequest();

            
            List<AuthorDto> authorDtos = new List<AuthorDto>();

            foreach(var author in authorlist)
            {
                var authorEntity = _mapper.Map<Entities.Author>(author);
                _courselibraryRepository.AddAuthor(authorEntity);
                _courselibraryRepository.Save();
                authorDtos.Add(_mapper.Map<AuthorDto>(authorEntity));
            }

            return Ok(authorDtos);
        }

    }
}
