using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment.Shared.Requests.Category
{
    public class CategoryUpdateRequest : BaseRequest
    {
        [Required]
        [NotNull]
        public override string? Id { get; set; }
        public string? Name { get; set; }
        public string? ParentId { get; set; }
    }
}
