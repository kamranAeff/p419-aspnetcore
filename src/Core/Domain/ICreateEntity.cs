namespace Domain
{
    public interface ICreateEntity
    {
        public int CreateBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
