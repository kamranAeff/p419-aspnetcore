using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Basket : ICreateEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
