using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using JunAndChhiroWebApi.Data.Db;
using JunAndChihiroWebApi.Service.Service;

namespace JunAndChihiroWebApi.Controllers
{
    public class FolderController : ApiController
    {
        private readonly IFolderService _folderService = new FolderService();

        public List<JFolder> Get()
        {
            return _folderService.GetRoot();
        }

        public IHttpActionResult GetFolder(Guid folderOid)
        {
            var user = _folderService.GetFolders(folderOid);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
