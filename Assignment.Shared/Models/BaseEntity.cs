using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Models
{
    public class BaseEntity : IBaseEntity, IIdentity, IActiveEntity
    {
        [Key]
        public string Id { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? LastUpdatedBy { get; set; }
        public bool IsActive { get; set; }

        public void GenerateId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
