using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public abstract class Base
    {
        [Key]
        public long Id { get; set; }
    }
}
