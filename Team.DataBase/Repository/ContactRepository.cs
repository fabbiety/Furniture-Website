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
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private readonly TeamBaseContext _db;  //Extracting appDbContext Using Independency Injection through the base class

        public ContactRepository(TeamBaseContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(Contact obj)
        {
            var objFromDb = _db.Contact.FirstOrDefault(u => u.Id == obj.Id);
            
            objFromDb.Name = obj.Name;
            objFromDb.Message = obj.Message;
            objFromDb.Email = obj.Email;
            objFromDb.ContactNumber = obj.ContactNumber;

        }
    }
}
