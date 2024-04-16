
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment.Shared.Requests.Category
{
    public class CategoryAddRequest : BaseRequest
    {
        [JsonIgnore]
        public override string? Id { get => base.Id; set => base.Id = value; }
        public string Name { get; set; } = null!;
        public string? ParentId { get; set; }
    }
}
