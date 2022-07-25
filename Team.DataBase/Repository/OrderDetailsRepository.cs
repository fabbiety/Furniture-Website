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
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly TeamBaseContext _db;

        public OrderDetailsRepository(TeamBaseContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(OrderDetails obj)
        {
           _db.OrderDetails.Update(obj);
            
        }
    }
}
