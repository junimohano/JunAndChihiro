using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JunAndChhiroWebApi.Data.Db;

namespace JunAndChihiroWebApi.Service.Service
{
    public interface IFileService
    {
        Task<List<JFile>> GetFiles(Guid folderOid);
        Task<bool> SetFile(JFile jFile);
    }
}