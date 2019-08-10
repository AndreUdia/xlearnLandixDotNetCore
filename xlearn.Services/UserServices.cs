using Microsoft.Extensions.Configuration;
using xlearn.Models;
using xlearn.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace xlearn.Services {
    public class UserServices : RepositoryBase<User> {
        public UserServices(IConfiguration configuration) : base(configuration) { }

    }
}