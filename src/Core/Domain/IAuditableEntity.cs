namespace Domain
{
    public interface IAuditableEntity : ICreateEntity
    {
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
