﻿namespace Domain.Entities
{
    public class ProductImage : AuditableEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public bool IsMain { get; set; }
        public string Path { get; set; }
    }
}
