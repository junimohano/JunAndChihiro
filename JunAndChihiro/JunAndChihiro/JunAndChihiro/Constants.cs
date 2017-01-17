using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JunAndChihiro.Models;

namespace JunAndChihiro
{
    public static class Constants
    {
        private static readonly string _restUrl = "http://junimohano.ddns.net/";
        public static string RestUrlApi = _restUrl + "api/";
        public static string RestUrlUpload = _restUrl + "Upload/";

        private static readonly string _restUrlInside = "http://192.168.2.245/";
        private static readonly string RestUrlApiInside = _restUrlInside + "api/";
        private static readonly string RestUrlUploadInside = _restUrlInside + "Upload/";

        public static string Username = "Id";
        public static string Password = "Password";

        public enum FileType
        {
            Gif,
            Image,
            Video,
            Music
        }

        public static FileType GetFileType(JFile jFile)
        {
            if (jFile.FilePath.Contains(".mp4") ||
                jFile.FilePath.Contains(".avi") ||
                jFile.FilePath.Contains(".wmv") ||
                jFile.FilePath.Contains(".mkv"))
                return FileType.Video;
            else if (jFile.FilePath.Contains(".mp3"))
                return FileType.Music;
            else if (jFile.FilePath.Contains(".gif"))
                return FileType.Gif;
            else
                return FileType.Image;
        }


        public static void SetLocalIp(bool check)
        {
            if (check)
            {
                RestUrlApi = RestUrlApiInside;
                RestUrlUpload = RestUrlUploadInside;
            }
        }

    }
}
