using System;
using System.Collections.Generic;
using JunAndChhiroWebApi.Data.Db;

namespace JunAndChihiroWebApi.Service.Service
{
    public interface IUserService
    {
        List<JUser> GetAll();
        JUser Get(Guid userOid);
        JUser GetLogin(string id, string pw);
    }
}