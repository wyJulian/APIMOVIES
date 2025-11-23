using System.ComponentModel.DataAnnotations;

namespace APIMOVIES.DAL.Models
{
    public class AuditBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
