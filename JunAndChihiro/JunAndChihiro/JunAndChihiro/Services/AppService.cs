using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JunAndChihiro.Models;
using Newtonsoft.Json;

namespace JunAndChihiro.Services
{
    public class AppService : IAppService
    {
        private readonly HttpClient _client;

        public AppService()
        {
            var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 2147483647;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<bool?> GetLogin(string id, string pw)
        {
            var uri = new Uri(string.Format(Constants.RestUrlApi + "User/GetLogin?id={0}&pw={1}", id, pw));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content == "true")
                        return true;
                    else
                        return false;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<List<JFolder>> GetRootFolder()
        {
            var uri = new Uri(string.Format(Constants.RestUrlApi + "Folder"));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<JFolder>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return new List<JFolder>();
        }

        public async Task<List<JFolder>> GetFolder(Guid folderOid)
        {
            var uri = new Uri(string.Format(Constants.RestUrlApi + "Folder/GetFolder?folderOid={0}", folderOid));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<JFolder>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return new List<JFolder>();
        }

        public async Task<List<JFile>> GetFiles(Guid folderOid)
        {
            var uri = new Uri(string.Format(Constants.RestUrlApi + "File/GetFiles?folderOid={0}", folderOid));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<JFile>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return new List<JFile>();
        }

        public async Task<bool> SetFile(JFile jFile)
        {
            var uri = new Uri(string.Format(Constants.RestUrlApi + "File/SetFile"));

            try
            {
                var json = JsonConvert.SerializeObject(jFile);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uri, content);
                //response = await _client.PutAsync(uri, content);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<bool>(result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }

        //public async Task<List<User>> RefreshDataAsync()
        //{
        //    Items = new List<User>();

        //    var uri = new Uri(string.Format(Constants.RestUrl + "user/GetLogin?id={0}&pw={1}", "jun", "1234"));

        //    try
        //    {
        //        var response = await _client.GetAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            Items = JsonConvert.DeserializeObject<List<User>>(content);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"				ERROR {0}", ex.Message);
        //    }

        //    return Items;
        //}

        //public async Task SaveTodoItemAsync(User item, bool isNewItem = false)
        //{
        //    // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
        //    var uri = new Uri(string.Format(Constants.RestUrl, item.Id));

        //    try
        //    {
        //        var json = JsonConvert.SerializeObject(item);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = null;
        //        if (isNewItem)
        //        {
        //            response = await _client.PostAsync(uri, content);
        //        }
        //        else
        //        {
        //            response = await _client.PutAsync(uri, content);
        //        }

        //        if (response.IsSuccessStatusCode)
        //        {
        //            Debug.WriteLine(@"				TodoItem successfully saved.");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"				ERROR {0}", ex.Message);
        //    }
        //}

        //public async Task DeleteTodoItemAsync(string id)
        //{
        //    // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}
        //    var uri = new Uri(string.Format(Constants.RestUrl, id));

        //    try
        //    {
        //        var response = await _client.DeleteAsync(uri);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            Debug.WriteLine(@"				TodoItem successfully deleted.");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"				ERROR {0}", ex.Message);
        //    }
        //}
    }
}
