using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.DataBase.Data;
using Team.DataBase.Repository.IRepository;
using Team.Models;

namespace Team.DataBase.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly TeamBaseContext _db;  //Extracting appDbContext Using Independency Injection through the base class

        public CategoryRepository(TeamBaseContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(Category category)
        {
            var objFromDb = _db.Category1.FirstOrDefault(u =>u.Id == category.Id);
            objFromDb.Name = category.Name;
            objFromDb.Orders = category.Orders;
            
        }
    }
}
