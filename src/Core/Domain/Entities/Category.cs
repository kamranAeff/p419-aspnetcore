﻿namespace Domain.Entities
{
    public class Category : AuditableEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
