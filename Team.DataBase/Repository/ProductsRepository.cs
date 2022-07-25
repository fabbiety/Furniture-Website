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
    public class ProductsRepository : Repository<Products>, IProductsRepository
    {
        private readonly TeamBaseContext _db;  //Extracting appDbContext Using Independency Injection through the base class

        public ProductsRepository(TeamBaseContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Products> Search(string searchItem)
        {
            if (string.IsNullOrEmpty(searchItem))
            {
                return _db.Products;


             }
            return _db.Products.Where(p => p.Description.Contains(searchItem));
        }

        public void Update(Products obj)    //This action is performed so that update is only done the specfic object.
        {
            var objFromDb = _db.Products.FirstOrDefault(u =>u.Id == obj.Id);
            objFromDb.Name = obj.Name;
            objFromDb.Description = obj.Name;
            objFromDb.Price = obj.Price;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.FurnitureTypeId = obj.FurnitureTypeId;
            if (objFromDb.Image != null)
            {
                objFromDb.Image = obj.Image;
            }


        }
    }
}
