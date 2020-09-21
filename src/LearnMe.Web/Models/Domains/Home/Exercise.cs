using LearnMe.Models.Base;
using LearnMe.Enum;

namespace LearnMe.Models.Domains.Home
{
    /// <summary>
    /// Exercise class relates to eg. language level test files available for not-logged users
    /// </summary>
    public class Exercise : BaseHome
    {      
        public ExerciseGroup ExerciseGroup { get; set; }
    }
}
