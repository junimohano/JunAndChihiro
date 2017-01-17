using System;
using System.Collections.Generic;
using System.Linq;
using JunAndChhiroWebApi.Data.Db;

namespace JunAndChihiroWebApi.Service.Service
{
    public class UserService : IUserService
    {
        private readonly JunAndChihiroEntities _db = new JunAndChihiroEntities();

        public List<JUser> GetAll()
        {
            return _db.JUsers.ToList();
        }

        public JUser Get(Guid userOid)
        {
            return _db.JUsers.FirstOrDefault(x => x.UserOid == userOid);
        }

        public JUser GetLogin(string id, string pw)
        {
            return _db.JUsers.FirstOrDefault(x => x.Id.ToLower() == id.ToLower() && x.Password.ToLower() == pw.ToLower());
        }
    }
}