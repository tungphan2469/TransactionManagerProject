using System.ComponentModel.DataAnnotations;

namespace TestProject.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }    
    }
}
