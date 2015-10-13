using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using CommonClasses;

namespace ServerSTUNLibrary.Models
{
    public class FileUriData
    {
        public FileUriData(string fileName, string uri)
        {
            FileName = fileName;
            Uri = uri;
        }

        public string FileName { get; set; }
        public string Uri { get; set; }
    }

    public class FileUriDataModel
    {
        public static FileUriDataModel GetUriList(Uri baseUri)
        {
            var appDataDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            var listFiles = Directory.GetFiles(appDataDirectoryPath, "*.txt", SearchOption.TopDirectoryOnly).Select(
                    file =>
                    {
                        var fileName = Path.GetFileName(file);
                        var uri = string.Format("{0}://{1}:{2}/api/file/download/{3}", baseUri.Scheme, baseUri.DnsSafeHost,
                            baseUri.Port, fileName);
                        return new FileUriData(fileName, uri);
                    }).ToList();
            return new FileUriDataModel {Files = listFiles};
        }

        public List<FileUriData> Files { get; set; } 
    }
}
