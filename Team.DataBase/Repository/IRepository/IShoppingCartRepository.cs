using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Models;

namespace Team.DataBase.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
       
      int IncreaseCount (ShoppingCart shoppingCart, int count);
      int DecreaseCount(ShoppingCart shoppingCart, int count);
    }
}
