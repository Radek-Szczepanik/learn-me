using System.ComponentModel.DataAnnotations;

namespace LearnMe.Infrastructure.DbModels.Base
{
    public abstract class BaseEntity
    {
        
        public int Id { get; set; }
    }
}
