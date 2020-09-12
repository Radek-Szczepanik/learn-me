using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LernMe.Models;

namespace LernMe.Models.Lesson
{
    public class HomeworkBasic
    {   
        public int HomeworkId { get; set; }


        public int LessonsAssignedId { get; set; }
        public LessonsAssigned Lesson { get; set; }


    }
}
