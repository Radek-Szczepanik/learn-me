using System;

namespace LearnMeAPI.Models
{
    public class Homeworks
    {
        public int Id { get; set; }
        public DateTime HomeworkDate { get; set; }
        public string Title { get; set; }
        public string HomeworkContent { get; set; }
        public string FileString { get; set; }
        public bool IsDone { get; set; }
    }
}
