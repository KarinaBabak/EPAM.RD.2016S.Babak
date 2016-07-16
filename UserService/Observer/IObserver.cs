﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorage.Repository;

namespace UserService.Observer
{
    public interface IObserver
    {
        void Update(IUserRepository repository);
    }
}
