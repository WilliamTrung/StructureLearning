using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment.Shared.Requests.Product
{
    public class ProductUpdateRequest : BaseRequest
    {
        [Required]
        [NotNull]
        public override string? Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CategoryId { get; set; }

    }
}
