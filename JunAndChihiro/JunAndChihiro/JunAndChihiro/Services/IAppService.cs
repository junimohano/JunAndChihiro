using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JunAndChihiro.Models;

namespace JunAndChihiro.Services
{
    public interface IAppService
    {
        Task<bool?> GetLogin(string id, string pw);
        Task<List<JFolder>> GetRootFolder();
        Task<List<JFolder>> GetFolder(Guid folderOid);
        Task<List<JFile>> GetFiles(Guid folderOid);
        Task<bool> SetFile(JFile jFile);
        //      Task<List<User>> RefreshDataAsync ();

        //Task SaveTodoItemAsync (User item, bool isNewItem);

        //Task DeleteTodoItemAsync (string id);
    }
}
