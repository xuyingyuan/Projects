using AutoMapper;
using CourseLibrary.API.ActionConstraints;
using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
using CourseLibrary.API.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Net.Http.Headers;
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
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckingService _propertyCheckingService;
        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper, 
                IPropertyMappingService propertyMappingService,
                IPropertyCheckingService propertyCheckingService)
        {
            _courselibraryRepository = courseLibraryRepository?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper?? throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckingService = propertyCheckingService ?? throw new ArgumentNullException(nameof(propertyCheckingService));
        }

        [HttpGet(Name ="GetAuthors")]
        [HttpHead]
        public IActionResult GetAuthors([FromQuery]AuthorResourceParameters authorResourceParameters, [FromHeader(Name = "Accept")] string mediatype)
        {

            if (!MediaTypeHeaderValue.TryParse(mediatype, out MediaTypeHeaderValue parsedMeidaType))
            {
                return BadRequest();
            }
            var vendorMediaType = (parsedMeidaType.MediaType == "application/vnd.marvin.hateoas+json");
                if (!_propertyMappingService.ValidMappingExistsFor<AuthorDto, Author>(authorResourceParameters.OrderBy))
            {
                return BadRequest();
            }

            if(!_propertyCheckingService.TypeHasProperties<AuthorDto>(authorResourceParameters.Fields))
            { return BadRequest(); 
            }
            var authors =  _courselibraryRepository.GetAuthors(authorResourceParameters);
         
            if (authors is null)
                return NotFound();

         
            if (vendorMediaType)
            {
                var paginationMetaData = new
                {
                    totalCount = authors.TotalCount,
                    pageSize = authors.PageSize,
                    currentPage = authors.CurrentPage,
                    totalPages = authors.TotalPages

                    //previousPageLink = previousPageLink,
                    //nextPagelink = nextPagelink
                };
                //add metadata to header:
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
                
                var links = CreateLinkForAuthors(authorResourceParameters, authors.HasNext, authors.HasPrevious);
                var shapedAuthors = _mapper.Map<IEnumerable<AuthorDto>>(authors).ShapeData(authorResourceParameters.Fields);
                var shapedAuthorWithLinks = shapedAuthors.Select(author =>
                {
                    var authorAsDictionary = author as IDictionary<string, object>;
                    var authorLinks = CreateLinkForAuthor((Guid)authorAsDictionary["Id"], null);
                    authorAsDictionary.Add("links", authorLinks);
                    return authorAsDictionary;
                });

                var linkedCollectionResource = new
                {
                    value = shapedAuthorWithLinks,
                    links
                };

                return Ok(linkedCollectionResource);
            }
            else
            {
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
            }
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors).ShapeData(authorResourceParameters.Fields));
        }

        [Produces("application/json",
            "application/vnd.marvin.hateoas+json",
            "application/vnd.marvin.author.full.hateoas+json",
            "application/vnd.marvin.author.full+json",
            "application/vnd.marvin.author.friendly.hateoas+json",
            "application/vnd.marvin.author.friendly+json")]
        [HttpGet("{authorId:guid}", Name ="GetAuthor")]
        public IActionResult GetAuthors(Guid authorId, string fields, [FromHeader(Name ="Accept")] string mediatype)
        {
           if(!MediaTypeHeaderValue.TryParse(mediatype, out MediaTypeHeaderValue parsedMeidaType))
            {
                return BadRequest();
            }
            
            if (!_propertyCheckingService.TypeHasProperties<AuthorDto>(fields))
            {
                return BadRequest();
            }

            var author = _courselibraryRepository.GetAuthor(authorId);
            if(author == null)
            {
                return NotFound();
            }

            IEnumerable<LinkDto> links = new List<LinkDto>() ;
            var includeLinks = parsedMeidaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
            if(includeLinks)
                 links = CreateLinkForAuthor(authorId, fields);

            var primaryMedieType = includeLinks ? parsedMeidaType.SubTypeWithoutSuffix.Substring(0, parsedMeidaType.SubTypeWithoutSuffix.Length - 8) : parsedMeidaType.SubTypeWithoutSuffix;
            if(primaryMedieType == "vnd.marvin.author.full")
            {
                var fullResourceReturn = _mapper.Map<AuthorFullDto>(author).ShapeData(fields) as IDictionary<string, object>;
                if (includeLinks)
                {
                    fullResourceReturn.Add("links",  links);
                }
                return Ok(fullResourceReturn);
            }

            var ResourceToReturn = _mapper.Map<AuthorDto>(author).ShapeData(fields) as IDictionary<string, object>;
            if (includeLinks)
            {
                ResourceToReturn.Add("links", links);
            }
            return Ok(ResourceToReturn);

            //if (parsedMeidaType.MediaType == "application/vnd.marvin.hateoas+json")
            //{
            //   // var links = CreateLinkForAuthor(authorId, fields);
            //    var linksResourceToReturn = _mapper.Map<AuthorDto>(author).ShapeData(fields) as IDictionary<string, object>;
            //    linksResourceToReturn.Add("links", links);
            //    return Ok(linksResourceToReturn);
            //}


           // return Ok(_mapper.Map<AuthorDto>(author).ShapeData(fields));
        }

       

        [HttpPost(Name = "CreateAuthorWithDateOfDeath")]
        [RequestHeaderMatchMediaType("Content-Type",            
            "application/vnd.marvin.authorforcreationwithdateofdeath+json")]
        [Consumes("application/vnd.marvin.authorforcreationwithdateofdeath+json")]
        public ActionResult<AuthorDto> CreateAuthorWithDateOfDeath(AuthorForCreationWithDateOfDeathDto author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            var authorEntity = _mapper.Map<Entities.Author>(author);
            _courselibraryRepository.AddAuthor(authorEntity);
            _courselibraryRepository.Save();
            var authorDto = _mapper.Map<AuthorDto>(authorEntity);

            var links = CreateLinkForAuthor(authorDto.Id, null);

            var linkedResourceForReturn = authorDto.ShapeData(null) as IDictionary<string, object>;
            linkedResourceForReturn.Add("links", links);

            return CreatedAtRoute("GetAuthor", new { authorId = linkedResourceForReturn["Id"] }, linkedResourceForReturn);
        }


        [HttpPost(Name = "CreateAuthor")]
        [RequestHeaderMatchMediaType("Content-Type",
           "application/json",
           "application/vnd.marvin.authorforcreation+json")]
        [Consumes("application/json",
            "application/vnd.marvin.authorforcreation+json")]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            var authorEntity = _mapper.Map<Entities.Author>(author);
            _courselibraryRepository.AddAuthor(authorEntity);
            _courselibraryRepository.Save();
            var authorDto = _mapper.Map<AuthorDto>(authorEntity);

            var links = CreateLinkForAuthor(authorDto.Id, null);

            var linkedResourceForReturn = authorDto.ShapeData(null) as IDictionary<string, object>;
            linkedResourceForReturn.Add("links", links);

            return CreatedAtRoute("GetAuthor", new { authorId = linkedResourceForReturn["Id"] }, linkedResourceForReturn);
        }


        [HttpDelete("{authorId}", Name ="DeleteAuthor")]
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
                        new {
                            fields = authorResourceParameters.Fields,
                        orderby = authorResourceParameters.OrderBy,
                            pageNumber = authorResourceParameters.PageNumber - 1,
                            pageSize = authorResourceParameters.PageSize,
                            mainCategory = authorResourceParameters.MainCategory,
                            searchQuery = authorResourceParameters.SearchQuery }); ;
                case ResourceUriType.NextPage:
                    return Url.Link("GetAuthors", 
                        new
                        {
                            fields = authorResourceParameters.Fields,
                            orderby = authorResourceParameters.OrderBy,
                            pageNumber = authorResourceParameters.PageNumber + 1,
                        pageSize = authorResourceParameters.PageSize,
                        mainCategory = authorResourceParameters.MainCategory,
                        searchQuery = authorResourceParameters.SearchQuery
                    });
                case ResourceUriType.Current:
                default:
                    return Url.Link("GetAuthors",
                        new
                        {
                            fields = authorResourceParameters.Fields,
                            orderby = authorResourceParameters.OrderBy,
                            pageNumber = authorResourceParameters.PageNumber,
                            pageSize = authorResourceParameters.PageSize,
                            mainCategory = authorResourceParameters.MainCategory,
                            searchQuery = authorResourceParameters.SearchQuery

                        });
            }

        }
    
    private IEnumerable<LinkDto> CreateLinkForAuthor(Guid authorId, string fields)
        {
            var links = new List<LinkDto>();
            if (string.IsNullOrEmpty(fields))
            {
                links.Add(new LinkDto(Url.Link("GetAuthor", new { authorId = authorId }), "self", "GET"));                 
            }
            else
            {
                links.Add(new LinkDto(Url.Link("GetAuthor", new { authorId, fields }), "self", "GET"));
            }

            links.Add(new LinkDto(Url.Link("DeleteAuthor", new { authorId }), "delete_author", "DELETE"));
            links.Add(new LinkDto(Url.Link("CreateCourseForAuthor", new { authorId}), "create_course_for_author", "POST"));
            links.Add(new LinkDto(Url.Link("GetCoursesForAuthor", new { authorId }), "courses", "GET"));


            return links;
        }

        private IEnumerable<LinkDto> CreateLinkForAuthors(AuthorResourceParameters authorResourceParameters, bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            links.Add(new LinkDto(CreateAuthorsResourceUri(authorResourceParameters, ResourceUriType.Current), "self", "GET"));
            if (hasNext)
                links.Add(new LinkDto(CreateAuthorsResourceUri(authorResourceParameters, ResourceUriType.NextPage), "nextPage", "GET"));
            if (hasPrevious)
                links.Add(new LinkDto(CreateAuthorsResourceUri(authorResourceParameters, ResourceUriType.PreviousPage), "previousPage", "GET"));

            return links;
        }
    }

   
}
