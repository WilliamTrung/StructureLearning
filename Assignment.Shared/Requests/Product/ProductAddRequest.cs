using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment.Shared.Requests.Product
{
    public class ProductAddRequest : BaseRequest
    {
        [JsonIgnore]
        public override string? Id { get => base.Id; set => base.Id = value; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
    }
}
