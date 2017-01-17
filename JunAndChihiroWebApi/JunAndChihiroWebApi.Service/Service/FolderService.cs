using System;
using System.Collections.Generic;
using System.Linq;
using JunAndChhiroWebApi.Data.Db;

namespace JunAndChihiroWebApi.Service.Service
{
    public class FolderService : IFolderService
    {
        private readonly JunAndChihiroEntities _db = new JunAndChihiroEntities();

        public List<JFolder> GetRoot()
        {
            return _db.JFolders.Where(x => x.FolderOid == new Guid("79EAADE1-04F3-47D3-86F9-187FC9F634DA")).ToList();
        }

        public List<JFolder> GetFolders(Guid folderOid)
        {
            List<JFolder> list = new List<JFolder>();
            var originFolder = _db.XFolderHierarchies.FirstOrDefault(x => x.OidDestination == folderOid);

            if (originFolder != null)
                list.Add(_db.JFolders.FirstOrDefault(x => x.FolderOid == originFolder.OidOrigin));
            
            var result = _db.JFolders.Join(_db.XFolderHierarchies, x => x.FolderOid, y => y.OidOrigin, (j, x) => new { j, x })
               .Join(_db.JFolders, x => x.x.OidDestination, y => y.FolderOid, (x, y) => new { x, y })
               .Where(x => x.x.j.FolderOid == folderOid)
               .Select(x => x.y)
               .ToList();

            if (result.Any())
                list.AddRange(result);

            return list;
        }
    }
}