using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Requests.Product
{
    public class ProductUpdateCategoryRequest : BaseRequest
    {
        [Required]
        [NotNull]
        public override string? Id { get; set; }
        public string CategoryId { get; set; } = null!;
    }
}
