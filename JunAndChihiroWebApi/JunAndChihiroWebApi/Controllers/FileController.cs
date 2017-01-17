using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using JunAndChhiroWebApi.Data.Db;
using JunAndChihiroWebApi.Service.Service;

namespace JunAndChihiroWebApi.Controllers
{
    public class FileController : ApiController
    {
        private readonly IFileService _fileService = new FileService();

        [HttpGet]
        public async Task<IHttpActionResult> GetFiles(Guid folderOid)
        {
            var user = await _fileService.GetFiles(folderOid);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IHttpActionResult> SetFile(JFile jFile)
        {
            var result = await _fileService.SetFile(jFile);
            if (result == false)
                return NotFound();

            return Ok(result);
        }

    }
}
