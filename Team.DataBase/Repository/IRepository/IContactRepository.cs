﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Models;

namespace Team.DataBase.Repository.IRepository
{
    public interface IContactRepository : IRepository<Contact>
    {
        void Update(Contact obj);

    }
}
