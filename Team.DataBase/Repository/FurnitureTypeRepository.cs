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
    public class FurnitureTypeRepository : Repository<FurnitureTypes>, IFurnitureTypeRepository
    {
        private readonly TeamBaseContext _db;  //Extracting appDbContext Using Independency Injection through the base class

        public FurnitureTypeRepository(TeamBaseContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(FurnitureTypes obj)
        {
            var objFromDb = _db.FurnitureTypes.FirstOrDefault(u =>u.Id == obj.Id);
            objFromDb.Name = obj.Name;
            
            
        }
    }
}
