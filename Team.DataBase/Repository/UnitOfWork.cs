using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.DataBase.Data;
using Team.DataBase.Repository.IRepository;

namespace Team.DataBase.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TeamBaseContext _db;
        public UnitOfWork(TeamBaseContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            FurnitureTypes = new FurnitureTypeRepository(_db);
            Products = new ProductsRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db); 
            OrderHeader = new OrderHeaderRepository(_db);
            AppUser = new AppUserRepository(_db);
            Contact = new ContactRepository(_db);

        }

        public ICategoryRepository Category { get; private set; }
        public IFurnitureTypeRepository FurnitureTypes { get; private set; }
        public IProductsRepository Products { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public IAppUserRepository AppUser { get; private set; }

        public IContactRepository Contact { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }

}
