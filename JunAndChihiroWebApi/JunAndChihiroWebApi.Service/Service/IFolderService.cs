using System;
using System.Collections.Generic;
using JunAndChhiroWebApi.Data.Db;

namespace JunAndChihiroWebApi.Service.Service
{
    public interface IFolderService
    {
        List<JFolder> GetRoot();
        List<JFolder> GetFolders(Guid folderOid);

    }
}