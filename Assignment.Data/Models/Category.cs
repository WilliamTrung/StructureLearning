using Assignment.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? ParentId { get; set; }
        public virtual ICollection<Product> Products { get; set; } = null!;
        public virtual Category? Parent { get; set; }
        public virtual ICollection<Category>? Children { get; set; }
    }
}
