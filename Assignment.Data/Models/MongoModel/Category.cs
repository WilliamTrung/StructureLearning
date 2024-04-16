using Assignment.Shared.Models;
using AutoMapper.Configuration.Annotations;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Models.MongoModels
{
    public class Category : BaseMongoEntity
    {
        public string Name { get; set; } = null!;
        public string? ParentId { get; set; }
        [NotMapped]
        [BsonIgnore]
        public Category? Parent { get; set; }
        [NotMapped]
        [BsonIgnore]
        public List<Category>? Children { get; set; }
    }
}
