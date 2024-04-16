using Assignment.Shared.Models;
using Assignment.Shared.Responses.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Responses.Product
{
    public class ProductResponse : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IEnumerable<CategoryProductResponse> Categories { get; set; } = null!;
    }
}
