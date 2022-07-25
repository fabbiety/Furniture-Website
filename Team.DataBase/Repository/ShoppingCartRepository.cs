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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly TeamBaseContext _db;

        public ShoppingCartRepository(TeamBaseContext db) : base(db)
        {
            _db = db;
        }

        public int DecreaseCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            _db.SaveChanges();
            return shoppingCart.Count;
        }

        public int IncreaseCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            _db.SaveChanges();
            return shoppingCart.Count;
        }
    }
}
