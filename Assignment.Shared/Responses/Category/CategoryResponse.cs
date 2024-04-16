using Assignment.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Responses.Category
{
    public class CategoryResponse : BaseEntity
    {
        public string Name { get; set; } = null!;
        public CategoryResponse? Parent { get; set; }
        public List<CategoryResponse>? Children { get; set; } = null;
    }
}
