using Assignment.Shared.Models;
using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        [Ignore]
        public List<Category>? Categories { get; set; }
    }
}
