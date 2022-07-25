using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team.DataBase.Repository.IRepository
{
    public interface IUnitOfWork :IDisposable
    {
        ICategoryRepository Category { get; }
        IFurnitureTypeRepository FurnitureTypes { get; }
        IProductsRepository Products { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailsRepository OrderDetails { get; }
        IAppUserRepository AppUser { get; }
        IContactRepository Contact { get; }

        void Save();

    }
}
