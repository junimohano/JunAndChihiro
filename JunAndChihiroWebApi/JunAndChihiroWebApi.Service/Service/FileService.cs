using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Hosting;
using JunAndChhiroWebApi.Data.Db;
using System.Net.Http;
using System.Drawing;
using System.Drawing.Imaging;

namespace JunAndChihiroWebApi.Service.Service
{
    public class FileService : IFileService
    {
        private readonly JunAndChihiroEntities _db = new JunAndChihiroEntities();

        public Bitmap GetVideoThumbnail(string path, string saveThumbnailTo, int seconds)
        {
            string parameters = string.Format("-ss {0} -i {1} -f image2 -vframes 1 -y {2}", seconds, path, saveThumbnailTo);

            var processInfo = new ProcessStartInfo();
            processInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/Libs/") + "ffmpeg.exe";
            processInfo.Arguments = parameters;
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            System.IO.File.Delete(saveThumbnailTo);

            using (var process = new Process())
            {
                process.StartInfo = processInfo;
                process.Start();
                process.WaitForExit();
            }

            var ms = new MemoryStream(System.IO.File.ReadAllBytes(saveThumbnailTo));
            return (Bitmap)Image.FromStream(ms);
        }

        public async Task<List<JFile>> GetFiles(Guid folderOid)
        {
            var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/");

            var jFolder = await _db.JFolders.FirstOrDefaultAsync(x => x.FolderOid == folderOid);

            var jFile = await _db.JFiles.Where(x => x.FolderOid == folderOid).ToListAsync();

            foreach (var file in Directory.GetFiles(mappedPath + jFolder.RootFolderPath))
            {
                byte[] thumbImage = null;
                var fileInfo = new FileInfo(file);

                try
                {
                    if (fileInfo.Extension == ".avi" ||
                       fileInfo.Extension == ".mkv" ||
                       fileInfo.Extension == ".mp4" ||
                       fileInfo.Extension == ".wmv")
                    {
                        var test = GetVideoThumbnail(file, mappedPath + "tempThumbnail.jpg", 1);
                        thumbImage = ImageToByte(test);
                    }
                    else if (fileInfo.Extension == ".mp3")
                    {
                        //
                    }
                    else
                    {
                        Image temp = Image.FromFile(file)?.GetThumbnailImage(100, 100, () => false, IntPtr.Zero);
                        thumbImage = ImageToByte(temp);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }

                var f = jFile.FirstOrDefault(x => x.FileName.ToLower() == fileInfo.Name.ToLower());
                if (f == null)
                {
                    jFile.Add(new JFile()
                    {
                        FileName = fileInfo.Name,
                        FolderPath = jFolder.RootFolderPath,
                        FolderOid = jFolder.RootFolderOid,
                        Thumb = thumbImage,
                        IsExist = true
                    });
                }
                else
                {
                    f.Thumb = thumbImage;
                    f.IsExist = true;
                }
            }

            return jFile.Where(x => x.IsExist).ToList();
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public async Task<bool> SetFile(JFile jFile)
        {
            using (var trans = _db.Database.BeginTransaction())
            {
                try
                {
                    JunAndChhiroWebApi.Data.Db.File file = null;
                    if (jFile.FileOid == Guid.Empty)
                    {
                        var coreTableInfo = _db.CoreTableInfoes.Add(new CoreTableInfo()
                        {
                            Oid = Guid.NewGuid(),
                            CreatedDate = DateTime.Now,
                            CreatedOid = new Guid("D078528A-FCC6-483B-87EA-7B5D4A07BEF7")
                        });

                        file = new JunAndChhiroWebApi.Data.Db.File();
                        file.FileOid = coreTableInfo.Oid;

                        _db.CoreRelationInfoes.Add(new CoreRelationInfo()
                        {
                            OidDestination = file.FileOid,
                            OidOrigin = (Guid)jFile.FolderOid
                        });
                    }
                    else
                    {
                        file = await _db.Files.FirstOrDefaultAsync(x => x.FileOid == jFile.FileOid);
                        file.CoreTableInfo.UpdatedOid = new Guid("D078528A-FCC6-483B-87EA-7B5D4A07BEF7");
                        file.CoreTableInfo.UpdatedDate = DateTime.Now;
                    }

                    file.Name = jFile.Name;
                    file.Date = jFile.Date;
                    file.FileName = jFile.FileName;
                    file.Description = jFile.Description;
                    _db.Files.AddOrUpdate(file);

                    await _db.SaveChangesAsync();

                    trans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    trans.Rollback();
                }
            }

            return false;
        }

        //public async Task<JFile> GetFile(Guid folderOid, string fileName)
        //{
        //    var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Upload/");

        //    var jFolder = await _db.JFolders.FirstOrDefaultAsync(x => x.FolderOid == folderOid);

        //    foreach (var file in Directory.GetFiles(mappedPath + jFolder.RootFolderPath))
        //    {
        //        if (file == fileName)
        //        {
        //            var jFile = await _db.JFiles.FirstOrDefaultAsync(x => x.FolderOid == folderOid && x.FileName == fileName);
        //            if (jFile == null)
        //            {
        //                jFile = new JFile()
        //                {
        //                    FileName = file,
        //                    FolderPath = jFolder.RootFolderPath,
        //                    FolderOid = jFolder.RootFolderOid
        //                };
        //            }
        //            return jFile;
        //        }
        //    }

        //    return null;
        //}

    }
}