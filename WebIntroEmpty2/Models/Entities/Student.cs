namespace WebIntroEmpty2.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public int GroupId { get; set; }
    }
}
