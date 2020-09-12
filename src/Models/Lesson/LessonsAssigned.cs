using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LernMe.Models;
using QuestStore3.Models;

namespace LernMe.Models.Lesson
{
    public class LessonsAssigned
    {
        public int UserId { get; set; }
        public int LessonId { get; set; }
        public LessonBasic Lesson { get; set; }
        public User User { get; set; }
        public List<HomeworkBasic> Homeworks { get; set; }

    }
}
