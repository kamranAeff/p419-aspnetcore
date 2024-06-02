namespace WebIntroEmpty2.Models.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public int GroupId { get; set; }
        public required string GroupName { get; set; }
    }
}
