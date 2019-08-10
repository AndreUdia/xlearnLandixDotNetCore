using xlearn.Models;
using xlearn.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace xlearn.Services {
    public class ProfileServices : RepositoryBase<Profile> {
        public ProfileServices(IConfiguration configuration) : base(configuration) { }

    }
}