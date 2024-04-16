using Assignment.Data.Models.MongoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Data.Repositories.Abstractions.Mongo
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
