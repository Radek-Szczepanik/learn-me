using LearnMe.Infrastructure.DbModels.Base;
using LearnMe.Infrastructure.Enum;

namespace LearnMe.Infrastructure.DbModels.Domains.Home
{
    /// <summary>
    /// Exercise class relates to eg. language level test files available for not-logged users
    /// </summary>
    public class Exercise : BaseHome
    {      
        public ExerciseGroup ExerciseGroup { get; set; }
    }
}
