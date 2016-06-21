using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Profile.Data;
using TIC.PuppyLove.Services.Contracts.Common;

namespace TIC.PuppyLove.Profile.DataModel
{
    public class QuestionEntity
    {
        public Attribute Question { get; set; }
        
        // Questions
        public ProfileEntity CreateQuestionEntityInstance()
        {
            return new ProfileEntity
            {
                ID = Question.ID,
                Text = Question.Question,
                QuestionID = Question.QuestionTypeID
            };
        }

    }
}
