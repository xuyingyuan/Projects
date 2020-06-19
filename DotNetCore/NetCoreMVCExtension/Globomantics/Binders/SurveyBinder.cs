using Globomantics.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Globomantics.Core.Models;
namespace Globomantics.Binders
{
    public class SurveyBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var questions = bindingContext.ValueProvider.GetValue("q");
            var submission = new List<Core.Models.Submission>();
            foreach(var question in questions)
            {
                var answers = question.Split(",");
                submission.Add(new Core.Models.Submission()
                {
                    Id = int.Parse(answers[0]),
                    Rating = int.Parse(answers[1]),
                    Comment = answers[2]
                });
            }
            bindingContext.Result = ModelBindingResult.Success(submission);
            return Task.CompletedTask;
        }
    }
}
