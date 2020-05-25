using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;
using CourseLibrary.API.Services;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorid}/courses")]
    // [ResponseCache(CacheProfileName = "240secondsCacheProfile")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public)]
    [HttpCacheValidation(MustRevalidate = true)]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courselibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courselibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(Name ="GetCoursesForAuthor")]
      
        public ActionResult<IEnumerable<CoursesDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_courselibraryRepository.AuthorExists(authorId))
                return NotFound();
            var courses = _courselibraryRepository.GetCourses(authorId);
            if (courses == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<CoursesDto>>(courses));
        }

        [HttpGet("{courseId}", Name="GetCourseForAuthor")]
       // [ResponseCache(Duration =120)]
       [HttpCacheExpiration(CacheLocation=CacheLocation.Public, MaxAge =1000)]
       [HttpCacheValidation(MustRevalidate =true)]
        public ActionResult<IEnumerable<CoursesDto>> GetCoursesForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courselibraryRepository.AuthorExists(authorId))
                return NotFound();
            var course = _courselibraryRepository.GetCourse(authorId, courseId);
            if (course == null)
                return NotFound();

            return Ok(_mapper.Map<CoursesDto>(course));
        }

        [HttpPost(Name ="CreateCourseForAuthor")]
        public ActionResult<CoursesDto> CreateCourseForAuthor(Guid authorId, CourseForCreationDto courseForCreationDto)
        {
            if (!_courselibraryRepository.AuthorExists(authorId)){
                return NotFound();
            }

            var courseEntity = _mapper.Map<Entities.Course>(courseForCreationDto);
            _courselibraryRepository.AddCourse(authorId, courseEntity);
            _courselibraryRepository.Save();
            var courseDto = _mapper.Map<CoursesDto>(courseEntity);
            return CreatedAtRoute("GetCoursesForAuthor", new { authorId = authorId, courseId = courseDto.Id }, courseDto);
        }

        [HttpPut("{courseId}")]
        public IActionResult UpdateCourseForAuthor(Guid authorId, Guid courseId, CourseForUpdateDto courseForUpdateDto)
        {
            if (!_courselibraryRepository.AuthorExists(authorId))
            {
                return NotFound();  //404
            }

            var courseForAuthorFromRpo = _courselibraryRepository.GetCourse(authorId, courseId);
            if (courseForAuthorFromRpo == null)
            {
                var courseToAdd = _mapper.Map<Entities.Course>(courseForUpdateDto);
                courseToAdd.Id = courseId;
                _courselibraryRepository.AddCourse(authorId, courseToAdd);

                _courselibraryRepository.Save();

                var courseToReturn = _mapper.Map<CoursesDto>(courseToAdd);

                return CreatedAtRoute("GetCoursesForAuthor", new {authorId = authorId, courseId = courseToReturn.Id }, courseToReturn);
            }
           
           
                _mapper.Map(courseForUpdateDto, courseForAuthorFromRpo);
                _courselibraryRepository.UpdateCourse(courseForAuthorFromRpo);   
                _courselibraryRepository.Save();

            return NoContent();

            
        }

        [HttpPatch("{courseId}")]
        public IActionResult PartiallyUpdateCourseForAuthor(Guid authorId, Guid courseId, 
            JsonPatchDocument<CourseForUpdateDto> patchDocument)
        {
            if (!_courselibraryRepository.AuthorExists(authorId))
            {
                return NotFound(); //404
            }

            var courseFromEntity = _courselibraryRepository.GetCourse(authorId, courseId);
            
            if(courseFromEntity == null)
            {
                // return NotFound(); //404
                //create a course => upsert for patch
                var sourseforUpdateDto = new CourseForUpdateDto();
                patchDocument.ApplyTo(sourseforUpdateDto, ModelState);
                if (!TryValidateModel(sourseforUpdateDto))
                {
                    return ValidationProblem(ModelState);
                }
                var coursetoAdd = _mapper.Map<Entities.Course>(sourseforUpdateDto);
                coursetoAdd.Id = courseId;

                _courselibraryRepository.AddCourse(authorId, coursetoAdd);
                _courselibraryRepository.Save();

                var coursetoReturn = _mapper.Map<CoursesDto>(coursetoAdd);

                return CreatedAtRoute("GetCoursesForAuthor", new { authorId = authorId, courseId = coursetoReturn.Id }, coursetoReturn);
            }

            var courseToPatch = _mapper.Map<CourseForUpdateDto>(courseFromEntity);
            //add validation
            patchDocument.ApplyTo(courseToPatch, ModelState);

            if (!TryValidateModel(courseToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(courseToPatch, courseFromEntity);
            _courselibraryRepository.UpdateCourse(courseFromEntity);
            _courselibraryRepository.Save();

            return NoContent();

        }

        [HttpDelete("{courseId}")]
        public ActionResult DeleteCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courselibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseToBeDelete = _courselibraryRepository.GetCourse(authorId, courseId);
            if(courseToBeDelete == null)
            {
                return NotFound();
            }

            _courselibraryRepository.DeleteCourse(courseToBeDelete);
            _courselibraryRepository.Save();

            return NoContent();

        }

        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelstatedicionary)
        {
            var options = HttpContext.RequestServices
                    .GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
            //return base.ValidationProblem(modelstatedicionary);
        }
    }

   
}
