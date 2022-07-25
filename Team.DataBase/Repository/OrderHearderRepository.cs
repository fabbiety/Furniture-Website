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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly TeamBaseContext _db;  //Extracting appDbContext Using Independency Injection through the base class

        public OrderHeaderRepository(TeamBaseContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(OrderHeader obj)
        {
           _db.OrderHeader.Update(obj);
            
        }
    }
}
