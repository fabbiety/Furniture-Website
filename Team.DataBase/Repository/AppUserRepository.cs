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
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly TeamBaseContext _db;

        public AppUserRepository(TeamBaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(AppUser AppUser)
        {
            var objFromDb = _db.AppUser.FirstOrDefault(u => u.Email == AppUser.Email);
            objFromDb.FirstName = AppUser.FirstName;
            objFromDb.LastName = AppUser.LastName;
            objFromDb.Email = AppUser.Email;

        }

    }
}
