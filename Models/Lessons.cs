using System;

namespace LearnMeAPI.Models
{
    public class Lessons
    {
        public int Id { get; set; }
        public DateTime LessonDate { get; set; }
        public string Title { get; set; }
        public string LessonContent { get; set; }
        public string FileString { get; set; }
        public bool IsDone { get; set; }
    }
}
