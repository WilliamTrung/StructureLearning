using Assignment.Shared.Models;
using AutoMapper.Configuration.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Models.MongoModels
{
    public class Product : BaseMongoEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        [NotMapped]
        [BsonIgnore]
        public virtual ICollection<Category>? Categories { get; set; }
    }
}
