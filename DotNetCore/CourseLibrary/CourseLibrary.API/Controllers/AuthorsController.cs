using AutoMapper;
using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

        [HttpGet(Name ="GetAuthors")]
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

         

            var previousPageLink = authors.HasPrevious ? CreateAuthorsResourceUri(authorResourceParameters, ResourceUriType.PreviousPage) : null;
            var nextPagelink = authors.HasNext ? CreateAuthorsResourceUri(authorResourceParameters, ResourceUriType.NextPage) : null;
            var paginationMetaData = new
            {
                totalCount = authors.TotalCount,
                pageSize = authors.PageSize,
                currentPage = authors.CurrentPage,
                totalPages = authors.TotalPages,
                previousPageLink = previousPageLink,
                nextPagelink = nextPagelink
            };

            //add metadata to header:
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
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

        [HttpDelete("{authorId}")]
        public ActionResult DeleteAuthor(Guid authorId)
        {
            var authorToDelete = _courselibraryRepository.GetAuthor(authorId);
            if (authorToDelete==null)
            {
                return NotFound();
            }
            //No need below, when parent is deleted then the child will be deleted too            
            //var coursesForAuthor = _courselibraryRepository.GetCourses(authorId);
            //if(coursesForAuthor.Count() > 0)
            //{
            //    foreach(var courseForAuthor in coursesForAuthor)
            //    {
            //        _courselibraryRepository.DeleteCourse(courseForAuthor);
            //    }
            //}
            _courselibraryRepository.DeleteAuthor(authorToDelete);
            _courselibraryRepository.Save();
            return NoContent();
        }

        private string CreateAuthorsResourceUri(AuthorResourceParameters authorResourceParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetAuthors",
                        new { pageNumber = authorResourceParameters.PageNumber-1,
                        pageSize = authorResourceParameters.PageSize,
                        mainCategory = authorResourceParameters.MainCategory,
                        searchQuery = authorResourceParameters.SearchQuery});
                case ResourceUriType.NextPage:
                    return Url.Link("GetAuthors", 
                        new {
                        pageNumber = authorResourceParameters.PageNumber + 1,
                        pageSize = authorResourceParameters.PageSize,
                        mainCategory = authorResourceParameters.MainCategory,
                        searchQuery = authorResourceParameters.SearchQuery
                    });
                default:
                    return Url.Link("GetAuthors",
                        new
                        {
                            pageNumber = authorResourceParameters.PageNumber,
                            pageSize = authorResourceParameters.PageSize,
                            mainCategory = authorResourceParameters.MainCategory,
                            searchQuery = authorResourceParameters.SearchQuery

                        });
            }

        }
    }
}
