using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LernMe.Models.Lesson
{
    public class LessonBasic
    {
        public int Id { get; set;}

        public List<LessonsAssigned> LessonAssigned { get; set; }

    }
}
