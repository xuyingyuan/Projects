﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public static  class DateTimeOffsetExtenstions
    {
        public static int getCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTimeOffset.Year;

            if (currentDate < dateTimeOffset.AddYears(age))
                age--;
            return age;

        }
    }
}
