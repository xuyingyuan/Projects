using AutoMapper;
using CourseLibrary.API.Entities;
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
    [Route("api/authors/{authorid}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courselibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courselibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public ActionResult<IEnumerable<CoursesDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_courselibraryRepository.AuthorExists(authorId))
                return NotFound();
            var courses = _courselibraryRepository.GetCourses(authorId);
            if (courses == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<CoursesDto>>(courses));
        }

        [HttpGet("{courseId}", Name="GetCoursesForAuthor")]
        public ActionResult<IEnumerable<CoursesDto>> GetCoursesForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courselibraryRepository.AuthorExists(authorId))
                return NotFound();
            var course = _courselibraryRepository.GetCourse(authorId, courseId);
            if (course == null)
                return NotFound();

            return Ok(_mapper.Map<CoursesDto>(course));
        }

        [HttpPost]
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
    }

   
}
