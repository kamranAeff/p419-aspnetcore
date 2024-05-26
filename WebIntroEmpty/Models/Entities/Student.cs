using System.ComponentModel.DataAnnotations.Schema;

namespace WebIntroEmpty.Models.Entities
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string GroupNo { get; set; }
    }
}
