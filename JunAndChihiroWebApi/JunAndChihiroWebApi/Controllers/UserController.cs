using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JunAndChhiroWebApi.Data.Db;
using JunAndChihiroWebApi.Service.Service;

namespace JunAndChihiroWebApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService = new UserService();

        public IEnumerable<JUser> Get()
        {
            return _userService.GetAll();
        }

        public IHttpActionResult Get(Guid userOid)
        {
            var user = _userService.Get(userOid);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        public bool GetLogin(string id, string pw)
        {
            var user = _userService.GetLogin(id, pw);
            if (user == null)
                return false;

            return true;
        }
    }
}
