﻿using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Profiles
{
    public class CoursesProfile:Profile
    {
        public CoursesProfile()
        {
            CreateMap<Entities.Course, Models.CoursesDto>();

            CreateMap<Models.CourseForCreationDto, Entities.Course>();

            CreateMap<Models.CourseForUpdateDto, Entities.Course>();

            CreateMap<Entities.Course, Models.CourseForUpdateDto>();
        }
    }
}
